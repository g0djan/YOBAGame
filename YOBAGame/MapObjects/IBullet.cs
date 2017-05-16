namespace YOBAGame
{
    public interface IBullet : IPhysicalObject
    {
        int Damage { get; }
        Unit Owner { get; }
    }
}