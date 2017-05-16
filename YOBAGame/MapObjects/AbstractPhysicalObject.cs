using System;
using System.Collections.Generic;
using Archimedes.Geometry;
using YOBAGame.GameRules;

namespace YOBAGame
{
    public abstract class AbstractPhysicalObject : IPhysicalObject
    {
        public IGameRules Rules { get; }
        public abstract Vector2 Coordinates { get; set; }
        public abstract Vector2 Speed { get; set; }
        public abstract IEnumerable<IMapObject> GeneratedObjects();
        public abstract bool ShouldBeDeleted { get; set; }
        public abstract IEnumerable<IMapObject> DeletionResult();

        public virtual IShape HitBox { get; }

        protected AbstractPhysicalObject(IShape hitBox, IGameRules rules)
        {
            HitBox = hitBox ?? throw new ArgumentNullException();
            Rules = rules;
        }

        public abstract void Decide(double dt, GameState gameState);
    }
}