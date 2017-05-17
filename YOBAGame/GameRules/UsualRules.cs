using Archimedes.Geometry.Primitives;

namespace YOBAGame.GameRules
{
    public class UsualRules : IGameRules
    {
        public static UsualRules Default = new UsualRules();

        public double DroppedGunSpeed => 1;
        public double FrictionAcceleration => 1;
        public double PlayerAcceleration => 1;
        public double MaxPlayerSpeed => 4;
        public double SwordReloadDuration => 1;
        public double SwordSwingRadius => 3;
        public double SwordSwingLifeTime => 0.5;
        public Circle2 WeaponDefaultHitBox { get; }
    }
}