using Archimedes.Geometry;

namespace YOBAGame
{
    public interface IPhysicalObject : IMapObject
    {
        IShape HitBox { get; }
    }
}