using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public abstract class AbstractBullet : AbstractPhysicalObject, IBullet, IDrawableObject
    {
        protected AbstractBullet(IShape hitBox, IMapObject owner, int damage = int.MaxValue)
            : base(hitBox)
        {
            Damage = damage;
            Owner = owner;
        }

        public override IEnumerable<IMapObject> GeneratedObjects()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override IEnumerable<IMapObject> DeleteResult()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public int Damage { get; }
        public IMapObject Owner { get; }
    }
}