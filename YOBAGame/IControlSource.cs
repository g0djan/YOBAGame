using Archimedes.Geometry;
using Archimedes.Geometry.Units;

namespace YOBAGame
{
    internal interface IControlSource
    {
        Angle Direction { get; }
        Vector2 Speed { get; }
        bool ShouldDropWeapon { get; }
        bool ShouldFire { get; }
        bool ShouldPickUpWeapon { get; }
        bool ShouldWaveSword { get; }
    }
}