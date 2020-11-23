using System;
using GameOfLife.Abstractions;
// using GameOfLife.CSharp;
using GameOfLife.FSharp;
using GameOfLife.FSharp.Game;
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

                // Game game = Game.CreateBuilder()
                //     .SetSize(Width, Height)
                //     .UseConwaysGameOfLife()
                //     .RenderWith(new ConsoleRender())
                //     .WithGlider()
                //     .Build();
                
                var game = new Game(new GameSettings(
                    Width,
                    Height, 
                    new Preset.StickPreset(),
                    new ConsoleRender(),
                    new Ruleset.ConwaysRuleset()));

                var g = game as IGame;
                g.Prepare();

                var timer = new Timer(Interval);
                timer.Elapsed += (_, e) =>
                {
                    try
                    {
                        g.MakeNextGeneration();
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