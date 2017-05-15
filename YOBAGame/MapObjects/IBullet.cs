namespace YOBAGame.MapObjects
{
    public interface IBullet : IPhysicalObject
    {
        int Damage { get; }
        IMapObject Owner { get; }
    }
}