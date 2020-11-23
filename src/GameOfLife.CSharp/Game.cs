using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameOfLife.CSharp.Builder;

namespace GameOfLife.CSharp
{
    public class Game
    {
        private readonly IGameSettings _settings;
        private bool[,] _field;
        private long _generation;

        internal Game(IGameSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _field = new bool[settings.Width, settings.Height];
        }

        public static IGameBuilder CreateBuilder() => new GameOfLifeFluentBuilder();

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
                            bool isAlive = _settings.Ruleset.IsAlive(_field, i, j);
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

        private void Render(IEnumerable<ChangeEvent> events)
            => _settings.Render.Render(new GameState(_generation, events));
    }
}