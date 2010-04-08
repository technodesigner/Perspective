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
using Perspective.Wpf;
using Perspective.Core;

namespace PerspectiveDemo.Wpf.View
{
    /// <summary>
    /// Interaction logic for DpiScalingDemo.xaml
    /// </summary>
    public partial class DpiScalingDemo : Page
    {
        public DpiScalingDemo()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Point systemDpi = DipHelper.GetSystemDpi();
            tbSystemDpiValueText.Text = systemDpi.X.ToString();
            sScreenSize.Value = DipHelper.ScreenSize;
        }

        private void bSaveScale_Click(object sender, RoutedEventArgs e)
        {
            DipHelper.ScreenIndependentScaleX = ScreenIndependentScaleTransform.ScaleX;
            DipHelper.ScreenIndependentScaleY = ScreenIndependentScaleTransform.ScaleY;
            DipHelper.ScreenSize = sScreenSize.Value;
            AssemblyConfigManager.SaveSettings();
        }

        private void diagonalScreenSizeBinding_Converting(object sender, Perspective.Core.Wpf.Converters.ConverterEventArgs e)
        {
            double diagonal = (double)e.Value;
            diagonal = Math.Round(diagonal, 2, MidpointRounding.ToEven);
            e.ConvertedValue = diagonal;
        }

        private void diagonalScreenSizeBinding_ConvertingBack(object sender, Perspective.Core.Wpf.Converters.ConverterEventArgs e)
        {
            string diagonal = (string)e.Value;
            e.ConvertedValue = Convert.ToDouble(diagonal);
        }
    }
}
