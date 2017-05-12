using System;
using System.Collections.Generic;
using System.Drawing;
using Archimedes.Geometry;
using Archimedes.Geometry.Units;

namespace YOBAGame.MapObjects
{
    abstract class Unit : AbstractKillableObject
    {
        //protected Weapon _weapon;
        public Angle Dir;
        public Vector2 Coordinates { get; set; }
        public Vector2 Speed { get; set; }

        public Unit(Vector2 coordinates) : base(coordinates)
        {
            Dir = Angle.HalfRotation;
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
            Dir = Speed.GetAngleToXLegacy();
        }

        public override IEnumerable<IMapObject> GeneratedObjects()
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
    }
}