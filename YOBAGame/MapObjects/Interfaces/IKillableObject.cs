namespace YOBAGame.MapObjects.Interfaces
{
    public interface IKillableObject : IShootableObject
    {
        int HitPoints { get; }
    }
}