using System.Security.Cryptography.X509Certificates;

namespace YOBAGame
{
    public interface IShootableObject : IPhysicalObject
    {
        void GetShot(IBullet bullet);
    }
}