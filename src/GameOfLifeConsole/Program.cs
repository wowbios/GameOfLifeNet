using System;
using GameOfLifeNet;
using GameOfLifeNet.Preset;
using GameOfLifeNet.Ruleset;
using Timer = System.Timers.Timer;

namespace GameOfLifeConsole
{
    class Program
    {
        private const int Width = 10;
        private const int Height = 10;
        private const int ConsoleFont = 2;
        private const int RandomFulfillPercent = 30;
        private const double Interval = 1000;
        
        static void Main(string[] args)
        {
            try
            {
                ConsoleHelper.SetConsoleFont(ConsoleFont);

                Game game = Game.CreateBuilder()
                    .SetSize(Width, Height)
                    .UseConwaysGameOfLife()
                    .RenderWith(new ConsoleRender())
                    .UseBitArrayField()
                    .WithGlider()
                    .Build();

                game.Prepare();
                
                var timer = new Timer(Interval);
                timer.Elapsed += (_, e) =>
                {
                    try
                    {
                        game.MakeNextGeneration();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                        timer.Stop();
                    }
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