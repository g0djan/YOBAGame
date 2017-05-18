using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace YOBAGame
{
    public interface IDrawableObject : IMapObject
    {
        Resources Resources { get;}
        IEnumerable<Tuple<Bitmap, Point>> ForDrawing { get; }
    }
}