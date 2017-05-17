using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.GameRules;
using YOBAGame.MapObjects;

namespace YOBAGame
{
    public class UsualBot : AbstractUnit
    {
        private AbstractUnit Target { get; set; }

        public UsualBot(int hitPoints, Weapon weapon, Vector2 coordinates, Circle2 hitBox, IGameRules rules)
            : base(hitPoints, weapon, coordinates, hitBox, rules)
        {
        }

        public override void Decide(double dt, GameState gameState)
        {


            if (Target == null)
                return;

            if (Speed.Length < Rules.BotMinDesieredSpeed)
                ChooseSpeed();

            var toTarget = Target.Coordinates - Coordinates;

            Direction = toTarget.AngleSignedTo(Vector2.UnitX, false);

            if (toTarget.Length < Rules.BotMinShootingDistance && TargetGotVisual && Rando)

        }

        private void ChooseSpeed()
        {
            throw new System.NotImplementedException();
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

        override 
    }
}