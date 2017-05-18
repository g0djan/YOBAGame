using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.GameRules;
using YOBAGame.MapObjects.Interfaces;

namespace YOBAGame.MapObjects.Abstract
{
    public abstract class AbstractKillableObject : AbstractPhysicalObject, IKillableObject
    {
        public int Clan { get; set; }
        public override Vector2 Coordinates { get; set; }
        public abstract int HitPoints { get; protected set; }

        protected AbstractKillableObject(Vector2 coordinates, Circle2 hitBox, IGameRules rules)
            : base(hitBox, rules)
        {
            Coordinates = coordinates;
            Clan = 0;
        }

        public void GetShot(IBullet bullet)
        {
            if (bullet.Owner.Clan == Clan)
                return;
            HitPoints -= bullet.Damage;
            bullet.ShouldBeDeleted = true;
        }

        public override bool ShouldBeDeleted
        {
            get { return HitPoints > 0; }
            set { }
        }
    }
}