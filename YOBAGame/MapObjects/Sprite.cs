using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public class Sprite : StaticObject, IDrawableObject
    {
        public string ImageFileName { get; }
        public int DrawingPriority { get; }
        private readonly int _numberOfSprite;

        public IEnumerable<Bitmap> ForDrawing
        {
            get
            {
                var pictures = ImageParser.ParsePicture(ImageFileName);
                return new []{pictures[_numberOfSprite]};
            }
        }

        public Sprite(Vector2 coordinates, IGameRules rules) : base(coordinates, rules)
        {
            _numberOfSprite =

            DrawingPriority = 0;
            ImageFileName = 
        }

        public override IEnumerable<IMapObject> GeneratedObjects()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override bool ShouldBeDeleted { get; set; }

        public override IEnumerable<IMapObject> DeletionResult()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override void Decide(double dt, GameState gameState)
        {
        }
    }
}