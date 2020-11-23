using GameOfLife.Abstractions;

namespace GameOfLife.CSharp
{
    public readonly struct ChangeEvent : IChangeEvent
    {
        public ChangeEvent(int x, int y, bool isAlive)
        {
            X = x;
            Y = y;
            IsAlive = isAlive;
        }

        public int X { get; }

        public int Y { get; }

        public bool IsAlive { get; }
    }
}