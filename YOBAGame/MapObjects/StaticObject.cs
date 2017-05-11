using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public abstract class StaticObject : IMapObject
    {
        private readonly Vector2 _coordinates;

        protected StaticObject(Vector2 coordinates)
        {
            _coordinates = coordinates;
        }

        public Vector2 Coordinates
        {
            get { return _coordinates; }
            set { }
        }

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

        public abstract IEnumerable<IMapObject> GeneratedObjects();
        public abstract bool ShouldBeDeleted { get; }
        public abstract IEnumerable<IMapObject> DeleteResult();
    }
}