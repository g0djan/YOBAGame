using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Archimedes.Geometry;
using YOBAGame.MapObjects;

namespace YOBAGame
{
    class ConditionalAction
    {
        public Func<bool> Condition { get; }
        public Action Act { get; }

        public ConditionalAction(Func<bool> condition, Action act)
        {
            Act = act;
            Condition = condition;
        }
    }

    internal class Game
    {
        public SizeD MapSize { get; private set; }
        protected HashSet<IMapObject> Objects { get; set; }
        protected SpecialTimer GameTimer { get; }
        public double CurrentTime => GameTimer.CurrentTime;

        protected bool ShouldExit { get; set; }
        private Action _onExit;

        public Game(double width, double height)
        {
            MapSize = new SizeD(width, height);
            GameTimer = new SpecialTimer();
            BlokingActions = new List<ConditionalAction>();
        }

        public event Action OnExit
        {
            add => _onExit += value;
            // ReSharper disable once DelegateSubtraction
            remove => _onExit -= value;
        }

        public List<ConditionalAction> BlokingActions { get; }

        private void Tic(double dt)
        {
            foreach (var obj in Objects)
            {
                obj.Coordinates += obj.Speed * dt;
                var acceleration = obj.Acceleration;
                obj.Speed += acceleration * dt;
                if (obj.Speed.Length > obj.MaxSpeed)
                    obj.Speed *= obj.MaxSpeed / obj.Speed.Length;
                if (obj is Unit)
                    (obj as Unit).Dir = obj.Speed.GetAngleToXLegacy();
            }

            var toDelete = ResolveCollisions();
            DeleteObjects(toDelete);

            var toAdd = Objects
                .Aggregate<IMapObject, IEnumerable<IMapObject>>(null, (current, obj) =>
                    current?.Concat(obj.GeneratedObjects()) ?? obj.GeneratedObjects());
            Objects.UnionWith(toAdd);
        }

        private void DeleteObjects(IEnumerable<IMapObject> toDelete)
        {
            foreach (var obj in toDelete)
            {
                Objects.Remove(obj);
                Objects.UnionWith(obj.DeleteResult());
            }
        }

        private IEnumerable<IMapObject> ResolveCollisions()
        {
            var chunks = new Dictionary<Point, List<IMapObject>>();
            foreach (var obj in Objects)
            {
                var key = new Point((int) obj.Coordinates.X, (int) obj.Coordinates.Y);
                if (chunks.TryGetValue(key, out List<IMapObject> elem))
                    elem.Add(obj);
                else
                    chunks[key] = new List<IMapObject>() { obj };
            }

            var toDelete = new HashSet<IMapObject>();
            foreach (var pair in chunks)
            {
                var chunk = pair.Value;

                //resolve collisions inside single chunk
                foreach (var i in Enumerable.Range(0, chunk.Count))
                    if (!toDelete.Contains(chunk[i]))
                        foreach (var j in Enumerable
                            .Range(i + 1, chunk.Count - i - 1)
                            .Where(j => !toDelete.Contains(chunk[j])))
                            if (!toDelete.Contains(chunk[i]))
                                toDelete.UnionWith(ResolveCollision(chunk[i], chunk[j]));
                            else
                                break;

                //resolve collisions between neibour chunks
                foreach (var chunkNeighbour in ChunkNeighbours(pair.Key, chunks))
                foreach (var firstObject in chunk)
                    if (!toDelete.Contains(firstObject))
                        foreach (var secondObject in chunkNeighbour
                            .Where(obj => !toDelete.Contains(obj)))
                            if (!toDelete.Contains(firstObject))
                                toDelete.UnionWith(ResolveCollision(firstObject, secondObject));
                            else
                                break;
            }

            return toDelete;
        }

        private static IEnumerable<IMapObject> ResolveCollision(IMapObject firstObject,
            IMapObject secondObject)
        {
            //TODO: do things

            if (firstObject.ShouldBeDeleted())
                yield return firstObject;
            if (secondObject.ShouldBeDeleted())
                yield return secondObject;
        }

        

        private static IEnumerable<List<IMapObject>> ChunkNeighbours(
            Point chunkKey,
            Dictionary<Point, List<IMapObject>> chunks)
        {
            List<IMapObject> res;
            if (chunks.TryGetValue(new Point(chunkKey.X + 1, chunkKey.Y - 1), out res))
                yield return res;
            if (chunks.TryGetValue(new Point(chunkKey.X + 1, chunkKey.Y), out res))
                yield return res;
            if (chunks.TryGetValue(new Point(chunkKey.X + 1, chunkKey.Y + 1), out res))
                yield return res;
            if (chunks.TryGetValue(new Point(chunkKey.X, chunkKey.Y + 1), out res))
                yield return res;
        }

        public void Run()
        {
            GameTimer.Resume();
            while (true)
            {
                if (ShouldExit)
                {
                    GameTimer.Pause();
                    _onExit?.Invoke();
                    break;
                }

                foreach (var blokingAction in BlokingActions)
                    if (blokingAction.Condition())
                    {
                        GameTimer.Pause();
                        blokingAction.Act();
                        GameTimer.Resume();
                    }

                var dt = GameTimer.LastTimeSpan();
                Tic(dt);
            }
        }
    }
}