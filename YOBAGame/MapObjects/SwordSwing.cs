using System;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public class SwordSwing : AbstractBullet, IShootableObject, IDrawableObject
    {
        private readonly double _timeToDelete;

        public override Vector2 Coordinates
        {
            get { return Owner.Coordinates; }
            set { }
        }

        public SwordSwing(Circle2 hitBox, IMapObject owner, double timeToDelete, IGameRules rules, int damage = Int32.MaxValue)
            : base(hitBox, owner, rules, damage)
        {
            _timeToDelete = timeToDelete;
        }

        public override Vector2 Speed { get; set; }
        public override bool ShouldBeDeleted { get; set; }

        public override void Decide(double dt, GameState gameState)
        {
            ShouldBeDeleted = gameState.CurrentTime < _timeToDelete;
        }

        public void GetShot(IBullet bullet)
        {
            //TODO: throw bullets back
        }
    }
}