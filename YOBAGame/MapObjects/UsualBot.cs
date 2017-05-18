using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using Archimedes.Geometry.Units;
using YOBAGame.GameRules;
using YOBAGame.MapObjects;

namespace YOBAGame
{
    public class UsualBot : AbstractUnit
    {
        private Random rnd = new Random();
        private AbstractUnit Target { get; set; }

        public UsualBot(int hitPoints, Weapon weapon, Vector2 coordinates, Circle2 hitBox, IGameRules rules)
            : base(hitPoints, weapon, coordinates, hitBox, rules)
        {
            ImageFileName = "enemy1_sprites.png";
            Images = Game.pictures[ImageFileName];
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

            var toTarget = Target.Coordinates - Coordinates;

            Direction = toTarget.AngleSignedTo(Vector2.UnitX, false);

            if (toTarget.Length < Rules.BotMinShootingDistance && gotTarVis &&
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