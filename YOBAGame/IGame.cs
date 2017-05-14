using Archimedes.Geometry;
using YOBAGame.GameRules;

namespace YOBAGame
{
    internal interface IGame
    {
        IGameRules Rules { get; }
        SizeD MapSize { get; }
        double CurrentTime { get; }
        GameState CurrentGameState { get; }
        void Step(double dt);
    }
}