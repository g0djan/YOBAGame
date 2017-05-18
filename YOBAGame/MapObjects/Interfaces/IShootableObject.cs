namespace YOBAGame.MapObjects.Interfaces
{
    public interface IShootableObject : IPhysicalObject
    {
        void GetShot(IBullet bullet);
    }
}