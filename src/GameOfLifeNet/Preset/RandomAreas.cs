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

        public void InitializeField(IGameField field)
        {
            var areaWidth = field.Width * _areaSize / 100;
            var areaHeight = field.Height * _areaSize / 100;

            var rnd = new Random(Environment.TickCount);
            for (int i = 0; i < _areasCount; i++)
            {
                var area = new BitArrayField(areaWidth, areaHeight);
                _preset.InitializeField(area);
                area.Swap();
                var wIndex = rnd.Next(field.Width);
                var hIndex = rnd.Next(field.Height);
                for (int wCounter = 0; wCounter < areaWidth; wCounter++)
                {
                    for (int hCounter = 0; hCounter < areaHeight; hCounter++)
                    {
                        int w = (wIndex + wCounter) % field.Width;
                        int h = (hIndex + hCounter) % field.Height;
                        field[w, h] = area[wCounter, hCounter];
                    }
                }
            }
        }
    }
}