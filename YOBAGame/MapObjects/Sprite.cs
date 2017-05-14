using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public class Sprite : StaticObject, IDrawableObject
    {
        public Sprite(Vector2 coordinates) : base(coordinates)
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

        public override void Decide(GameState gameState)
        {
        }
    }
}