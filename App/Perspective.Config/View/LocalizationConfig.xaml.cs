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
using Perspective.Config.ViewModel;

namespace Perspective.Config.View
{
    /// <summary>
    /// Interaction logic for LocalizationDemo.xaml
    /// </summary>
    public partial class LocalizationConfig : Page
    {
        private LocalizationViewModel _localizationViewModel;

        public LocalizationConfig()
        {
            InitializeComponent();

            _localizationViewModel = new LocalizationViewModel();
            this.DataContext = _localizationViewModel;
        }

        private void radioButtonIsCheckedBinding_Converting(object sender, Perspective.Core.Wpf.Converters.ConverterEventArgs e)
        {
            e.ConvertedValue = false;
            string cultureName = e.Value as string;
            if (cultureName == _localizationViewModel.CurrentLocaleInfo.CultureName)
            {
                e.ConvertedValue = true;
            }
        }
    }
}
