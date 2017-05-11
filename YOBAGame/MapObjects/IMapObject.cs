using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public interface IMapObject
    {
        Vector2 Coordinates { get; set; }

        Vector2 Speed { get; set; }
        
        IEnumerable<IMapObject> GeneratedObjects();

        bool ShouldBeDeleted { get; }
        IEnumerable<IMapObject> DeleteResult();
    }
}