using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;
using YOBAGame.GameRules;
using YOBAGame.MapObjects.Abstract;
using YOBAGame.MapObjects.Interfaces;

namespace YOBAGame.MapObjects
{
    public abstract class AbstractBullet : AbstractPhysicalObject, IBullet
    {
        protected AbstractBullet(IGeometry hitBox, AbstractUnit owner, IGameRules rules, int damage = int.MaxValue)
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
        public AbstractUnit Owner { get; set; }
    }
}