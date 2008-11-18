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
using System.Xml;
using System.Windows.Resources;
using System.Windows.Media.Media3D;
using System.Globalization;
using System.IO;
using System.Windows.Interop;
using Perspective.Wpf;

namespace PerspectiveDemo.UI.Pages.pWpf3D
{
    /// <summary>
    /// Interaction logic for BubbleChart.xaml
    /// This page shows how to dynamically instantiate 3D shapes to build a bubble chart from XML data. It also shows how to copy the 3D scene in a graphic file using a RenderTargetBitmap and an encoder.
    /// </summary>
    public partial class BubbleChart : Page
    {
        public BubbleChart()
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
                XmlDocument xd = new XmlDocument();

                StreamResourceInfo sri = Application.GetResourceStream(
                    new Uri("pack://application:,,,/PerspectiveDemo.UI;component/Pages/pWpf3D/BubbleChartData.xml", UriKind.Absolute));
                xd.Load(sri.Stream);

                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";

                XmlNodeList xnl = xd.SelectNodes("/DataList/Data");
                for (int i = 0; i < xnl.Count; i++)
                {
                    XmlElement xe = (XmlElement)xnl.Item(i);
                    double x = Convert.ToDouble(xe.GetAttribute("X"), provider);
                    double y = Convert.ToDouble(xe.GetAttribute("Y"), provider);
                    double z = Convert.ToDouble(xe.GetAttribute("Z"), provider);
                    double value = Convert.ToDouble(xe.GetAttribute("Value"), provider);

                    Perspective.Wpf3D.Shapes.Spherical3D s = new Perspective.Wpf3D.Shapes.Spherical3D();
                    s.Material = (Material)this.FindResource("GlossyMaterial");
                    s.ParallelCount = 100;

                    Transform3DGroup transformGroup = new Transform3DGroup();
                    TranslateTransform3D translation = new TranslateTransform3D(x, y, z);
                    transformGroup.Children.Add(translation);
                    ScaleTransform3D scaling = new ScaleTransform3D(value, value, value);
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
