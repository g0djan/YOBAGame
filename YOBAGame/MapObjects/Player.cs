using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    internal class Player : Unit
    {
        private IControlSource Control { get; }
        private List<IMapObject> ObjectsToGenerate { get; set; }

        public override Vector2 Speed { get; set; }

        public override bool SeeksForWeapon { get; protected set; }

        protected Player(int hitPoints, Weapon weapon, Vector2 coordinates, Circle2 hitBox,
            IControlSource control, IGameRules rules) : base(hitPoints, weapon, coordinates, hitBox, rules)
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
            var res = ObjectsToGenerate ?? Enumerable.Empty<IMapObject>();
            ObjectsToGenerate = null;
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
            if (ObjectsToGenerate == null)
                ObjectsToGenerate = new List<IMapObject>() {obj};
            else
                ObjectsToGenerate.Add(obj);
        }

        private void AddToGenerated(IEnumerable<IBullet> enumerable)
        {
            if (ObjectsToGenerate == null)
                ObjectsToGenerate = new List<IMapObject>(enumerable);
            else
                ObjectsToGenerate.AddRange(enumerable);
        }

        private void DropWeapon()
        {
            if (Gun == null)
                return;
            Gun.Coordinates = Coordinates;
            Gun.Speed = Vector2.UnitX.GetRotated(Direction) * Rules.DroppedGunSpeed; //TODO: make settings and set default dropped weapon speed
            AddToGenerated(Gun);
            Gun = null;
        }

        public override void Decide(double dt, GameState gameState)
        {
            // to let gun cool down
            Gun.Decide(dt, gameState);

            Direction = Control.Direction;
            SetSpeedFromControl(Control.Speed, dt);

            if (Control.ShouldFire)
                TryFire();
            if (Control.ShouldWaveSword)
                TryWaveSword();

            if (Control.ShouldDropWeapon)
                DropWeapon();
            SeeksForWeapon = Control.ShouldPickUpWeapon;
        }

        private void TryWaveSword()
        {
            throw new System.NotImplementedException();
        }

        protected void SetSpeedFromControl(Vector2 controlSpeed, double dt)
        {
            var newSpeed = double.IsPositiveInfinity(Rules.PlayerAcceleration) ? controlSpeed : (Speed + controlSpeed * (Rules.PlayerAcceleration / controlSpeed.Length));
            if (newSpeed.Length > Rules.MaxPlayerSpeed)
                newSpeed *= Rules.MaxPlayerSpeed / newSpeed.Length;
            Speed = newSpeed;
        }

        private void TryFire()
        {
            if (Gun == null)
                return;
            AddToGenerated(Gun.Fire());
        }

        public override IEnumerable<IMapObject> DeletionResult()
        {
            //TODO: maybe a player should leave splashes of blood too?
            return Enumerable.Empty<IMapObject>();
        }

        public override bool ShouldBeDeleted { get; set; }
    }
}