using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    internal class Player : AbstractUnit
    {
        public IControlSource Control { get; set; }
        private Sword CarriedSword { get; set; }
        private UsualWeapon CarriedGun { get; set; }

        public override Vector2 Speed { get; set; }

        public override bool SeeksForWeapon { get; protected set; }

        public override Resources Resources { get; }

        protected Player(int hitPoints, UsualWeapon weapon, Sword sword, Vector2 coordinates, Circle2 hitBox,
            IControlSource control, IGameRules rules, Resources resources) 
            : base(hitPoints, weapon, coordinates, hitBox, rules)
        {
            CarriedSword = sword;
            Control = control;
            Resources = resources;
        }

        protected override bool IsMoving()
        {
            return Control.Speed != Vector2.Zero;
        }

        public override void TakeWeapon(AbstractWeapon weapon)
        {
            DropWeapon();
            base.TakeWeapon(weapon);
        }

        private void DropWeapon()
        {
            if (CarriedGun == null)
                return;
            if (WeaponInHand != null && !(WeaponInHand is Sword))
                WeaponInHand = null;
            
            //TODO: should HitBoxes be checked for being Circle2 if they are supposed but obligated to be?
            CarriedGun.Coordinates = Coordinates +
                                     Vector2.FromAngleAndLenght(Direction,
                                         (HitBox as Circle2).Radius + (CarriedGun.HitBox as Circle2).Radius +
                                         double.Epsilon);
            CarriedGun.Speed = Vector2.UnitX.GetRotated(Direction) * Rules.DroppedGunSpeed;
            AddToGenerated(WeaponInHand);
            CarriedGun = null;
        }

        public override void Decide(double dt, GameState gameState)
        {
            // to let gun cool down
            WeaponInHand.Decide(dt, gameState);

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
            if (WeaponInHand != null && !(WeaponInHand is Sword))
                if (CarriedSword != null && CarriedGun.Reloaded && CarriedSword.Reloaded)
                    WeaponInHand = CarriedSword;
            if (WeaponInHand != null)
                AddToGenerated(WeaponInHand.Fire());
        }

        protected void SetSpeedFromControl(Vector2 controlSpeed, double dt)
        {
            if (controlSpeed == Vector2.Zero)
            {
                var speedLength = Speed.Length;
                var dSpeed = Speed * (-Rules.FrictionAcceleration * dt / speedLength);
                if (speedLength > dSpeed.Length + double.Epsilon)
                    Speed += dSpeed;
                else
                    Speed = Vector2.Zero;
            }
            else
                Speed = controlSpeed * (Rules.MaxPlayerSpeed / controlSpeed.Length);
        }

        private void TryFire()
        {
            if (WeaponInHand is Sword)
                if (CarriedGun != null && CarriedSword.Reloaded && CarriedGun.Reloaded)
                    WeaponInHand = CarriedGun;
            if (WeaponInHand != null)
                AddToGenerated(WeaponInHand.Fire());
        }

        public override IEnumerable<IMapObject> DeletionResult()
        {
            //TODO: maybe a player should leave splashes of blood too?
            return Enumerable.Empty<IMapObject>();
        }

        public override bool ShouldBeDeleted { get; set; }
    }
}