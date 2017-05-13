using System;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;

namespace YOBAGame.MapObjects
{
    public class SwordSwing : AbstractBullet
    {
        private readonly double _timeToDelete;

        public override Vector2 Coordinates
        {
            get { return Owner.Coordinates; }
            set { }
        }

        public SwordSwing(Circle2 hitBox, IMapObject owner, double timeToDelete, int damage = Int32.MaxValue)
            : base(hitBox, owner, damage)
        {
            _timeToDelete = timeToDelete;
        }

        public override Vector2 Speed { get; set; }
        public override bool ShouldBeDeleted { get; set; }

        public override void Decide(double dt, GameState gameState)
        {
            ShouldBeDeleted = gameState.CurrentTime < _timeToDelete;
        }
    }
}