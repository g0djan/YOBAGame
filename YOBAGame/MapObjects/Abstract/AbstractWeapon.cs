using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Archimedes.Geometry;
using YOBAGame.GameRules;
using YOBAGame.MapObjects.Interfaces;

namespace YOBAGame.MapObjects.Abstract
{
    public abstract class AbstractWeapon : AbstractPhysicalObject, IDrawableObject
    {
        private double _timeToReload;
        public AbstractUnit Owner { get; set; }
        public bool Taken { get; set; }
        public bool Reloaded => TimeToReload < double.Epsilon;
        
        public virtual Resources Resources { get; }

        public IEnumerable<Tuple<Bitmap, Point>> ForDrawing
        {
            get
            {
                var pic = Resources.Images[2][0].Item1;
                var loc = new Point(
                    (int)Owner.Coordinates.X + Resources.Images[2][0].Item2.X,
                    (int)Owner.Coordinates.Y + Resources.Images[2][0].Item2.Y);
                return new[] {Tuple.Create(pic, loc)};
            }
        }

        protected AbstractWeapon(IGeometry hitBox, IGameRules rules) : base(hitBox, rules)
        {
            Owner = null;
            
        }

        public override bool ShouldBeDeleted
        {
            get { return Taken; }
            set { }
        }

        

        protected double TimeToReload
        {
            get { return _timeToReload; }
            set { _timeToReload = value >= 0 ? value : 0; }
        }

        protected abstract IEnumerable<IBullet> FiredBullets { get; }
        protected abstract double ReloadDuration { get; } //TODO: не проинициализирвоана

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
                Speed += Speed * (-Rules.FrictionAcceleration * dt / Speed.Length);
            else
                Speed = Vector2.Zero;
        }
    }
}