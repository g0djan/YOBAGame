using System;
using System.Collections.Generic;
using System.Drawing;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.GameRules;

namespace YOBAGame
{
    public class SwordSwing : AbstractBullet, IShootableObject, IDrawableObject
    {
        private readonly double _timeToDelete;
        public string ImageFileName { get; }
        public int DrawingPriority { get; }

        public int Itteration { get; set; }

        IEnumerable<Bitmap> IDrawableObject.ForDrawing
        {
            get
            {
                Bitmap[] pictures = PictureParse(ImageFileName);
                var imageHeight = pictures.Length / 2;
                var dirChange = WasChangedDirection(imageHeight);
                if (dirChange)
                    Itteration = -1;
                Itteration = (Itteration + 1) % imageHeight;
                if (Owner.IsRightSide() && Itteration == 0)
                    Itteration += imageHeight;
                return new[] {pictures[Itteration]};
            }
        }

        bool WasChangedDirection(int imageHeight)
        {
            return Itteration < imageHeight && Owner.IsRightSide() ||
                   Itteration > imageHeight && !Owner.IsRightSide();
        }

        public override Vector2 Coordinates
        {
            get => Owner.Coordinates;
            set { }
        }

        public SwordSwing(Circle2 hitBox, Unit owner, double timeToDelete, IGameRules rules, int damage = Int32.MaxValue)
            : base(hitBox, owner, rules, damage)
        {
            Itteration = 0;
            _timeToDelete = timeToDelete;
            DrawingPriority = 1;
            ImageFileName = 
        }

        public override Vector2 Speed { get; set; }
        public override bool ShouldBeDeleted { get; set; }

        public override void Decide(double dt, GameState gameState)
        {
            ShouldBeDeleted = gameState.CurrentTime < _timeToDelete;
        }

        public void GetShot(IBullet bullet)
        {
            var circleCenter = Coordinates;
            var B = bullet.Coordinates;
            var OB = (B - circleCenter).Normalize();
            bullet.Speed -= OB * (OB.DotProduct(bullet.Speed) * 2);
            // TODO: should HitBox be rotated? what is bullet's HitBox?
        }
    }
}