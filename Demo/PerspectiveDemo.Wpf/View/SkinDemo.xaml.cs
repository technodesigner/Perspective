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
using PerspectiveDemo.Wpf.ViewModel;

namespace PerspectiveDemo.Wpf.View
{
    /// <summary>
    /// Interaction logic for SkinDemo.xaml
    /// </summary>
    public partial class SkinDemo : Page
    {
        private SkinViewModel _skinViewModel = new SkinViewModel();

        public SkinDemo()
        {
            InitializeComponent();
            this.DataContext = _skinViewModel;
        }

        private void radioButtonIsCheckedBinding_Converting(object sender, Perspective.Core.Wpf.Converters.ConverterEventArgs e)
        {
            e.ConvertedValue = false;
            string skin = e.Value as string;
            if (skin == _skinViewModel.CurrentSkin)
            {
                e.ConvertedValue = true;
            }
        }
    }
}
