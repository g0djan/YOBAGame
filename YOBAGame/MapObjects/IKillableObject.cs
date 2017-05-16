namespace YOBAGame
{
    public interface IKillableObject : IShootableObject
    {
        int HitPoints { get; }
    }
}