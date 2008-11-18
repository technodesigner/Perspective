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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Perspective.Core;
using System.Globalization;
using Perspective.Wpf;

namespace PerspectiveDemo.UI.Pages.pWpf
{
    /// <summary>
    /// Interaction logic for DpiScaling.xaml
    /// </summary>
    public partial class DpiScaling : Page
    {
        public DpiScaling()
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
    }
}
