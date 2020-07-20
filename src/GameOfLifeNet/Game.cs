using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace GameOfLifeNet
{
    public class Game
    {
        private readonly GameSettings _settings;
        private readonly IRender _render;
        private bool[,] _field;
        private long _generation;

        public Game(GameSettings settings, IRender render)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _render = render ?? throw new ArgumentNullException(nameof(render));
            _field = new bool[settings.Width, settings.Height];
        }

        public void Prepare()
        {
            _settings.Preset.InitializeField(_field);

            Render(ReadEvents());

            IEnumerable<ChangeEvent> ReadEvents()
            {
                for(var i=0;i<_field.GetLength(0);i++)
                for (var j = 0; j < _field.GetLength(1); j++)
                {
                    yield return new ChangeEvent(i,j, _field[i,j]);
                }
            }
        }

        public (TimeSpan, TimeSpan) MakeNextGeneration()
        {
            var dt = DateTime.Now;
            var events = new ConcurrentBag<ChangeEvent>();
            var newField = _field.Clone() as bool[,];
            Parallel.For(0, _settings.Width,
                i =>
                {
                    Parallel.For(0, _settings.Height, j =>
                    {
                        Iteration(i, j);
                    });
                });

            var calcSpan = DateTime.Now - dt;

            _field = newField;
            _generation++;

            if(events.Count > 0)
                Render(events);

            var renderSpan = DateTime.Now - dt - calcSpan;
            return (calcSpan, renderSpan);

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

                if (_field[i, j] != result)
                {
                    events.Add(new ChangeEvent(i, j, result));
                    newField[i, j] = result;
                }
            }
        }
        
        private void Render(IEnumerable<ChangeEvent> events)
        {
            _render.Render(new GameState(_generation, events));
        }

        private byte GetClosestAliveCount(int i, int j)
        {
            //    1_2_3
            // 1| 1 2 3
            // 2| 4 5 6
            // 3| 7 8 9
            byte alive = 0;

            int i1 = (i + _settings.Width - 1) % _settings.Width;
            int i3 = (i + _settings.Width + 1) % _settings.Width;
            int j1 = (j + _settings.Height - 1) % _settings.Height;
            int j3 = (j + _settings.Height + 1) % _settings.Height;

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
    }
}