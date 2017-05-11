using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Archimedes.Geometry;
using Archimedes.Geometry.Units;

namespace YOBAGame
{
    abstract class Unit : IMapObject
    {
        //protected Weapon _weapon;
        private Angle dir;
        public Vector2 Coordinates { get; set; }
        public Vector2 Acceleration { get; set; }
        public double MaxSpeed { get; }
        public Vector2 Speed { get; set; }

        public Unit(Vector2 coordinates)
        {
            dir = Angle.HalfRotation;
            Coordinates = coordinates;
            //MaxSpeed =
            Speed = Vector2.Zero;
        }

        public void ChangeDirection(Point mouse)
        {
            double dx = mouse.X - Coordinates.X;
            double dy = mouse.Y - Coordinates.Y;
            Angle newDirection = Angle.FromRadians(
                Math.Atan2(dy, dx));
            Speed = Speed.GetRotated(newDirection);
            dir = dir + Angle.FromRadians(Math.Atan2(dy, dx));
        }
        
        public void ChangeAcceleration(Keys key)
        {
            const int addedSpeed = 1;
            switch (key)
            {
                case Keys.Up:
                    Acceleration += Vector2.FromAngleAndLenght(Speed.GetAngleToXLegacy(), 1);
                    break;
                case Keys.Down:
                    Acceleration -= Vector2.FromAngleAndLenght(Speed.GetAngleToXLegacy(), addedSpeed);
                    break;
                case Keys.Left:
                    Acceleration += Speed.Normalize().GetRotated(Angle.HalfRotation);
                    break;
                case Keys.Right:
                    Acceleration -= Speed.Normalize().GetRotated(Angle.HalfRotation);
                    break;
                case Keys.None:
                    Acceleration = Vector2.Zero;
                    break;
            }
            Acceleration = addedSpeed * Acceleration.Normalize();
            dir = Speed.GetAngleToXLegacy();
        }

        public IEnumerable<IMapObject> GeneratedObjects()
        {
            const int bulletCount = 5;
            var bullets = new Bullet[bulletCount];
            var rotateAngle = Angle.Zero;
            var bulletSpeed = 5 * Speed.Length;
            for (var i = 0; i < bulletCount; i++)
                bullets[i] = new Bullet(Coordinates, (bulletSpeed * Speed.Normalize())
                    .GetRotated(rotateAngle));
            return bullets;
        }

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