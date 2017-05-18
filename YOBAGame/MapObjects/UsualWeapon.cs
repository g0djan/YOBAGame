using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOBAGame.GameRules;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    class UsualWeapon : AbstractWeapon
    {
        private UsualBullet Ammo;
        public override Tuple<Bitmap, Point>[][] Images { get; }
        public override string ImageFileName { get; }

        public UsualWeapon(IShape hitBox, IGameRules rules, UsualBullet bullet) : base(hitBox, rules)
        {
            Ammo = bullet;
            ImageFileName = "weapon1_sprites.png";
            Images = Game.pictures[ImageFileName];
        }

        public override Vector2 Coordinates { get; set; }
        public override Vector2 Speed { get; set; }
        public override IEnumerable<IMapObject> GeneratedObjects()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IMapObject> DeletionResult()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<IBullet> FiredBullets { get; }
        protected override double ReloadDuration { get; }
    }
}
