namespace YOBAGame.MapObjects
{
    public interface IBullet : IPhysicalObject
    {
        int Damage { get; }
        AbstractUnit Owner { get; }
    }
}