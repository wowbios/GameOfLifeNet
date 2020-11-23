using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GameOfLife.Abstractions;

namespace GameOfLife.CSharp
{
    public readonly struct GameState : IGameState
    {
        internal GameState(long generation, IEnumerable<ChangeEvent> events)
        {
            _ = events ?? throw new ArgumentNullException(nameof(events));

            Events = events.Cast<IChangeEvent>().ToArray();
            Generation = generation;
        }
        
        public long Generation { get; }
        
        public IChangeEvent[] Events { get; }
    }
}