using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GameOfLifeNet;

namespace GameOfLifeWpf
{
    public class WpfRender : IRender
    {
        private readonly Image _image;
        private bool _inited;
        private WriteableBitmap _bitmap;

        public WpfRender(Image image)
        {
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        public void Render(GameState state)
        {
            if(!_inited)
                Init();

            Application.Current.Dispatcher.Invoke(() => DrawPixelsEx(state.Field));
        }

        private void Init()
        {
            _image.Source = _bitmap = BitmapFactory.New(
                (int)_image.Width,
                (int)_image.Height);
            _bitmap.Clear(Colors.Black);

            _inited = true;
        }

        // The DrawPixel method updates the WriteableBitmap by using
        // unsafe code to write a pixel into the back buffer.
        private void DrawPixels(bool[,] field)
        {
            try
            {
                // Reserve the back buffer for updates.
                _bitmap.Lock();

                unsafe
                {
                    // Get a pointer to the back buffer.
                    IntPtr pBackBuffer = _bitmap.BackBuffer;
                    for (var column = 0; column < field.GetLength(0); column++)
                    for (var row = 0; row < field.GetLength(1); row++)
                    {
                        // Find the address of the pixel to draw.
                        pBackBuffer += row * _bitmap.BackBufferStride;
                        pBackBuffer += column * 4;

                        // Compute the pixel's color.
                        int colorData = 255 << 16; // R
                        colorData |= 128 << 8;   // G
                        colorData |= 255 << 0;   // B

                        // Assign the color data to the pixel.
                        *((int*) pBackBuffer) = colorData;

                        // Specify the area of the bitmap that changed.
                        _bitmap.AddDirtyRect(new Int32Rect(column, row, 1, 1));
                    }
                }
            }
            finally
            {
                // Release the back buffer and make it available for display.
                _bitmap.Unlock();
            }
        }

        private void DrawPixelsEx(bool[,] field)
        {
            Debug.WriteLine("Draw");
            using (_bitmap.GetBitmapContext())
                _bitmap.ForEach((x, y) => field[x, y] ? Colors.White : Colors.Black);
        }
    }
}