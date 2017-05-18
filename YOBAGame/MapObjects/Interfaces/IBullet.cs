using YOBAGame.MapObjects.Abstract;

namespace YOBAGame.MapObjects.Interfaces
{
    public interface IBullet : IPhysicalObject
    {
        int Damage { get; }
        AbstractUnit Owner { get; }
    }
}