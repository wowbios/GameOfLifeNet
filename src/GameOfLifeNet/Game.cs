using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLifeNet
{
    public class Game
    {
        private readonly int _width;
        private readonly int _height;
        private readonly GamePreset _preset;
        private bool[,] _field;

        public Game(int width, int height, GamePreset preset)
        {
            _width = width;
            _height = height;
            _preset = preset;
            _field = new bool[width, height];
        }


        public long Generation { get; private set; }

        public void Prepare()
        {
            switch (_preset)
            {
                case GamePreset.Random:
                    RandomSeed();
                    break;
                case GamePreset.GliderAtTheMiddle:
                    GliderAtTheMiddle();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RandomSeed()
        {
            Console.WriteLine("Random seeding ...");
            const int fullfillPercent = 5;
            var random = new Random();
            int totalCount = _width * _height * fullfillPercent / 100;
            for (int i = 0; i < totalCount; i++)
            {
                Console.Write($"\r              \r{i + 1}/{totalCount}");
                int x = random.Next(0, _width);
                int y = random.Next(0, _height);
                _field[x, y] = true;
            }
            Console.WriteLine();
        }

        private void GliderAtTheMiddle()
        {
            Console.WriteLine("Glider at the middle");
            int i = _width / 2;
            int j = _height / 2;

            _field[i - 1, j] = true;
            _field[i, j + 1] = true;
            _field[i + 1, j - 1] = true;
            _field[i + 1, j] = true;
            _field[i + 1, j + 1] = true;
        }

        public void MakeNextGeneration()
        {
            var newField = new bool[_width, _height];
            Parallel.For(0, _width,
                i =>
                {
                    Parallel.For(0, _height, j =>
                    {
                        Iteration(i, j);
                    });
                });

            _field = newField;
            Generation++;

            void Iteration(int i, int j)
            {
                byte aliveNeighbor = GetClosestAliveCount(i, j);
                bool result = _field[i, j];
                if (_field[i, j])
                {
                    if (aliveNeighbor != 2 && aliveNeighbor != 3)
                    {
                        result = false;
                    }
                }
                else
                {
                    if (aliveNeighbor == 3)
                    {
                        result = true;
                    }
                }
                newField[i, j] = result;
            }
        }

        private byte GetClosestAliveCount(int i, int j)
        {
            //    1_2_3
            // 1| 1 2 3
            // 2| 4 5 6
            // 3| 7 8 9
            byte alive = 0;

            int i1 = (i + _width - 1) % _width;
            int i3 = (i + _width + 1) % _width;
            int j1 = (j + _height - 1) % _height;
            int j3 = (j + _height + 1) % _height;

            Check(i1, j1);
            Check(i1, j);
            Check(i1, j3);

            Check(i, j1);
            Check(i, j3);

            Check(i3, j1);
            Check(i3, j);
            Check(i3, j3);

            return alive;

            void Check(int x, int y) { if (_field[x, y]) alive++; }
        }

        public void OutputToConsole()
        {
            ConsoleColor borderColor = ConsoleColor.Blue;
            ConsoleColor mainColor = ConsoleColor.White;
            const char borderSymbol = '█';
            Console.Clear();
            Console.WriteLine($"Generation {Generation}");
            string horizontalBorder = new string(Enumerable.Repeat(borderSymbol, _width + 2).ToArray());
            Console.ForegroundColor = borderColor;
            Console.WriteLine(horizontalBorder);
            for (int i = 0; i < _width; i++)
            {
                Console.Write(borderSymbol);
                Console.ForegroundColor = mainColor;
                for (int j = 0; j < _height; j++)
                {
                    Console.Write(_field[i, j] ? "█" : " ");
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