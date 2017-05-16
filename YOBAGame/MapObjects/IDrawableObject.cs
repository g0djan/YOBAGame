using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace YOBAGame
{
    public interface IDrawableObject : IMapObject
    {
        string ImageFileName { get; }
        int DrawingPriority { get; }
        IEnumerable<Bitmap> ForDrawing { get; }
    }
}