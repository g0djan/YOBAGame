using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public abstract class MoveableObject : IMapObject
    {
        public abstract Vector2 Coordinates { get; set; }
        public abstract Vector2 Speed { get; set; }
        public abstract IEnumerable<IMapObject> GeneratedObjects();
        public abstract bool ShouldBeDeleted { get; }
        public abstract IEnumerable<IMapObject> DeleteResult();
    }
}