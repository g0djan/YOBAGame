using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public abstract class AbstractKillableObject : IKillableObject
    {
        public Vector2 Coordinates { get; set; }
        public abstract Vector2 Speed { get; set; }
        public abstract IEnumerable<IMapObject> GeneratedObjects();
        public abstract IEnumerable<IMapObject> DeleteResult();
        public abstract int HitPoints { get; protected set; }

        protected AbstractKillableObject(Vector2 coordinates)
        {
            Coordinates = coordinates;
        }

        public void TakeDamage(Bullet bullet)
        {
            HitPoints -= bullet.Damage;
        }

        public bool ShouldBeDeleted => HitPoints > 0;
    }
}