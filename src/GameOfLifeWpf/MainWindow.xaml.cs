using System.Threading.Tasks;
using System.Windows.Media;
using GameOfLife.Abstractions;
using GameOfLife.CSharp.Preset;
//using GameOfLife.CSharp;
using GameOfLife.FSharp;
using GameOfLife.FSharp.Game;

namespace GameOfLifeWpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            RenderOptions.SetBitmapScalingMode(FieldImage, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(FieldImage, EdgeMode.Aliased);

            IGame game = InitializeGame();

            FieldImage.Height = Height;
            FieldImage.Width = Width;

            RunGame(game);
        }

        private void RunGame(IGame game)
        {
            Task.Run(
                () =>
                {
                    long counter = 0;
                    while (true)
                    {
                        counter++;
                        game.MakeNextGeneration();
                        Dispatcher.Invoke(() => Counter.Text = counter.ToString());
                    }});
        }

        private IGame InitializeGame()
        {
            // Game game = Game.CreateBuilder()
            //     .SetSize((int)FieldImage.Width, (int)FieldImage.Height)
            //     .UseConwaysGameOfLife()
            //     .RenderWith(new WpfRender(FieldImage))
            //     .WithPreset(new RandomAreas(40, 2, new RandomPreset(70)))
            //     .Build();
            // game.Prepare();

            IGame game = new Game(new GameSettings((int) FieldImage.Width, (int) FieldImage.Height,
                new Preset.RandomAreas(40, 2, new Preset.RandomPreset(70)),
                //new Preset.StickPreset(),
                new WpfRender(FieldImage),
                new Ruleset.ConwaysRuleset()));
            game.Prepare();
            return game;
        }
    }
}