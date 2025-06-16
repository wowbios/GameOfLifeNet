using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GameOfLife.CSharp;
using GameOfLife.CSharp.Preset;
using GameOfLife.CSharp.Ruleset;

namespace GameOfLifeWpf
{
    public partial class MainWindow : Window
    {
        private Game game;
        private CancellationTokenSource gameLoopTokenSource;
        private object gameLock = new object();

        public MainWindow()
        {
            InitializeComponent();

            RenderOptions.SetBitmapScalingMode(FieldImage, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(FieldImage, EdgeMode.Aliased);

            FieldImage.Height = Height;
            FieldImage.Width = Width;
            FieldImage.SizeChanged += FieldImage_SizeChanged;

            lock (gameLock)
            {
                game = InitializeGame((int)FieldImage.Width, (int)FieldImage.Height);
                gameLoopTokenSource = new CancellationTokenSource();
                RunGame(game, gameLoopTokenSource.Token);
            }
        }

        private void RunGame(Game game, CancellationToken cancellationToken)
        {
            Task.Run(
                () =>
                {
                    long counter = 0;
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        counter++;
                        game.MakeNextGeneration();
                        if (counter % 10 == 0)
                        {
                            try
                            {
                                Dispatcher.Invoke(() => 
                                {
                                    lock (gameLock)
                                    {
                                        Counter.Text = counter.ToString();
                                    }
                                });
                            }
                            catch (TaskCanceledException) { /* App is closing */ }
                        }
                    }
                }, cancellationToken);
        }

        private Game InitializeGame(int width, int height)
        {
            var baseRuleset = new ConwaysRuleset();
            var ruleset = new RandomRevivalRulesetDecorator(baseRuleset);
            
            Game game = Game.CreateBuilder()
                .SetSize(width, height)
                .UseRuleset(ruleset)  // Use the decorated ruleset
                .RenderWith(new WpfRender(FieldImage))
                .WithPreset(new RandomAreas(40, 2, new RandomPreset(70)))
                .Build();
            game.Prepare();

            return game;
        }

        private void FieldImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            lock (gameLock)
            {
                gameLoopTokenSource?.Cancel();
                gameLoopTokenSource = new CancellationTokenSource();
                game = InitializeGame((int)e.NewSize.Width, (int)e.NewSize.Height);
                RunGame(game, gameLoopTokenSource.Token);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            lock (gameLock)
            {
                gameLoopTokenSource?.Cancel();
                base.OnClosed(e);
            }
        }
    }
}