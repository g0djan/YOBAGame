using System;
using System.Collections.Generic;
using System.Drawing;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using Archimedes.Geometry.Units;

namespace YOBAGame.MapObjects
{
    internal abstract class Unit : AbstractKillableObject
    {
        public Weapon Gun { get; protected set; }
        public Angle Direction { get; protected set; }
        public override Vector2 Speed { get; set; }
        public override int HitPoints { get; protected set; }

        public abstract bool SeeksForWeapon { get; }

        protected Unit(int hitPoints, Weapon weapon,Vector2 coordinates, Circle2 hitBox) : base(hitBox)
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