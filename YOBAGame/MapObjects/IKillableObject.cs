namespace YOBAGame.MapObjects
{
    public interface IKillableObject : IMapObject
    {
        int HitPoints { get; }
        void TakeDamage(Bullet bullet);
    }
}