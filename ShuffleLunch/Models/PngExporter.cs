using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShuffleLunch.Models
{
    class PngExporter
    {
        public static void Export(FrameworkElement element)
        {
            var rtb = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, 96, 96, PixelFormats.Rgb24);
            rtb.Render(element);
        }
    }
}
