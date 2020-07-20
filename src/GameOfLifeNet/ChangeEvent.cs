namespace GameOfLifeNet
{
    public readonly struct ChangeEvent
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