﻿using System;
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
    /// Interaction logic for LocalizationDemo.xaml
    /// </summary>
    public partial class LocalizationDemo : Page
    {
        private LocalizationViewModel _localizationViewModel;

        public LocalizationDemo()
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
