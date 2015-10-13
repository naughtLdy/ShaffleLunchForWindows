using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShuffleLunch.Models
{
    class PngExporter
    {
        public static void Export(FrameworkElement element)
        {
            // render uielement
            var rtb = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, 96, 96, PixelFormats.Default);
            rtb.Render(element);
            // save to png
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));
            // TODO: Use OpenFileDialog to choose target file.
            using (var fs = System.IO.File.OpenWrite("export.png"))
            {
                pngEncoder.Save(fs);
            }
        }
    }
}
