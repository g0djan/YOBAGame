namespace YOBAGame.MapObjects
{
    public interface IBullet : IPhysicalObject
    {
        int Damage { get; }
        Unit Owner { get; }
    }
}