using System;
using GameOfLifeNet;
using Timer = System.Timers.Timer;

namespace GameOfLifeConsole
{
    class Program
    {
        private const int Width = 10;
        private const int Height = 10;
        private const int ConsoleFont = 2;
        private const double Interval = 100;
        private const GamePreset Preset = GamePreset.Random;
        
        static void Main(string[] args)
        {
            try
            {
                ConsoleHelper.SetConsoleFont(ConsoleFont);
                
                var game = new Game(Width, Height, Preset);
                game.Prepare();
                game.OutputToConsole();
                
                var timer = new Timer(Interval);
                timer.Elapsed += (_, e) =>
                {
                    game.MakeNextGeneration();
                    game.OutputToConsole();
                };
                
                Console.WriteLine("Press Enter to start");
                Console.ReadLine();

                timer.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            finally
            {
                Console.WriteLine("END");
                Console.ReadLine();
            }
        }
    }
}