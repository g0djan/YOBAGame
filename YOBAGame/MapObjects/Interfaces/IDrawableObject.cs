using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace YOBAGame
{
    public interface IDrawableObject : IMapObject
    {
        string ImageFileName { get; }
        Tuple<Bitmap, Point>[][] Images { get; }
        IEnumerable<Tuple<Bitmap, Point>> ForDrawing { get; }
    }
}