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
using System.Configuration;
using System.Data;
using System.Windows;
using Perspective.Core;
using System.Windows.Media;
using System.Reflection;

namespace PerspectiveDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //// Localization test
            ////------------------
            //string culture = "en-US";
            //System.Threading.Thread.CurrentThread.CurrentUICulture =
            //    new System.Globalization.CultureInfo(culture);
            //System.Threading.Thread.CurrentThread.CurrentCulture =
            //    new System.Globalization.CultureInfo(culture);

            // Registers assemblies containing localization resources
            PerspectiveDemo.UI.ResourceAssembly.Register();
            Perspective.Wpf.ResourceAssembly.Register();

        }

        private string _filename = "PerspectiveDemo.dat";

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            IsolatedStorageHelper.SaveToUserStoreForDomain(this.Properties, _filename);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IsolatedStorageHelper.LoadFromUserStoreForDomain(this.Properties, _filename);

            MainWindow mw = new MainWindow();
            mw.Show();
        }

    }
}
