//------------------------------------------------------------------
//
//  For licensing information and to get the latest version go to:
//  http://www.codeplex.com/perspective
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//  FITNESS FOR A PARTICULAR PURPOSE.
//
//------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Perspective.Core.Wpf.Imaging
{
    /// <summary>
    /// Image file creation from a Visual, UIElement or FrameworkElement.
    /// </summary>
    public static class ImagingHelper
    {
        /// <summary>
        /// Creates an image file from a Visual.
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <param name="pixelWidth">The width of the bitmap.</param>
        /// <param name="pixelHeight">The height of the bitmap.</param>
        /// <param name="dpiX">The horizontal DPI of the bitmap.</param>
        /// <param name="dpiY">The vertical DPI of the bitmap.</param>
        /// <param name="background">A background brush.</param>
        /// <param name="fileName">The filename.</param>
        /// <param name="encoder">The image encoder.</param>
        public static void CreateImageFile(
            Visual visual,
            int pixelWidth, int pixelHeight,
            int dpiX, int dpiY,
            Brush background, 
            string fileName,
            BitmapEncoder encoder)
        {
            var rtb = new RenderTargetBitmap(pixelWidth, pixelHeight, dpiX, dpiY, PixelFormats.Pbgra32);
            if (background == null)
            {
                rtb.Render(visual);
            }
            else
            {
                var visualBrush = new VisualBrush(visual);
                var drawingVisual = new DrawingVisual();
                var rect = new Rect(0, 0, pixelWidth, pixelHeight);
                using (var dc = drawingVisual.RenderOpen())
                {
                    dc.DrawRectangle(background, null, rect);
                    dc.DrawRectangle(visualBrush, null, rect);
                }
                rtb.Render(drawingVisual);
            }
            SaveTargetBitmap(rtb, fileName, encoder);
        }

        /// <summary>
        /// Saves a RenderTargetBitmap.
        /// </summary>
        /// <param name="rtb">The RenderTargetBitmap.</param>
        /// <param name="fileName">The filename.</param>
        /// <param name="encoder">The image encoder.</param>
        public static void SaveTargetBitmap(RenderTargetBitmap rtb,             
            string fileName,
            BitmapEncoder encoder)
        {
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            using (var fs = File.Create(fileName))
            {
                encoder.Save(fs);
            }
        }

        /// <summary>
        /// Creates a PNG file from a UIElement, named radix_wwwwxhhhh.png.
        /// </summary>
        /// <param name="uie">The UIElement.</param>
        /// <param name="pixelWidth">The width of the bitmap.</param>
        /// <param name="pixelHeight">The height of the bitmap.</param>
        /// <param name="dpi">The DPI of the bitmap.</param>
        /// <param name="background">A background brush.</param>
        /// <param name="directory">The output directory (optional).</param>
        /// <param name="fileRadix">The radix label of the file.</param>
        public static void CreatePngFile(UIElement uie, 
            int pixelWidth, 
            int pixelHeight, 
            int dpi, 
            Brush background, 
            string directory, 
            string fileRadix)
        {
            DrawingHelper.BuildLayout(uie, new Size(pixelWidth, pixelHeight));
            var fileName = String.Format("{2}_{0}x{1}.png", pixelWidth, pixelHeight, fileRadix);
            var filePath = String.IsNullOrEmpty(directory) ? fileName : Path.Combine(directory, fileName);
            CreateImageFile(uie, pixelWidth, pixelHeight, dpi, dpi, background, filePath, new PngBitmapEncoder());
        }

        /// <summary>
        /// Creates a 96 DPI PNG file from a FrameworkElement, named radix_wwwwxhhhh.png.
        /// </summary>
        /// <param name="element">The FrameworkElement.</param>
        /// <param name="directory">The output directory (optional).</param>
        /// <param name="fileRadix">The radix label of the file.</param>
        public static void CreatePngFile(FrameworkElement element, string directory, string fileRadix)
        {
            CreatePngFile(element, Convert.ToInt32(element.Width), Convert.ToInt32(element.Height), 96, null, directory, fileRadix);
        }

        /// <summary>
        /// Creates a JPEG file from a UIElement, named radix_wwwwxhhhh.jpg.
        /// </summary>
        /// <param name="uie">The UIElement.</param>
        /// <param name="pixelWidth">The width of the bitmap.</param>
        /// <param name="pixelHeight">The height of the bitmap.</param>
        /// <param name="dpi">The DPI of the bitmap.</param>
        /// <param name="background">A background brush.</param>
        /// <param name="directory">The output directory (optional).</param>
        /// <param name="fileRadix">The radix label of the file.</param>
        public static void CreateJpegFile(UIElement uie, int pixelWidth, int pixelHeight, int dpi, string directory, string fileRadix)
        {
            DrawingHelper.BuildLayout(uie, new Size(pixelWidth, pixelHeight));
            var fileName = String.Format("{2}_{0}x{1}.jpg", pixelWidth, pixelHeight, fileRadix);
            var filePath = String.IsNullOrEmpty(directory) ? fileName : Path.Combine(directory, fileName);
            CreateImageFile(uie, pixelWidth, pixelHeight, dpi, dpi, null, filePath, new JpegBitmapEncoder());
        }

        /// <summary>
        /// Creates a 96 DPI JPEG file from a FrameworkElement, named radix_wwwwxhhhh.jpg.
        /// </summary>
        /// <param name="element">The FrameworkElement.</param>
        /// <param name="directory">The output directory (optional).</param>
        /// <param name="fileRadix">The radix label of the file.</param>
        public static void CreateJpegFile(FrameworkElement element, string directory, string fileRadix)
        {
            CreateJpegFile(element, Convert.ToInt32(element.Width), Convert.ToInt32(element.Height), 96, directory, fileRadix);
        }

        /// <summary>
        /// Creates a XAML file from a FrameworkElement, named radix.xaml.
        /// </summary>
        /// <param name="element">The FrameworkElement.</param>
        /// <param name="directory">The output directory (optional).</param>
        /// <param name="fileRadix">The radix label of the file.</param>
        public static void CreateXamlFile(FrameworkElement element, string directory, string fileRadix)
        {
            var fileName = String.Format("{0}.xaml", fileRadix);
            var filePath = String.IsNullOrEmpty(directory) ? fileName : Path.Combine(directory, fileName);
            var xaml = XamlWriter.Save(element);
            using (var fs = File.Create(filePath))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.Write(xaml);
                }
            }
        }
    }
}