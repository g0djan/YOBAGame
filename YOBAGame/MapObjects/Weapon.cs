using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public abstract class Weapon : AbstractPhysicalObject, IDrawableObject
    {
        private double _timeToReload;
        public Unit Owner { get; set; }
        public bool Taken { get; set; }
        public bool Reloaded => TimeToReload < double.Epsilon;

        public override bool ShouldBeDeleted
        {
            get { return Taken; }
            set { }
        }

        protected abstract double ReloadDuration { get; }

        protected double TimeToReload
        {
            get { return _timeToReload; }
            set { _timeToReload = value >= 0 ? value : 0; }
        }

        protected abstract IEnumerable<IBullet> FiredBullets { get; }

        protected Weapon(IShape hitBox, IGameRules rules) : base(hitBox, rules)
        {
            Owner = null;
        }

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