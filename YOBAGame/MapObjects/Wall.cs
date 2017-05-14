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

        public override bool ShouldBeDeleted
        {
            get { return false; }
            set { }
        }


        public override IEnumerable<IMapObject> DeleteResult()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override void Decide(double dt, GameState gameState)
        {
        }
    }
}