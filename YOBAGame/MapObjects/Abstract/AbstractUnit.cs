using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using Archimedes.Geometry.Units;
using YOBAGame.Extensions;
using YOBAGame.GameRules;
using YOBAGame.MapObjects.Interfaces;

namespace YOBAGame.MapObjects.Abstract
{
    public abstract class AbstractUnit : AbstractKillableObject, IDrawableObject
    {
        protected List<IMapObject> ObjectsToGenerate { get; set; }
        protected AbstractWeapon WeaponInHand { get; set; }
        public Angle Direction { get; protected set; }
        public override Vector2 Speed { get; set; }
        public override int HitPoints { get; protected set; }

        public virtual Resources Resources { get; }

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
                var pic = Resources.Images[_part][_itteration].Item1;
                var loc = new Point(
                    (int)Coordinates.X + Resources.Images[_part][_itteration].Item2.X,
                    (int)Coordinates.Y + Resources.Images[_part][_itteration].Item2.Y);
                var unitImage = Tuple.Create(pic, loc);
                _itteration = (_itteration + 1) % Resources.Images[0].Length;
                var sequence = new List<Tuple<Bitmap, Point>>();
                sequence.Add(unitImage);
                if (WeaponInHand != null)
                {
                    _part = IsRightSide() ? 1 : 0;
                    var relative = IsRightSide() ? Angle.Zero : Angle.HalfRotation;
                    pic = WeaponInHand.Resources.Images[_part][0].Item1.RotateImage((relative - Direction).Radians);
                    var displacedLoc = WeaponInHand.Resources.Images[_part][0].Item2.RotatePoint((relative - Direction).Radians);
                    loc = new Point(
                        (int)Coordinates.X - displacedLoc.X,
                        (int)Coordinates.Y - displacedLoc.Y);
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
            return Direction >= -Angle.HalfRotation / 2 && Direction <= Angle.HalfRotation / 2;
        }

        protected abstract bool IsMoving();

        public abstract bool SeeksForWeapon { get; protected set; }

        protected AbstractUnit(int hitPoints, AbstractWeapon weapon, Vector2 coordinates, 
            Circle2 hitBox, IGameRules rules)
            : base(coordinates, hitBox, rules)
        {
            HitPoints = hitPoints;
            Direction = default(Angle);
            Speed = Vector2.Zero;
            if (weapon != null)
            {
                weapon.Owner = this;
                if (weapon is UsualWeapon)
                {
                    var usualWeapon = weapon as UsualWeapon;
                    usualWeapon.Ammo.Owner = this;
                    weapon = usualWeapon;
                }
                TakeWeapon(weapon);
            }

            _part = 0;
            _itteration = 0;
        }

        public virtual void TakeWeapon(AbstractWeapon weapon)
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