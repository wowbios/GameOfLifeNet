namespace GameOfLifeNet
{
    public class BoolField : IGameField
    {
        private bool[,] _old;
        private bool[,] _new;

        public BoolField(int width, int height)
        {
            Width = width;
            Height = height;
            _new = new bool[width, height];
            _old = new bool[width, height];
        }

        public int Width { get; }

        public int Height { get; }

        public bool this[int x, int y]
        {
            get => _old[x, y];
            set => _new[x, y] = value;
        }

        public void Swap()
        {
            _old = (bool[,])_new.Clone();
        }
    }
}