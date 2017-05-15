using Archimedes.Geometry;
using YOBAGame.GameRules;
using YOBAGame.MapObjects;

namespace YOBAGame
{
    internal interface IGame
    {
        IGameRules Rules { get; }
        SizeD MapSize { get; }
        double CurrentTime { get; }
        GameState CurrentGameState { get; }
        void Step(double dt);
        void AddObject(IMapObject obj);
    }
}