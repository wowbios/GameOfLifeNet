using System;

namespace GameOfLifeNet.Preset
{
    public class RandomAreas : IPreset
    {
        private readonly int _areaSize;
        private readonly int _areasCount;
        private readonly IPreset _preset;

        public RandomAreas(int areaSize, int areasCount, IPreset preset)
        {
            _areaSize = areaSize;
            _areasCount = areasCount;
            _preset = preset;
        }

        public void InitializeField(bool[,] field)
        {
            var width = field.GetLength(0);
            var height = field.GetLength(1);

            var areaWidth = width * _areaSize / 100;
            var areaHeight = height * _areaSize / 100;

            var rnd = new Random(Environment.TickCount);
            for (int i = 0; i < _areasCount; i++)
            {
                var area = new bool[areaWidth, areaHeight];
                _preset.InitializeField(area);
                var wIndex = rnd.Next(width);
                var hIndex = rnd.Next(height);
                for (int wCounter = 0; wCounter < areaWidth; wCounter++)
                {
                    for (int hCounter = 0; hCounter < areaHeight; hCounter++)
                    {
                        int w = (wIndex + wCounter) % width;
                        int h = (hIndex + hCounter) % height;
                        field[w, h] = area[wCounter, hCounter];
                    }
                }
            }
        }
    }
}