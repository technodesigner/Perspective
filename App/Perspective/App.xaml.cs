using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

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
    }
}
