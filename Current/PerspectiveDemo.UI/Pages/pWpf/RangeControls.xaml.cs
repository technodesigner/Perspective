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

namespace PerspectiveDemo.UI.Pages.pWpf
{
    /// <summary>
    /// Interaction logic for RangeControls.xaml
    /// </summary>
    public partial class RangeControls : Page
    {
        public RangeControls()
        {
            InitializeComponent();
        }

        private void knobValueBinding_Converting(object sender, Perspective.Core.Wpf.Converters.ConverterEventArgs e)
        {
            double knobValue = System.Convert.ToDouble(e.Value, e.Culture);
            e.ConvertedValue = Math.Round(knobValue, 1, MidpointRounding.ToEven);
        }
    }
}
