using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameOfLife.Abstractions
{
    public interface IGameState
    {
        long Generation { get; }
        
        IChangeEvent[] Events { get; }
    }
}