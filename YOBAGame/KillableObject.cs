using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame
{
    public interface IKillableObject : IMapObject
    {
        int HitPoints { get; }
        void TakeDamage(Bullet bullet);
    }
}