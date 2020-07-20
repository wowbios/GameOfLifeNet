using System;
using System.Linq;
using GameOfLifeNet;

namespace GameOfLifeConsole
{
    public class ConsoleRender : IRender
    {
        public void Render(GameState state)
        {
            Console.SetCursorPosition(0,0);
            Console.Write(state.Generation);
            foreach (var e in state.Events)
            {
                Console.SetCursorPosition(e.X, e.Y +1);
                Console.Write(e.IsAlive ? "█" : " ");
            }
        }
    }
}