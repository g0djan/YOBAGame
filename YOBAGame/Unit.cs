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
        public Angle Dir;
        public Vector2 Coordinates { get; set; }
        public Vector2 Acceleration { get; set; }
        public double MaxSpeed { get; }
        public Vector2 Speed { get; set; }

        public Unit(Vector2 coordinates)
        {
            Dir = Angle.HalfRotation;
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
            Dir = Dir + Angle.FromRadians(Math.Atan2(dy, dx));
        }
        
        public void ChangeAcceleration(Vector2 force)
        {
            const int forceModule = 1;
            Acceleration += force;
            Acceleration = forceModule * Acceleration.Normalize();
            Dir = Speed.GetAngleToXLegacy();
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