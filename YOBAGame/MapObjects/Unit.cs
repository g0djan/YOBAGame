using System;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using Archimedes.Geometry.Units;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    internal abstract class Unit : AbstractKillableObject
    {
        public Weapon Gun { get; protected set; }
        public Angle Direction { get; protected set; }
        public override Vector2 Speed { get; set; }
        public override int HitPoints { get; protected set; }

        public abstract bool SeeksForWeapon { get; protected set; }

        protected Unit(int hitPoints, Weapon weapon,Vector2 coordinates, Circle2 hitBox, IGameRules rules) : base(hitBox, rules)
        {
            HitPoints = hitPoints;
            Direction = default(Angle);
            Speed = Vector2.Zero;
            if (weapon != null)
                TakeWeapon(weapon);
        }

        public virtual void TakeWeapon(Weapon weapon)
        {
            Gun = weapon;
            Gun.Owner = this;
            Gun.Taken = true;
        }
    }
}