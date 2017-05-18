using System.Collections.Generic;
using Archimedes.Geometry;

namespace YOBAGame
{
    public struct GameState
    {
        public GameState(SizeD mapSize, IEnumerable<IMapObject> objects, double currentTime)
        {
            MapSize = mapSize;
            Objects = objects;
            CurrentTime = currentTime;
        }

        public SizeD MapSize { get; }

        public IEnumerable<IMapObject> Objects { get; }

        public double CurrentTime { get; }
    }
}