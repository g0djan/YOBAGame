using System;
using System.Collections.Generic;
using System.Drawing;

namespace YOBAGame
{
    internal interface IMapObject
    {
        PointF Coordinates { get; set; }
        
        double MaxSpeed { get; set; }
        PointF Speed { get; set; }

        PointF Acceleration(Game game);
        IEnumerable<IMapObject> GeneratedObjects();

        bool ShouldBeDeleted();
        IEnumerable<IMapObject> DeleteResult();
    }
}