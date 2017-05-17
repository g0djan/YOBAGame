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
        protected Angle Direction { get; set; }
        public override Vector2 Speed { get; set; }
        public override int HitPoints { get; protected set; }

        public virtual string ImageFileName { get; }
        public int DrawingPriority { get; }

        private int Itteration { get; set; }

        public virtual IEnumerable<Bitmap> ForDrawing
        {
            get
            {
                //number of needed picture evaluates from itteration and height of image here
                var pictures = ImageParser.ParsePicture(ImageFileName);
                var imageHeight = pictures.Length / 2;
                var changeDir = WasChangedDirection(imageHeight);
                var changeMove = BeganOrStopedMove(imageHeight);
                if (changeDir || changeMove)
                    Itteration = -1;
                IncrementItteration(imageHeight);
                if (IsRightSide())
                    Itteration += imageHeight;
                var forDrawing = new List<Bitmap>();
                forDrawing.Add(pictures[Itteration]);

                if (WeaponInHand != null)
                {
                    var weaponPictureNumber = IsRightSide() ? 1 : 0;
                    var weaponPictures = ImageParser.ParsePicture(WeaponInHand.ImageFileName);
                    forDrawing.Add(weaponPictures[weaponPictureNumber].RotateImage(Direction.Radians));
                }
                return forDrawing;
            }
        }

        private void IncrementItteration(int imageHeight)
        {
            int addedPart = Speed == Vector2.Zero ? imageHeight / 2 : 0;
            Itteration = (Itteration + 1) % (imageHeight / 2);
            Itteration += addedPart;
        }

        public bool IsRightSide()
        {
            return Direction >= -Angle.HalfRotation && Direction <= Angle.HalfRotation;
        }

        private bool WasChangedDirection(int imageHeight)
        {
            return Itteration < imageHeight && IsRightSide() ||
                Itteration > imageHeight && !IsRightSide();
        }

        private bool BeganOrStopedMove(int imageHeight)
        {
            return (Itteration < imageHeight / 2 || Itteration > imageHeight && Itteration < 3 * imageHeight / 2) &&
                   Speed == Vector2.Zero ||
                   (Itteration > imageHeight / 2 && Itteration < imageHeight  || Itteration > 3 * imageHeight / 2) &&
                   Speed != Vector2.Zero;
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

            Itteration = -1;
            DrawingPriority = 2;
            ImageFileName =
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
    }
}