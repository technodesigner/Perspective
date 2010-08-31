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
using Perspective.Core.Wpf;

namespace Perspective.Config.View
{
    /// <summary>
    /// Interaction logic for RenderingConfig.xaml
    /// </summary>
    public partial class RenderingConfig : Page
    {
        public RenderingConfig()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tbRenderingTier.Text = String.Format("Rendering tier : {0}", RenderingHelper.RenderingTier);
        }

        public bool IsHWAccelerationDisabled
        {
            get
            {
                return RenderingHelper.IsHWAccelerationDisabled;
            }
            set
            {
                RenderingHelper.IsHWAccelerationDisabled = value;
            }
        }
    }
}
