using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLifeNet
{
    public class Game
    {
        private readonly IGameSettings _settings;
        private readonly IGameField _field;
        private long _generation;

        internal Game(IGameSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _field = new BitArrayField(settings.Width, settings.Height);
        }

        public static IGameBuilder CreateBuilder() => new GameOfLifeFluentBuilder();

        public void Prepare()
        {
            _settings.Preset.InitializeField(_field);

            _field.Swap();
            Render(ReadEvents());

            IEnumerable<ChangeEvent> ReadEvents()
            {
                for (var i = 0; i < _field.Width; i++)
                for (var j = 0; j < _field.Height; j++)
                    yield return new ChangeEvent(i, j, _field[i, j]);
            }
        }

        public void MakeNextGeneration()
        {
            var events = new ConcurrentBag<ChangeEvent>();
            Parallel.For(
                0,
                _settings.Width,
                i =>
                {
                    Parallel.For(0, _settings.Height,
                        j =>
                        {
                            bool isAlive = _settings.Ruleset.IsAlive(_field, i, j);
                            if (_field[i, j] != isAlive)
                            {
                                events.Add(new ChangeEvent(i, j, isAlive));
                                _field[i, j] = isAlive;
                            }
                        });
                });

            _field.Swap();
            _generation++;

            if (events.Count > 0)
                Render(events);
        }

        private void Render(IEnumerable<ChangeEvent> events)
            => _settings.Render.Render(new GameState(_generation, events));
    }
}