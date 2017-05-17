using Archimedes.Geometry.Primitives;

namespace YOBAGame.GameRules
{
    public interface IGameRules
    {
        double DroppedGunSpeed { get; }
        double FrictionAcceleration { get; }
        double PlayerAcceleration { get; }
        double MaxPlayerSpeed { get; }
        double SwordReloadDuration { get; }
        double SwordSwingRadius { get; }
        double SwordSwingLifeTime { get; }
        Circle2 WeaponDefaultHitBox { get; }
        double BotMinDesieredSpeed { get; }
        double BotMinShootingDistance { get; }
    }
}