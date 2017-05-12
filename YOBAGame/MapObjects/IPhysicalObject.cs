using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    public interface IPhysicalObject : IMapObject
    {
        IShape HitBox { get; }
    }
}