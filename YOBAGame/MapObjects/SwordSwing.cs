using System;
using System.Collections.Generic;
using System.Drawing;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public class SwordSwing : AbstractBullet, IShootableObject, IDrawableObject
    {
        private readonly double _timeToDelete;
        public Resources Resources { get; }

        private int part;
        private int itteration;
        public IEnumerable<Tuple<Bitmap, Point>> ForDrawing
        {
            get
            {
                if (Owner.IsRightSide() && part == 0)
                {
                    part = 1;
                    itteration = 0;
                }
                else if (!Owner.IsRightSide() && part == 1)
                {
                    part = 0;
                    itteration = 0;
                }
                var pic = Resources.Images[part][itteration].Item1;
                return new[] {Tuple.Create(pic, new Point((int)Coordinates.X, (int)Coordinates.Y))};
            }
        }


        public override Vector2 Coordinates
        {
            get { return Owner.Coordinates; }
            set { }
        }

        public SwordSwing(Circle2 hitBox, AbstractUnit owner, double timeToDelete, IGameRules rules, Resources resources,
            int damage = Int32.MaxValue)
            : base(hitBox, owner, rules, damage)
        {
            _timeToDelete = timeToDelete;

            part = 0;
            itteration = 0;
            Resources = resources;
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