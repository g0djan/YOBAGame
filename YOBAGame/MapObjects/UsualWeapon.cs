﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOBAGame.GameRules;
using Archimedes.Geometry;
using Archimedes.Geometry.Units;

namespace YOBAGame.MapObjects
{
    public class UsualWeapon : AbstractWeapon
    {
        private int _bulletsNumber;
        public UsualBullet Ammo { get; private set; }

        public int BulletsNumber
        {
            get { return _bulletsNumber; }
            set { _bulletsNumber = value > 0 ? value : 1; }
        }

        public Angle Scatter { get; private set; }
        public override Tuple<Bitmap, Point>[][] Images { get; }
        public override string ImageFileName { get; }

        public UsualWeapon(IShape hitBox, IGameRules rules, UsualBullet bullet, int bulletsNumber,
            Angle scatter) : base(hitBox, rules)
        {
            Ammo = bullet;
            BulletsNumber = bulletsNumber;
            Scatter = scatter;
            ImageFileName = "weapon1_sprites.png";
            Images = Game.pictures[ImageFileName];
        }

        public UsualWeapon(UsualWeapon weapon)
            : this(weapon.HitBox, weapon.Rules, weapon.Ammo, weapon.BulletsNumber, weapon.Scatter)
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
                    yield return new UsualBullet(Ammo, Owner.Coordinates, Vector2.FromAngleAndLenght(Owner.Direction, speed));
                    yield break;
                }
                var anglePiece = Scatter / (BulletsNumber - 1);
                var startAngle = Owner.Direction - Scatter / 2;
                foreach (var i in Enumerable.Range(0, BulletsNumber))
                    yield return new UsualBullet(Ammo, Owner.Coordinates, Vector2.FromAngleAndLenght(startAngle + i * anglePiece, speed));
            }

        }

        protected override double ReloadDuration { get; }
    }
}