using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using Archimedes.Geometry;
using Archimedes.Geometry.Units;
using YOBAGame.Extensions;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public class UsualBullet : AbstractBullet, IDrawableObject
    {
        private readonly Vector2 _speed;
        public string ImageFileName { get; }
        public Tuple<Bitmap, Point>[][] Images { get; }
        public int DrawingPriority { get; }

        IEnumerable<Tuple<Bitmap, Point>> IDrawableObject.ForDrawing
        {
            get
            {
                var relative = Owner.IsRightSide() ? Angle.Zero : Angle.HalfRotation;
                var pic = Images[0][0].Item1.RotateImage((Owner.Direction - relative).Degrees);
                var loc = new Point(
                    (int)Owner.Coordinates.X + Images[0][0].Item2.X,
                    (int)Owner.Coordinates.X + Images[0][0].Item2.X);
                return new[] {Tuple.Create(pic, loc)};
            }
        }

        private readonly int _scaleCoefficient; // TODO: настроить

        public UsualBullet(Vector2 coordinates, Vector2 speed, IShape hitBox, AbstractUnit owner, IGameRules rules, int damage = int.MaxValue)
            : base(hitBox, owner, rules, damage)
        {
            Coordinates = coordinates;
            _speed = speed;
            
            _scaleCoefficient = 5;
            DrawingPriority = 1;
            ImageFileName = "bullet_sprites.png";
            if (ImageParser.Bullet == null)
            {
                var tuple = ImageParser.ParsePicture(ImageFileName, 1)[0][0];
                Images = new []{new[]{Tuple.Create(tuple.Item1.ScaleImage(_scaleCoefficient, 1), tuple.Item2)}};
                ImageParser.Bullet = Images;
            }
            else
            {
                Images = ImageParser.Bullet;
            }
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