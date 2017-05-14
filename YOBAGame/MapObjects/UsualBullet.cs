using System;
using System.Windows.Forms;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public class UsualBullet : AbstractBullet
    {
        private readonly Vector2 _speed;

        public UsualBullet(Vector2 coordinates, Vector2 speed, IShape hitBox, IMapObject owner, int damage = int.MaxValue)
            : base(hitBox, owner, damage)
        {
            Coordinates = coordinates;
            _speed = speed;
        }

        public override bool ShouldBeDeleted { get; set; }

        public override Vector2 Speed
        {
            get { return _speed; }
            set { }
        }

        public override Vector2 Coordinates { get; set; }
        public override void Decide(double dt, GameState gameState)
        {
        }
    }
}