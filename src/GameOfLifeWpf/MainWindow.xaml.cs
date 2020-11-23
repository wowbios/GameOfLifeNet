using System.Threading.Tasks;
using System.Windows.Media;
using GameOfLife.CSharp;
using GameOfLife.CSharp.Preset;

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
            Game game = Game.CreateBuilder()
                .SetSize((int)FieldImage.Width, (int)FieldImage.Height)
                .UseConwaysGameOfLife()
                .RenderWith(new WpfRender(FieldImage))
                .WithPreset(new RandomAreas(40, 2, new RandomPreset(70)))
                .Build();
            game.Prepare();

            return game;
        }
    }
}