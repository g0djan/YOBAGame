using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;
using YOBAGame.GameRules;

namespace YOBAGame
{
    public abstract class AbstractBullet : AbstractPhysicalObject, IBullet
    {
        protected AbstractBullet(IShape hitBox, Unit owner, IGameRules rules, int damage = int.MaxValue)
            : base(hitBox, rules)
        {
            Damage = damage;
            Owner = owner;
        }

        public override IEnumerable<IMapObject> GeneratedObjects()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override IEnumerable<IMapObject> DeletionResult()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public int Damage { get; }
        public Unit Owner { get; }
    }
}