using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Archimedes.Geometry;
using YOBAGame.GameRules;

namespace YOBAGame
{
    public abstract class Weapon : AbstractPhysicalObject, IDrawableObject
    {
        private double _timeToReload;
        public Unit Owner { get; set; }
        public bool Taken { get; set; }
        public bool Reloaded => TimeToReload < double.Epsilon;

        public int DrawingPriority { get; }
        public string ImageFileName { get; }

        public IEnumerable<Bitmap> ForDrawing
        {
            get
            {
                Bitmap[] pictures = PictureParse(ImageFileName);
                if (!Taken)
                    return new[] {pictures[2]};
                if (Owner.IsRightSide())
                    return new[] {pictures[1]};
                return new[] {pictures[0]};
            }
        }

        protected Weapon(IShape hitBox, IGameRules rules) : base(hitBox, rules)
        {
            Owner = null;
            DrawingPriority = 3;
            ImageFileName =
        }

        public override bool ShouldBeDeleted
        {
            get => Taken;
            set { }
        }

        protected abstract double ReloadDuration { get; }

        protected double TimeToReload
        {
            get => _timeToReload;
            set => _timeToReload = value >= 0 ? value : 0;
        }

        protected abstract IEnumerable<IBullet> FiredBullets { get; }

        public IEnumerable<IBullet> Fire()
        {
            if (TimeToReload > double.Epsilon)
                return Enumerable.Empty<IBullet>();

            TimeToReload = ReloadDuration;
            return FiredBullets;
        }

        public override void Decide(double dt, GameState gameState)
        {
            TimeToReload -= dt;

            if (Taken)
                return;

            if (Speed == Vector2.Zero) return;
            if (Speed.Length > double.Epsilon)
                Speed -= Speed * (1 * Rules.FrictionAcceleration * dt / Speed.Length);
            else
                Speed = Vector2.Zero;
        }
    }
}