using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame
{
    public interface IMapObject
    {
        Vector2 Coordinates { get; set; }
        
        double MaxSpeed { get; set; }
        Vector2 Speed { get; set; }

        Vector2 Acceleration();
        IEnumerable<IMapObject> GeneratedObjects();

        bool ShouldBeDeleted();
        IEnumerable<IMapObject> DeleteResult();
    }
}