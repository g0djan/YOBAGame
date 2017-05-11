using YOBAGame.MapObjects;

namespace YOBAGame
{
    public interface IBullet : IMapObject
    {
        int Damage { get; }
    }
}