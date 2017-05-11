using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;

namespace YOBAGame
{
    public class Bullet : MovableObject
    {
        private readonly Vector2 _speed;

        public sealed override Vector2 Coordinates { get; set; }

        public override double MaxSpeed
        {
            get { return Speed.Length; }
            set
            {
            }
        }

        public override Vector2 Speed
        {
            get { return _speed; }
            set { }
        }

        public override Vector2 Acceleration()
        {
            return Vector2.Zero;
        }

        public Bullet(Vector2 coordinates, Vector2 speed)
        {
            _speed = speed;
            Coordinates = coordinates;
        }

        public override IEnumerable<IMapObject> GeneratedObjects()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override bool ShouldBeDeleted()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<IMapObject> DeleteResult()
        {
            return Enumerable.Empty<IMapObject>();
        }
    }
}