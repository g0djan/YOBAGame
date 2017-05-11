using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Windows.Forms;
using Archimedes.Geometry;

namespace YOBAGame
{
    internal interface IMapObject
    {
        Vector2 Coordinates { get; set; }
        Vector2 Acceleration { get; set; }
        
        double MaxSpeed { get; }
        Vector2 Speed { get; set; }

        void ChangeDirection(Point mouseLocation);
        IEnumerable<IMapObject> GeneratedObjects();

        bool ShouldBeDeleted();
        IEnumerable<IMapObject> DeleteResult();
    }
}