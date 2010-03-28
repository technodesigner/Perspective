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

namespace PerspectiveDemo.Wpf.View
{
    /// <summary>
    /// Interaction logic for RangeControlDemo.xaml
    /// </summary>
    public partial class RangeControlDemo : Page
    {
        public RangeControlDemo()
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
