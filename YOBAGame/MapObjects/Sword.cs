using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public class Sword : Weapon
    {
        public override string ImageFileName { get; }
        public override Tuple<Bitmap, Point>[][] Images { get; }

        public Sword(Circle2 hitBox, IGameRules rules) : base(hitBox, rules)
        {
            ImageFileName = "sword_sprites.png";
            Images = Game.pictures[ImageFileName];
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

        protected override double ReloadDuration => Rules.SwordReloadDuration;

        protected override IEnumerable<IBullet> FiredBullets
            =>
                new IBullet[]
                {
                    new SwordSwing(new Circle2(Vector2.Zero, Rules.SwordSwingRadius), Owner,
                        Rules.SwordSwingLifeTime, Rules)
                };
    }
}