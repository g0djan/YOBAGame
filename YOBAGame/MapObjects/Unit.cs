using System;
using System.Collections.Generic;
using System.Drawing;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using Archimedes.Geometry.Units;

namespace YOBAGame.MapObjects
{
    internal abstract class Unit : AbstractKillableObject
    {
        private Weapon _weapon;
        public Angle Dir;
        public override Vector2 Speed { get; set; }
        public abstract bool SeeksForWeapon { get; }

        protected Unit(Vector2 coordinates, Circle2 hitBox, Weapon weapon) : base(hitBox)
        {
            Dir = default(Angle);
            Speed = Vector2.Zero;
            TakeWeapon(weapon);
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
            var bullets = new AbstractBullet[bulletCount];
            var rotateAngle = Angle.Zero;
            var bulletSpeed = 5 * Speed.Length;
            for (var i = 0; i < bulletCount; i++)
                bullets[i] = new AbstractBullet(Coordinates, (bulletSpeed * Speed.Normalize())
                    .GetRotated(rotateAngle));
            return bullets;
        }

        public abstract void Decide(double d, GameState gameState);

        public void TakeWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _weapon.Taken = true;
        }
    }
}