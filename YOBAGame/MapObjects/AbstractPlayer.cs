using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using Archimedes.Geometry.Units;

namespace YOBAGame.MapObjects
{
    internal abstract class AbstractPlayer : Unit
    {
        private IControlSource Control { get; }
        private List<IMapObject> objectsToGenerate;

        public override Vector2 Speed { get; set; }

        public override bool SeeksForWeapon
        {
            get { throw new NotImplementedException(); }
        }

        protected AbstractPlayer(int hitPoints, Weapon weapon, Vector2 coordinates, Circle2 hitBox,
            IControlSource control) : base(hitPoints, weapon, coordinates, hitBox)
        {
            Control = control;
        }

        /*public void ChangeDirection(Point mouse)
        {
            double dx = mouse.X - Coordinates.X;
            double dy = mouse.Y - Coordinates.Y;
            Angle newDirection = Angle.FromRadians(
                Math.Atan2(dy, dx));
            Speed = Speed.GetRotated(newDirection);
            Dir = Dir + Angle.FromRadians(Math.Atan2(dy, dx));
        }*/

        /*public void ChangeAcceleration(Vector2 force)
        {
            const int forceModule = 1;
            Dir = Speed.GetAngleToXLegacy();
        }*/

        public override IEnumerable<IMapObject> GeneratedObjects()
        {
            var res = objectsToGenerate ?? Enumerable.Empty<IMapObject>();
            objectsToGenerate = null;
            return res;
            /*const int bulletCount = 5;
            var bullets = new AbstractBullet[bulletCount];
            var rotateAngle = Angle.Zero;
            var bulletSpeed = 5 * Speed.Length;
            for (var i = 0; i < bulletCount; i++)
                bullets[i] = new AbstractBullet(Coordinates, (bulletSpeed * Speed.Normalize())
                    .GetRotated(rotateAngle));
            return bullets;*/
        }

        public override void TakeWeapon(Weapon weapon)
        {
            DropWeapon();
            base.TakeWeapon(weapon);
        }

        private void AddToGenerated(IMapObject obj)
        {
            if (objectsToGenerate == null)
                objectsToGenerate = new List<IMapObject>() {obj};
            else
                objectsToGenerate.Add(obj);
        }

        private void AddToGenerated(IEnumerable<IBullet> enumerable)
        {
            if (objectsToGenerate == null)
                objectsToGenerate = new List<IMapObject>(enumerable);
            else
                objectsToGenerate.AddRange(enumerable);
        }

        private void DropWeapon()
        {
            if (Gun == null)
                return;
            Gun.Coordinates = Coordinates;
            Gun.Speed =; //TODO: make settings and set default dropped weapon speed
            AddToGenerated(Gun);
            Gun = null;
        }

        public override void Decide(double dt, GameState gameState)
        {
            Direction = Control.Direction;
            Speed = Control.Speed;
            if (Control.ShouldDropWeapon)
                DropWeapon();
            if (Control.ShouldFire)
                TryFire();
            // TODO: Aidar stoped here
        }

        private void TryFire()
        {
            if (Gun == null)
                return;
            AddToGenerated(Gun.Fire());
        }
    }
}