using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.Extensions;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public class Sprite : StaticObject, IDrawableObject
    {
        public string ImageFileName { get; }
        public Tuple<Bitmap, Point>[][] Images { get; }
        public int DrawingPriority { get; }
        private readonly int _numberOfSprite;
        IEnumerable<Tuple<Bitmap, Point>> IDrawableObject.ForDrawing
        {
            get
            {
                return new[] {Tuple.Create(Images[0][_numberOfSprite].Item1, 
                new Point((int)Coordinates.X, (int)Coordinates.Y))};
            }
        }

        public Sprite(Vector2 coordinates, IGameRules rules) : base(coordinates, rules)
        {
            _numberOfSprite =

            DrawingPriority = 0;
            ImageFileName = "sprites.png";
            if (ImageParser.Sprites == null)
            {
                Images = ImageParser.ParsePicture(ImageFileName, 1);
                ImageParser.Sprites = Images;
            }
            else
            {
                Images = ImageParser.Sprites;
            }
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