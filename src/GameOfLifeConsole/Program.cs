using System;
using GameOfLifeNet;
using GameOfLifeNet.Preset;
using Timer = System.Timers.Timer;

namespace GameOfLifeConsole
{
    class Program
    {
        private const int Width = 10;
        private const int Height = 10;
        private const int ConsoleFont = 2;
        private const int RandomFulfillPercent = 30;
        private const double Interval = 100;
        
        static void Main(string[] args)
        {
            try
            {
                ConsoleHelper.SetConsoleFont(ConsoleFont);
                
                var settings = new GameSettings(Width, Height, new RandomPreset(RandomFulfillPercent));
                
                var game = new Game(settings, new ConsoleRender());
                game.Prepare();
                
                var timer = new Timer(Interval);
                timer.Elapsed += (_, e) =>
                {
                    game.MakeNextGeneration();
                };
                
                Console.WriteLine("Press Enter to start");
                Console.ReadLine();

                // timer.Start();
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