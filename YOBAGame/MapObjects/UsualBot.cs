using System;
using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using Archimedes.Geometry.Units;
using YOBAGame.GameRules;
using YOBAGame.MapObjects.Abstract;

namespace YOBAGame.MapObjects
{
    public class UsualBot : AbstractUnit
    {
        private static readonly Random rnd = new Random();
        private AbstractUnit Target { get; set; }
        public override Resources Resources { get; }

        public UsualBot(int hitPoints, UsualWeapon weapon, Vector2 coordinates,
            Circle2 hitBox, Resources resources, IGameRules rules)
            : base(hitPoints, weapon, coordinates, hitBox, rules)
        {
            Resources = resources;
            ObjectsToGenerate = new List<IMapObject>();
        }

        public UsualBot(UsualBot bot, Vector2 coordinates) : base(bot.HitPoints,
            new UsualWeapon((UsualWeapon) bot.WeaponInHand), coordinates, bot.HitBox as Circle2, bot.Rules)
        {
            Clan = bot.Clan;
        }

        protected override bool IsMoving()
        {
            return Speed != Vector2.Zero;
        }

        public override void Decide(double dt, GameState gameState)
        {
            if (Target == null)
                return;

            var gotTarVis = TargetGotVisual(gameState);

            if (Speed.Length < Rules.BotMinDesieredSpeed)
            {
                if (gotTarVis)
                {
                    var vect =
                        Vector2.FromAngleAndLenght(Direction, rnd.NextDouble() * Rules.MaxPlayerSpeed) +
                        Vector2.FromAngleAndLenght(Angle.FullRotation * rnd.NextDouble(),
                            Rules.MaxPlayerSpeed);
                    vect *= Rules.MaxPlayerSpeed / vect.Length;
                    Speed = vect;
                }
                else
                {
                    var vect = Vector2.FromAngleAndLenght(Angle.FullRotation * rnd.NextDouble(),
                        Rules.MaxPlayerSpeed);
                    vect *= Rules.MaxPlayerSpeed / vect.Length;
                    Speed = vect;
                }
            }
            else
                Speed += Speed * (-Rules.FrictionAcceleration / Speed.Length);

            var toTarget = Target.Coordinates - Coordinates;

            Direction = toTarget.AngleSignedTo(Vector2.UnitX, false);

            if (WeaponInHand.Reloaded && toTarget.Length < Rules.BotMinShootingDistance && gotTarVis &&
                rnd.NextDouble() < Rules.BotShootingProbability)
                AddToGenerated(WeaponInHand.Fire());
        }

        private bool TargetGotVisual(GameState gameState)
        {
            var segm = new LineSegment2(Coordinates, Target.Coordinates);
            return gameState.Objects.All(obj => !(obj is Wall) || ((Wall) obj).HitBox.HasCollision(segm));
        }

        public override IEnumerable<IMapObject> DeletionResult()
        {
            //TODO: leave blood splashes after death
            return Enumerable.Empty<IMapObject>();
        }

        public override bool SeeksForWeapon
        {
            get { return false; }
            protected set { }
        }
    }
}