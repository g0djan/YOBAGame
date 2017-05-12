using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public class Wall : AbstractStaticPhysicalObject, IDrawableObject
    {
        public Wall(Vector2 coordinates, IShape hitBox) : base(coordinates, hitBox.ToPolygon2())
        {
        }

        public override IEnumerable<IMapObject> GeneratedObjects()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override bool ShouldBeDeleted => false;
        public override IEnumerable<IMapObject> DeleteResult()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public sealed override IShape HitBox => base.HitBox;
    }
}