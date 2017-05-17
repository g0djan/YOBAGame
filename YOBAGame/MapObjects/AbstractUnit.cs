using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using Archimedes.Geometry.Units;
using YOBAGame.Extensions;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public abstract class AbstractUnit : AbstractKillableObject, IDrawableObject
    {
        protected List<IMapObject> ObjectsToGenerate { get; set; }
        protected Weapon WeaponInHand { get; set; }
        public Angle Direction { get; protected set; }
        public override Vector2 Speed { get; set; }
        public override int HitPoints { get; protected set; }

        public virtual string ImageFileName { get; protected set; }
        public Tuple<Bitmap, Point>[][] Images { get; protected set; }
        public int DrawingPriority { get; }

        private int _part;
        private int _itteration;
        public virtual IEnumerable<Tuple<Bitmap, Point>> ForDrawing
        {
            get
            {
                if (_part != ChoosePartOfImage())
                {
                    _part = ChoosePartOfImage();
                    _itteration = 0;
                }
                var pic = Images[_part][_itteration].Item1;
                var loc = new Point(
                    (int)Coordinates.X + Images[_part][_itteration].Item2.X,
                    (int)Coordinates.Y + Images[_part][_itteration].Item2.Y);
                var unitImage = Tuple.Create(pic, loc);
                _itteration = (_itteration + 1) % Images[0].Length;
                var sequence = new List<Tuple<Bitmap, Point>>();
                sequence.Add(unitImage);
                if (WeaponInHand != null)
                {
                    _part = IsRightSide() ? 1 : 0;
                    var relative = IsRightSide() ? Angle.Zero : Angle.HalfRotation;
                    pic = WeaponInHand.Images[_part][0].Item1.RotateImage((Direction - relative).Radians);
                    loc = new Point(
                        (int)Coordinates.X + WeaponInHand.Images[_part][0].Item2.X,
                        (int)Coordinates.Y + WeaponInHand.Images[_part][0].Item2.Y);
                    sequence.Add(Tuple.Create(pic, loc));
                }
                return sequence;
            }
        }

        int ChoosePartOfImage()
        {
            if (IsMoving() && !IsRightSide())
                return 0;
            if (!IsMoving() && !IsRightSide())
                return 1;
            if (IsMoving() && IsRightSide())
                return 2;
            return 3;
        }

        public bool IsRightSide()
        {
            return Direction >= -Angle.HalfRotation && Direction <= Angle.HalfRotation;
        }

        public bool IsMoving()
        {
            return Speed != Vector2.Zero;
        }

        public abstract bool SeeksForWeapon { get; protected set; }

        protected AbstractUnit(int hitPoints, Weapon weapon, Vector2 coordinates, Circle2 hitBox, IGameRules rules)
            : base(coordinates, hitBox, rules)
        {
            HitPoints = hitPoints;
            Direction = default(Angle);
            Speed = Vector2.Zero;
            if (weapon != null)
                TakeWeapon(weapon);

            _part = 0;
            _itteration = 0;
            DrawingPriority = 2;
        }

        public virtual void TakeWeapon(Weapon weapon)
        {
            WeaponInHand = weapon;
            WeaponInHand.Owner = this;
            WeaponInHand.Taken = true;
        }

        public sealed override IEnumerable<IMapObject> GeneratedObjects()
        {
            var res = ObjectsToGenerate ?? Enumerable.Empty<IMapObject>();
            ObjectsToGenerate = null;
            return res;
        }

        protected void AddToGenerated(IMapObject obj)
        {
            if (ObjectsToGenerate == null)
                ObjectsToGenerate = new List<IMapObject>() { obj };
            else
                ObjectsToGenerate.Add(obj);
        }

        protected void AddToGenerated(IEnumerable<IBullet> enumerable)
        {
            if (ObjectsToGenerate == null)
                ObjectsToGenerate = new List<IMapObject>(enumerable);
            else
                ObjectsToGenerate.AddRange(enumerable);
        }
    }
}