using System.Collections.Generic;
using System.Drawing;
using Archimedes.Geometry;
using YOBAGame.Extensions;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public class UsualBullet : AbstractBullet, IDrawableObject
    {
        private readonly Vector2 _speed;
        public string ImageFileName { get; }
        public int DrawingPriority { get; }

        private readonly int _scaleCoefficient; // TODO: настроитть оба
        private readonly int _bulletNumber;
        public Bitmap bulletImage; //вот здесь неплохо бы избавится от public
        public bool needToScale;

        public IEnumerable<Bitmap> ForDrawing
        {
            get
            {
                if (needToScale)
                {
                    Bitmap[] pictures = PictureParse(ImageFileName);
                    bulletImage = pictures[_bulletNumber].ScaleImage(_scaleCoefficient, 1);
                    needToScale = false;
                }
                return new[]{bulletImage.RotateImage(_speed.GetRadiansVector2Angle())};
            }
        }

        public UsualBullet(Vector2 coordinates, Vector2 speed, IShape hitBox, Unit owner, IGameRules rules, int damage = int.MaxValue)
            : base(hitBox, owner, rules, damage)
        {
            Coordinates = coordinates;
            _speed = speed;

            _bulletNumber = 
            _scaleCoefficient = 5;
            needToScale = true;
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