﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Archimedes.Geometry;
using YOBAGame.MapObjects;

namespace YOBAGame
{
    internal class ConditionalAction
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
        public SizeD MapSize { get; }
        protected HashSet<IMapObject> Objects { get; set; }
        public double CurrentTime { get; private set; }

        // TODO:???
        private const double MaxSpeed = 10;


        public Game(double width, double height)
        {
            MapSize = new SizeD(width, height);
            CurrentTime = 0.0;
        }

        private void Step(double dt)
        {
            CurrentTime += dt;

            foreach (var obj in Objects)
                (obj as Unit)?.Decide(dt, CurrentGameState);

            foreach (var obj in Objects)
                obj.Coordinates += obj.Speed * dt;

            var toDelete = ResolveCollisions();
            DeleteObjects(toDelete);

            var toAdd = Objects
                .Aggregate<IMapObject, IEnumerable<IMapObject>>(null, (current, obj) =>
                    current?.Concat(obj.GeneratedObjects()) ?? obj.GeneratedObjects());
            Objects.UnionWith(toAdd);
        }

        public GameState CurrentGameState => new GameState(MapSize, Objects, CurrentTime);

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
                // ReSharper disable once CollectionNeverQueried.Local
                if (chunks.TryGetValue(key, out List<IMapObject> elem))
                    elem.Add(obj);
                else
                    chunks[key] = new List<IMapObject>() {obj};
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
            if (firstObject is IPhysicalObject && secondObject is IPhysicalObject)
            {
                var first = (firstObject as IPhysicalObject);
                var second = (secondObject as IPhysicalObject);
                if (first.HitBox.HasCollision(second.HitBox))
                {
                    if (first is Wall)
                        CollideWithWall(second, first as Wall);
                    else if (second is Wall)
                        CollideWithWall(first, second as Wall);
                    else if (first is AbstractBullet)
                        ShootWithBullet(second, first as AbstractBullet);
                    else if (second is AbstractBullet)
                        ShootWithBullet(first, second as AbstractBullet);
                    else if (first is Unit && second is Weapon)
                        TakeWeaponBy(second as Weapon, first as Unit);
                    else if (second is Unit && second is Weapon)
                        TakeWeaponBy(second as Weapon, first as Unit);
                }
            }

            if (firstObject.ShouldBeDeleted)
                yield return firstObject;
            if (secondObject.ShouldBeDeleted)
                yield return secondObject;
        }

        private static void TakeWeaponBy(Weapon weapon, Unit unit)
        {
            if (unit.SeeksForWeapon)
                unit.TakeWeapon(weapon);
        }

        private static void ShootWithBullet(IPhysicalObject obj, AbstractBullet bullet)
        {
            var unit = obj as Unit;
            if (unit != null)
                unit.TakeDamage(bullet);
            else
                bullet.ShouldBeDeleted = true;
        }

        private static void CollideWithWall(IPhysicalObject obj, Wall wall)
        {
            if (obj is AbstractBullet)
                ((AbstractBullet) obj).ShouldBeDeleted = true;
            else
                // TODO: solve physical collision
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
    }
}