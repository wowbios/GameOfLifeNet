using System;
using GameOfLife.Abstractions;
using GameOfLife.CSharp;

namespace GameOfLifeConsole
{
    public class ConsoleRender : IRender
    {
        public void Render(IGameState state)
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