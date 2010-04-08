using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using Perspective.Core;

namespace Perspective
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
            //string culture = "it-IT";
            //System.Threading.Thread.CurrentThread.CurrentUICulture =
            //    new System.Globalization.CultureInfo(culture);
            //System.Threading.Thread.CurrentThread.CurrentCulture =
            //    new System.Globalization.CultureInfo(culture);

            //Console.WriteLine("CurrentUICulture {0}.", System.Globalization.CultureInfo.CurrentUICulture.Name);


            // Internationalization
            Perspective.Wpf.ResourceAssembly.Register();

            // Skins
            Perspective.Wpf.SkinManager.Current.LoadDefaultSkin();
            Perspective.Wpf3D.SkinManager.Current.LoadDefaultSkin();
        }

        private const string _configFilename = "Perspective.dat";

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IsolatedStorageHelper.LoadFromUserStoreForDomain(this.Properties, _configFilename);

            Perspective.View.MainWindow mw = new Perspective.View.MainWindow();
            mw.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            IsolatedStorageHelper.SaveToUserStoreForDomain(this.Properties, _configFilename);
        }
    }
}
