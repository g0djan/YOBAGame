using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;
using Archimedes.Geometry;
using YOBAGame.Extensions;
using YOBAGame.GameRules;

namespace YOBAGame
{
    public class UsualBullet : AbstractBullet, IDrawableObject
    {
        private readonly Vector2 _speed;
        public string ImageFileName { get; }
        public int DrawingPriority { get; }

        private readonly int _scaleCoefficient; // TODO: настроитть оба
        private readonly int _bulletNumber;
        private Bitmap _bulletImage;
        private bool _needToScale;

        public IEnumerable<Bitmap> ForDrawing
        {
            get
            {
                if (_needToScale)
                {
                    var pictures = ImageParser.ParsePicture(ImageFileName);
                    _bulletImage = pictures[_bulletNumber].ScaleImage(_scaleCoefficient, 1);
                    _needToScale = false;
                }
                return new[]{_bulletImage.RotateImage(_speed.GetRadiansVector2Angle())};
            }
        }

        public UsualBullet(Vector2 coordinates, Vector2 speed, IShape hitBox, Unit owner, IGameRules rules, int damage = int.MaxValue)
            : base(hitBox, owner, rules, damage)
        {
            Coordinates = coordinates;
            _speed = speed;

            _bulletNumber = 
            _scaleCoefficient = 5;
            _needToScale = true;
            DrawingPriority = 1;
            ImageFileName =
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