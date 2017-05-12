using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public abstract class AbstractStaticPhysicalObject : AbstractPhysicalObject
    {
        private readonly Vector2 _coordinates;

        protected AbstractStaticPhysicalObject(Vector2 coordinates, IShape hitBox) : base(hitBox)
        {
            _coordinates = coordinates;
        }

        public sealed override Vector2 Coordinates
        {
            get { return _coordinates; }
            set { }
        }

        public sealed override Vector2 Speed
        {
            get
            {
                return Vector2.Zero;
            }
            set
            {
            }
        }
    }
}