using System;
using System.Linq;
using GameOfLifeNet;

namespace GameOfLifeConsole
{
    public class ConsoleRender : IRender
    {
        public void Render(GameState state)
        {
            int width = state.Field.GetLength(0);
            int height = state.Field.GetLength(1);

            ConsoleColor borderColor = ConsoleColor.Blue;
            ConsoleColor mainColor = ConsoleColor.White;
            const char borderSymbol = '█';
            Console.Clear();
            Console.WriteLine($"Generation {state.Generation}");
            string horizontalBorder = new string(Enumerable.Repeat(borderSymbol, width + 2).ToArray());
            Console.ForegroundColor = borderColor;
            Console.WriteLine(horizontalBorder);
            for (int i = 0; i < width; i++)
            {
                Console.Write(borderSymbol);
                Console.ForegroundColor = mainColor;
                for (int j = 0; j < height; j++)
                {
                    Console.Write(state.Field[i, j] ? "█" : " ");
                }
                Console.ForegroundColor = borderColor;
                Console.Write(borderSymbol);
                Console.WriteLine();
            }
            Console.WriteLine(horizontalBorder);

            Console.ForegroundColor = mainColor;
        }
    }
}