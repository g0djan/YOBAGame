using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame
{
    public abstract class StaticObject : IMapObject
    {
        private Vector2 _coordinates;

        protected StaticObject(Vector2 coordinates)
        {
            _coordinates = coordinates;
        }

        public Vector2 Coordinates
        {
            get { return _coordinates; }
            set { }
        }

        public abstract double MaxSpeed { get; set; }

        public Vector2 Speed
        {
            get
            {
                return Vector2.Zero;
            }
            set
            {
            }
        }

        public Vector2 Acceleration()
        {
                return Vector2.Zero;
        }

        public abstract IEnumerable<IMapObject> GeneratedObjects();
        public abstract bool ShouldBeDeleted();
        public abstract IEnumerable<IMapObject> DeleteResult();
    }
}