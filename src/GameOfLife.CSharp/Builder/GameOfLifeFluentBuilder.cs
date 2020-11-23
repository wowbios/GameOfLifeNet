using System;
using System.Drawing;
using GameOfLife.Abstractions;
using GameOfLife.CSharp.Exceptions;
using GameOfLife.CSharp.Preset;
using GameOfLife.CSharp.Ruleset;

namespace GameOfLife.CSharp.Builder
{
    internal sealed class GameOfLifeFluentBuilder :
        IGameBuilder,
        IGameWithSize,
        IGameWithRuleset,
        IGameWithRenderer,
        IGameWithPreset,
        IGameSettings
    {
        private int _width;
        private int _height;
        private IPreset _preset;
        private IRender _render;
        private IRuleset _ruleset;

        internal GameOfLifeFluentBuilder()
        {
        }

        int IGameSettings.Width => _width;

        int IGameSettings.Height => _height;

        IPreset IGameSettings.Preset => _preset;

        IRender IGameSettings.Render => _render;

        IRuleset IGameSettings.Ruleset => _ruleset;


        public IGameWithSize SetSize(int size) => SetSize(size, size);

        public IGameWithSize SetSize(Size size) => SetSize(size.Width, size.Height);

        public IGameWithSize SetSize(int width, int height)
        {
            if(width <= 0)
                throw new ArgumentException("Width must be positive", nameof(width));
            if(height <= 0)
                throw new ArgumentException("Height must be positive", nameof(height));

            _width = width;
            _height = height;
            return this;
        }

        public IGameWithRuleset UseRuleset(IRuleset ruleset)
        {
            _ruleset = ruleset ?? throw new ArgumentNullException(nameof(ruleset));
            return this;
        }

        public IGameWithRuleset UseConwaysGameOfLife()
        {
            _ruleset = new ConwaysRuleset();
            return this;
        }

        public IGameWithRenderer RenderWith(IRender render)
        {
            _render = render ?? throw new ArgumentNullException(nameof(render));
            return this;
        }

        public IGameWithPreset WithPreset(IPreset preset)
        {
            _preset = preset ?? throw new ArgumentNullException(nameof(preset));
            return this;
        }

        public IGameWithPreset WithRandomPreset(int fulfillPercent)
        {
            _preset = new RandomPreset(fulfillPercent);
            return this;
        }

        public IGameWithPreset WithGlider()
        {
            _preset = new GliderAtTheMiddlePreset();
            return this;
        }

        public Game Build()
        {
            Validate();
            return new Game(this);
        }

        private void Validate()
        {
            if (_height <= 0 || _width <= 0)
                throw new GameOfLifeException("Некорретные размеры");

            _ = _preset ?? throw new GameOfLifeException("Preset is not set");
            _ = _render ?? throw new GameOfLifeException("Render is not set");
            _ = _ruleset ?? throw new GameOfLifeException("Ruleset is not set");
        }
    }
}