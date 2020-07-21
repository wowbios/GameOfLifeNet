using System.Threading.Tasks;
using System.Windows.Media;
using GameOfLifeNet;
using GameOfLifeNet.Preset;
using GameOfLifeNet.Ruleset;

namespace GameOfLifeWpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            RenderOptions.SetBitmapScalingMode(FieldImage, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(FieldImage, EdgeMode.Aliased);

            Game game = InitializeGame();

            FieldImage.Height = Height;
            FieldImage.Width = Width;

            RunGame(game);
        }

        private void RunGame(Game game)
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

        private Game InitializeGame()
        {
            var preset = new RandomPreset(70);

            var settings = new GameSettings(
                (int)FieldImage.Width,
                (int)FieldImage.Height,
                new RandomAreas(40, 2, preset));

            var game = new Game(settings, new WpfRender(FieldImage), new ConwaysRuleset());
            game.Prepare();

            return game;
        }
    }
}