namespace YOBAGame.MapObjects
{
    public interface IDrawableObject : IMapObject
    {
        string[] ImagesFileNames { get; }
        int DrawingPriority { get; }
    }
}