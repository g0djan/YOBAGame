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
    }
}