using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public class Bullet : IBullet
    {
        private readonly Vector2 _speed;

        public Vector2 Coordinates { get; set; }

        public Vector2 Speed
        {
            get { return _speed; }
            set { }
        }

        public Bullet(Vector2 coordinates, Vector2 speed, int damage = int.MaxValue)
        {
            _speed = speed;
            Damage = damage;
            Coordinates = coordinates;
            ShouldBeDeleted = false;
        }

        public IEnumerable<IMapObject> GeneratedObjects()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public bool ShouldBeDeleted { get; private set; }

        public IEnumerable<IMapObject> DeleteResult()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public int Damage { get; }
    }
}