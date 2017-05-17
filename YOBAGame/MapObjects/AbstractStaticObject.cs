using System.Collections.Generic;
using Archimedes.Geometry;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public abstract class StaticObject : IMapObject
    {
        private readonly Vector2 _coordinates;

        public IGameRules Rules { get; }

        protected StaticObject(Vector2 coordinates, IGameRules rules)
        {
            _coordinates = coordinates;
            Rules = rules;
        }

        public Vector2 Coordinates
        {
            get => _coordinates;
            set { }
        }

        public Vector2 Speed
        {
            get => Vector2.Zero;
            set { } 
        }

        public abstract IEnumerable<IMapObject> GeneratedObjects();
        public abstract bool ShouldBeDeleted { get; set; }
        public abstract IEnumerable<IMapObject> DeletionResult();
        public abstract void Decide(double dt, GameState gameState);
    }
}