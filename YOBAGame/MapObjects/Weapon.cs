using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Archimedes.Geometry;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public abstract class Weapon : AbstractPhysicalObject, IDrawableObject
    {
        private double _timeToReload;
        public AbstractUnit Owner { get; set; }
        public bool Taken { get; set; }
        public bool Reloaded => TimeToReload < double.Epsilon;

        public string ImageFileName { get; }
        public Tuple<Bitmap, Point>[][] Images { get; }
        public int DrawingPriority { get; }
        public IEnumerable<Tuple<Bitmap, Point>> ForDrawing
        {
            get
            {
                var pic = Images[2][0].Item1;
                var loc = new Point(
                    (int)Owner.Coordinates.X + Images[2][0].Item2.X,
                    (int)Owner.Coordinates.Y + Images[2][0].Item2.Y);
                return new[] {Tuple.Create(pic, loc)};
            }
        }

        protected Weapon(IShape hitBox, IGameRules rules) : base(hitBox, rules)
        {
            Owner = null;
            DrawingPriority = 3;
            ImageFileName = Owner is Player ? "weapon_player.png" : "weapon_enemy.png";
            if (ImageParser.PlayerGun == null && Owner is Player)
            {
                Images = ImageParser.ParsePicture(ImageFileName, 3);
                ImageParser.PlayerGun = Images;
            }
            else if (ImageParser.EnemyGun == null && Owner is UsualBot)
            {
                Images = ImageParser.ParsePicture(ImageFileName, 3);
                ImageParser.EnemyGun = Images;
            }
            else if (Owner is Player)
            {
                Images = ImageParser.PlayerGun;
            }
            else if (Owner is UsualBot)
            {
                Images = ImageParser.EnemyGun;
            }
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