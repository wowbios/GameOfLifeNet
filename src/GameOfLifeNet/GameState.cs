using System;

namespace GameOfLifeNet
{
    public readonly struct GameState
    {
        internal GameState(bool[,] field, long generation)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field));
            Generation = generation;
        }
        
        public long Generation { get; }
        
        public bool[,] Field { get; }
    }
}