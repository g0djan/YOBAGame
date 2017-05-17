namespace YOBAGame.MapObjects
{
    public interface IShootableObject : IPhysicalObject
    {
        void GetShot(IBullet bullet);
    }
}