using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using YOBAGame.GameRules;
using Archimedes.Geometry;
using Archimedes.Geometry.Units;

namespace YOBAGame.MapObjects
{
    public class UsualWeapon : AbstractWeapon
    {
        private int _bulletsNumber;
        private UsualBullet Ammo { get; }

        private int BulletsNumber
        {
            get { return _bulletsNumber; }
            set { _bulletsNumber = value > 0 ? value : 1; }
        }

        private Angle Scatter { get; }
        public override Resources Resources { get; }

        public UsualWeapon(IShape hitBox, IGameRules rules, double reloadDuration, UsualBullet bullet,
            int bulletsNumber, Resources resources,
            Angle scatter) : base(hitBox, rules)
        {
            Ammo = bullet;
            BulletsNumber = bulletsNumber;
            Scatter = scatter;
            ReloadDuration = reloadDuration;
            Resources = resources;
        }

        public UsualWeapon(UsualWeapon weapon)
            : this(
                weapon.HitBox, weapon.Rules, weapon.ReloadDuration, weapon.Ammo, weapon.BulletsNumber,
                weapon.Resources, weapon.Scatter)
        {
        }

        public override Vector2 Coordinates { get; set; }
        public override Vector2 Speed { get; set; }

        public override IEnumerable<IMapObject> GeneratedObjects()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override IEnumerable<IMapObject> DeletionResult()
        {
            return Enumerable.Empty<IMapObject>();
        }

        protected override IEnumerable<IBullet> FiredBullets
        {
            get
            {
                var speed = (Owner is Player) ? Rules.PlayerBulletSpeed : Rules.BotBulletSpeed;
                if (BulletsNumber == 1)
                {
                    yield return
                        new UsualBullet(Ammo, Owner.Coordinates,
                            Vector2.FromAngleAndLenght(Owner.Direction, speed));
                    yield break;
                }
                var anglePiece = Scatter / (BulletsNumber - 1);
                var startAngle = Owner.Direction - Scatter / 2;
                foreach (var i in Enumerable.Range(0, BulletsNumber))
                    yield return
                        new UsualBullet(Ammo, Owner.Coordinates,
                            Vector2.FromAngleAndLenght(startAngle + i * anglePiece, speed));
            }
        }

        protected override double ReloadDuration { get; }
    }
}