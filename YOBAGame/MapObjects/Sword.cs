using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public class Sword : AbstractWeapon
    {
        public override Resources Resources { get; }
        private SwordSwing _swing;

        public Sword(Circle2 hitBox, IGameRules rules, Resources resources, SwordSwing swing) : base(hitBox, rules)
        {
            Resources = resources;
            _swing = swing;
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

        protected override IEnumerable<IBullet> FiredBullets => new IBullet[] {_swing};
    }
}