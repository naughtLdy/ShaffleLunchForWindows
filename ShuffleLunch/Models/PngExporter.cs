using Microsoft.Win32;
using System;
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

			var saveFileDialog = new SaveFileDialog();
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.Filter = "pngファイル(.png)|*.png|All Files (*.*)|*.*";

			// ファイル名は毎週水曜日
			var dt = DateTime.Today;
			for (int i = 0; i < 7; i++)
			{
				var d = dt.AddDays(i);
				if (d.DayOfWeek == DayOfWeek.Wednesday)
				{
					saveFileDialog.FileName = d.ToString("yyyyMMdd") + ".png";
					break;
				}
			}
			
			bool? result = saveFileDialog.ShowDialog();
			if (result == true)
			{
				using (var fs = System.IO.File.OpenWrite(saveFileDialog.FileName))
				{
					pngEncoder.Save(fs);
				}
			}

			
        }
    }
}
