using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using GameOfLifeNet;
using GameOfLifeNet.Preset;

namespace GameOfLifeWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RenderOptions.SetBitmapScalingMode(FieldImage, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(FieldImage, EdgeMode.Aliased);

            var preset = new RandomPreset(70);

            var settings = new GameSettings(
                (int)FieldImage.Width,
                (int)FieldImage.Height,
                new RandomAreas(40, 2, preset));

            var game = new Game(settings, new WpfRender(FieldImage));
            game.Prepare();

            FieldImage.Height = Height;
            FieldImage.Width = Width;

            Task.Run(
                () =>
                {
                    long counter = 0;
                    while (true)
                    {
                        counter++;
                        game.MakeNextGeneration();
                        Dispatcher.Invoke(() => Counter.Text = counter.ToString());
                    }
                });
        }
    }
}