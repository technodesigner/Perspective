using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using PerspectiveDemo.Wpf3D.ViewModel;
using System.Windows.Media.Media3D;
using PerspectiveDemo.Wpf3D.Entities;
using System.IO;

namespace PerspectiveDemo.Wpf3D.View
{
    /// <summary>
    /// Interaction logic for BubbleChartDemo.xaml
    /// </summary>
    public partial class BubbleChartDemo : Page
    {
        public BubbleChartDemo()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (BrowserInteropHelper.IsBrowserHosted)
            {
                bCreateFile.Visibility = Visibility.Collapsed;
            }
            
            try
            {
                BubbleViewModel bubbleViewModel = new BubbleViewModel();
                for (int i = 0; i < bubbleViewModel.Bubbles.Count; i++)
                {
                    Bubble bubble = bubbleViewModel.Bubbles[i];

                    Perspective.Wpf3D.Shapes.Spherical3D s = new Perspective.Wpf3D.Shapes.Spherical3D();
                    s.Material = (Material)this.FindResource("GlossyMaterial");
                    s.ParallelCount = 100;

                    Transform3DGroup transformGroup = new Transform3DGroup();
                    TranslateTransform3D translation = new TranslateTransform3D(
                        bubble.X, bubble.Y, bubble.Z);
                    transformGroup.Children.Add(translation);
                    ScaleTransform3D scaling = new ScaleTransform3D(
                        bubble.Value, bubble.Value, bubble.Value);
                    transformGroup.Children.Add(scaling);
                    s.Transform = transformGroup;

                    workshop.Children.Add(s);
                }
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void bCreateFile_Click(object sender, RoutedEventArgs e)
        {

            if (!BrowserInteropHelper.IsBrowserHosted)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                try
                {
                    RenderTargetBitmap rtb = workshop.TakeScreenshot(300);

                    using (FileStream fs = File.OpenWrite("BubbleChart.png"))
                    {
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(rtb));
                        encoder.Save(fs);
                    }
                }
                finally
                {
                    Mouse.OverrideCursor = null;
                }
            }
        }
    }
}
