using System;
using GameOfLife.CSharp;
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

                Game game = SetupGame();
                game.Prepare();
                
                Timer timer = SetupTimer(game);

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

        private static Game SetupGame()
        {
            return Game.CreateBuilder()
                .SetSize(Width, Height)
                .UseConwaysGameOfLife()
                .RenderWith(new ConsoleRender())
                .WithGlider()
                .Build();
        }

        private static Timer SetupTimer(Game game)
        {
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
            return timer;
        }
    }
}