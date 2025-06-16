using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using GameOfLife.Abstractions;

namespace GameOfLifeWpf
{
    public class WpfRender : IRender
    {
        private readonly Image _image;
        private WriteableBitmap _bitmap;
        private bool _inited;
        private readonly object _renderLock = new object();

        public WpfRender(Image image)
        {
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        public void Render(IGameState state)
        {
            if (state == null) return;
            
            if (!_inited)
            {
                Init();
                return;
            }

            try 
            {
                var events = state.Events.ToList();
                if (events.Count == 0) return;

                var dispatcher = Application.Current?.Dispatcher;
                if (dispatcher == null) return;

                dispatcher.BeginInvoke((Action)(() => 
                {
                    try
                    {
                        lock (_renderLock)
                        {
                            if (_bitmap == null) return;
                            
                            using (var context = _bitmap.GetBitmapContext())
                            {
                                foreach (var e in events)
                                {
                                    _bitmap.SetPixel(e.X, e.Y, e.IsAlive ? Colors.White : Colors.Black);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Render error: {ex.Message}");
                    }
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Render preparation error: {ex.Message}");
            }
        }

        private void Init()
        {
            _image.Source = _bitmap = BitmapFactory.New(
                (int)_image.Width,
                (int)_image.Height);
            _bitmap.Clear(Colors.Black);
            _inited = true;
        }
    }
}