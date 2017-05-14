using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public class Sprite : StaticObject, IDrawableObject
    {
        public Sprite(Vector2 coordinates, IGameRules rules) : base(coordinates, rules)
        {
        }

        public override IEnumerable<IMapObject> GeneratedObjects()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override bool ShouldBeDeleted { get; set; }

        public override IEnumerable<IMapObject> DeleteResult()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override void Decide(double dt, GameState gameState)
        {
        }
    }
}