namespace YOBAGame.MapObjects
{
    public interface IBullet : IMapObject
    {
        int Damage { get; }
        IMapObject Owner { get; }
    }
}