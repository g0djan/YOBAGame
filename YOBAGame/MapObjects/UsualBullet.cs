using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using Archimedes.Geometry.Units;
using YOBAGame.Extensions;
using YOBAGame.GameRules;
using YOBAGame.MapObjects.Abstract;

namespace YOBAGame.MapObjects
{
    public class UsualBullet : AbstractBullet, IDrawableObject
    {
        private readonly Vector2 _speed;
        public Resources Resources { get; }

        IEnumerable<Tuple<Bitmap, Point>> IDrawableObject.ForDrawing
        {
            get
            {
                var relative = Owner.IsRightSide() ? Angle.Zero : Angle.HalfRotation;
                var pic = Resources.Images[0][0].Item1.RotateImage((Owner.Direction - relative).Degrees);
                var loc = new Point(
                    (int) Owner.Coordinates.X + Resources.Images[0][0].Item2.X,
                    (int) Owner.Coordinates.X + Resources.Images[0][0].Item2.X);
                return new[] {Tuple.Create(pic, loc)};
            }
        }

        public UsualBullet(Vector2 coordinates, Vector2 speed, double length, AbstractUnit owner,
            IGameRules rules, Resources resources, int damage = int.MaxValue)
            : base(
                new LineSegment2(
                    Vector2.FromAngleAndLenght(speed.AngleSignedTo(Vector2.UnitX, false), length),
                    Vector2.FromAngleAndLenght(
                        speed.AngleSignedTo(Vector2.UnitX, false) + Angle.HalfRotation, length)), owner, rules,
                damage)
        {
            Coordinates = coordinates;
            _speed = speed;
            Resources = resources;
            Length = length;
        }

        public UsualBullet(UsualBullet bullet, Vector2 coordinates, Vector2 speed)
            : this(
                coordinates, speed, bullet.Length, bullet.Owner, bullet.Rules, bullet.Resources, bullet.Damage
            )
        {
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

        private double Length { get; }
    }
}