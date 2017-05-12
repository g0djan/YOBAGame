using System.Collections.Generic;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;

namespace YOBAGame.MapObjects
{
    public abstract class AbstractKillableObject : AbstractPhysicalObject, IKillableObject
    {
        public override Vector2 Coordinates { get; set; }
        public abstract int HitPoints { get; protected set; }

        protected AbstractKillableObject(Circle2 hitBox) : base(hitBox)
        {
        }

        public void TakeDamage(Bullet bullet)
        {
            HitPoints -= bullet.Damage;
        }

        public override bool ShouldBeDeleted => HitPoints > 0;
    }
}