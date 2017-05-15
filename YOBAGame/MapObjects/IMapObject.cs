using System.Collections.Generic;
using Archimedes.Geometry;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public interface IMapObject
    {
        IGameRules Rules { get; }

        Vector2 Coordinates { get; set; }
        Vector2 Speed { get; set; }

        void Decide(double dt, GameState gameState);
        
        IEnumerable<IMapObject> GeneratedObjects();

        bool ShouldBeDeleted { get; set; }
        IEnumerable<IMapObject> DeletionResult();
    }
}