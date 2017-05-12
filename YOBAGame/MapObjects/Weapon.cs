using System.Collections.Generic;

namespace YOBAGame.MapObjects
{
    internal abstract class Weapon : MoveableObject, IDrawableObject
    {
        public abstract IEnumerable<IBullet> Fire();
    }
}