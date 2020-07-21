using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLifeNet
{
    public class Game
    {
        private readonly GameSettings _settings;
        private readonly IRender _render;
        private readonly IRuleset _ruleset;
        private bool[,] _field;
        private long _generation;

        public Game(GameSettings settings, IRender render, IRuleset ruleset)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _render = render ?? throw new ArgumentNullException(nameof(render));
            _ruleset = ruleset ?? throw new ArgumentNullException(nameof(ruleset));
            _field = new bool[settings.Width, settings.Height];
        }

        public void Prepare()
        {
            _settings.Preset.InitializeField(_field);

            Render(ReadEvents());

            IEnumerable<ChangeEvent> ReadEvents()
            {
                for (var i = 0; i < _field.GetLength(0); i++)
                for (var j = 0; j < _field.GetLength(1); j++)
                {
                    yield return new ChangeEvent(i, j, _field[i, j]);
                }
            }
        }

        public void MakeNextGeneration()
        {
            var events = new ConcurrentBag<ChangeEvent>();
            var newField = _field.Clone() as bool[,];
            Parallel.For(
                0,
                _settings.Width,
                i =>
                {
                    Parallel.For(0, _settings.Height,
                        j =>
                        {
                            bool isAlive = _ruleset.IsAlive(_field, i, j);
                            if (_field[i, j] != isAlive)
                            {
                                events.Add(new ChangeEvent(i, j, isAlive));
                                newField[i, j] = isAlive;
                            }
                        });
                });

            _field = newField;
            _generation++;

            if (events.Count > 0)
                Render(events);
        }

        private void Render(IEnumerable<ChangeEvent> events) => _render.Render(new GameState(_generation, events));
    }
}