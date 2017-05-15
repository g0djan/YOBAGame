namespace YOBAGame.MapObjects
{
    public interface IKillableObject : IShootableObject
    {
        int HitPoints { get; }
    }
}