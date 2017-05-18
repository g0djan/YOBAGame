using System;
using System.Collections.Generic;
using Archimedes.Geometry;
using YOBAGame.GameRules;
using YOBAGame.MapObjects.Interfaces;

namespace YOBAGame.MapObjects.Abstract
{
    public abstract class AbstractPhysicalObject : IPhysicalObject
    {
        public IGameRules Rules { get; }
        public abstract Vector2 Coordinates { get; set; }
        public abstract Vector2 Speed { get; set; }
        public abstract IEnumerable<IMapObject> GeneratedObjects();
        public abstract bool ShouldBeDeleted { get; set; }
        public abstract IEnumerable<IMapObject> DeletionResult();

        public virtual IGeometry HitBox { get; }

        protected AbstractPhysicalObject(IGeometry hitBox, IGameRules rules)
        {
            if (hitBox == null)
                throw new ArgumentNullException();
            HitBox = hitBox;
            Rules = rules;
        }

        public abstract void Decide(double dt, GameState gameState);
    }
}