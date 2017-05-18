using Archimedes.Geometry;
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
        public Circle2 WeaponDefaultHitBox { get; } = new Circle2(Vector2.Zero, 1);
        public double BotMinDesieredSpeed => 1;
        public double BotMinShootingDistance => 5;
        public double BotShootingProbability => 0.7;
        public double BotBulletSpeed => 5;
        public double PlayerBulletSpeed => 10;
        public double DefaultBulletLength => 2;
        public double WeaponDefaultRadius => 1;
        public double DefaultReloadDuration => 1;
        public double DefaultSwordRadius => 3;
        public int DefaultHP => 1;
        public double DefaultPlayerRadius => 1;
    }
}