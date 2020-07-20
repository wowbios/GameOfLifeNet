using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLifeNet
{
    public readonly struct GameState
    {
        internal GameState(long generation, IEnumerable<ChangeEvent> events)
        {
            _ = events ?? throw new ArgumentNullException(nameof(events));

            Events = events.ToArray();
            Generation = generation;
        }
        
        public long Generation { get; }
        
        public IReadOnlyCollection<ChangeEvent> Events { get; }
    }
}