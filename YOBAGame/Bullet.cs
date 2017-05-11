using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Archimedes.Geometry;

namespace YOBAGame
{
    class Bullet : IMapObject
    {
        public Vector2 Coordinates { get; set; }
        public double MaxSpeed { get; }
        public Vector2 Speed { get; set; }

        public Bullet(Vector2 coordinates, Vector2 speed)
        {
            //MaxSpeed = 
            Coordinates = coordinates;
            Speed = speed;
        }

        public Vector2 ChangeDirection(Point mouseLocation)
        {
            throw new NotImplementedException();
        }

        public Vector2 Acceleration(Keys key) => Speed;

        public IEnumerable<IMapObject> GeneratedObjects() => null;

        public bool ShouldBeDeleted()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMapObject> DeleteResult()
        {
            throw new NotImplementedException();
        }
    }
}
