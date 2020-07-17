using System;

namespace GameOfLifeNet
{
    public struct GameState
    {
        internal GameState(bool[,] field, long generation)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field));
            Generation = generation;
        }
        
        public long Generation { get; private set; }
        
        public bool[,] Field { get; private set; }
    }
}