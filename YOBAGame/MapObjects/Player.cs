using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame.MapObjects
{
    class Player : AbstractKillableObject
    {
        public Player(Vector2 coordinates) : base(coordinates)
        {
        }

        public override Vector2 Speed { get; set; }
        public override IEnumerable<IMapObject> GeneratedObjects()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<IMapObject> DeleteResult()
        {
            throw new System.NotImplementedException();
        }

        public override int HitPoints { get; protected set; }
    }
}