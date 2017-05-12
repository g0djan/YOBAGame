using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    internal abstract class Weapon : AbstractPhysicalObject, IDrawableObject
    {
        protected Weapon(IShape hitBox) : base(hitBox)
        {
        }

        public abstract IEnumerable<IBullet> Fire();
        public bool Taken { get; set; }
        public override bool ShouldBeDeleted => Taken;
    }
}