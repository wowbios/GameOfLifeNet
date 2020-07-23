using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace GameOfLifeNet
{
    public class BitArrayField : IGameField
    {
        private BitArray[] _old;
        private readonly BitArray[] _new;

        public BitArrayField(int width, int height)
        {
            Width = width;
            Height = height;
            _old = Enumerable.Range(0, width).Select(_ => new BitArray(height)).ToArray();
            _new = Enumerable.Range(0, width).Select(_ => new BitArray(height)).ToArray();
        }

        public int Width { get; }

        public int Height { get; }

        public bool this[int x, int y]
        {
            get => _old[x][y];
            set => _new[x][y] = value;
        }

        public void Swap()
        {
            _old = new BitArray[_new.Length];
            for (var i = 0; i < _new.Length; i++) _old[i] = new BitArray(_new[i]);
        }
    }
}