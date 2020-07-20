using System;
using System.ComponentModel;
using System.Diagnostics;
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
            var settings = new GameSettings(
                (int)FieldImage.Width,
                (int)FieldImage.Height,
                new GliderAtTheMiddlePreset());

            var game = new Game(settings, new WpfRender(FieldImage));
            game.Prepare();

            var timer = new Timer(1000);
            timer.Elapsed += (_, e) =>
            {
                game.MakeNextGeneration();
                Debug.WriteLine($"Generation {DateTime.Now:HH:mm:ss}");
            };
            timer.Start();
        }
    }
}