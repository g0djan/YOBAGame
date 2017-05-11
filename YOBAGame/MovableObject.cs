using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame
{
    public abstract class MovableObject : IMapObject
    {
        public abstract Vector2 Coordinates { get; set; }
        public abstract double MaxSpeed { get; set; }
        public abstract Vector2 Speed { get; set; }
        public abstract Vector2 Acceleration();
        public abstract IEnumerable<IMapObject> GeneratedObjects();
        public abstract bool ShouldBeDeleted();
        public abstract IEnumerable<IMapObject> DeleteResult();
    }
}