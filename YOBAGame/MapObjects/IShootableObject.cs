using System.Security.Cryptography.X509Certificates;

namespace YOBAGame.MapObjects
{
    public interface IShootableObject : IPhysicalObject
    {
        void GetShot(IBullet bullet);
    }
}