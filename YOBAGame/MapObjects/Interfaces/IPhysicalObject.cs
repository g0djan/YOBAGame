using Archimedes.Geometry;

namespace YOBAGame.MapObjects.Interfaces
{
    public interface IPhysicalObject : IMapObject
    {
        IGeometry HitBox { get; }
    }
}