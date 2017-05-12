using System;
using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public abstract class AbstractPhysicalObject : IPhysicalObject
    {
        public abstract Vector2 Coordinates { get; set; }
        public abstract Vector2 Speed { get; set; }
        public abstract IEnumerable<IMapObject> GeneratedObjects();
        public abstract bool ShouldBeDeleted { get; set; }
        public abstract IEnumerable<IMapObject> DeleteResult();

        public virtual IShape HitBox { get; }

        protected AbstractPhysicalObject(IShape hitBox)
        {
            if (hitBox == null)
                throw new ArgumentNullException();
            HitBox = hitBox;
        }

        public abstract void Decide(GameState gameState);
    }
}