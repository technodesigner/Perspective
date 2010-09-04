using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using Perspective.Core;
using System.Text;

namespace Perspective
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Constructor.
        /// </summary>
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
            Perspective.ResourceAssembly.Register();
            Perspective.View.MainWindow mw = new Perspective.View.MainWindow();
            mw.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            IsolatedStorageHelper.SaveToUserStoreForDomain(this.Properties, _configFilename);
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string logFileName = "Perspective.Errors.log";
            var errorStringBuilder = new StringBuilder();
            var exception = e.Exception;
            errorStringBuilder.AppendLine(String.Format("{0} - Log : {1}\n", DateTime.Now, logFileName));
            errorStringBuilder.AppendLine("");
            errorStringBuilder.AppendLine(exception.ToString());
            var compactErrorText = (String.Format("{0} : {1}\n\nLog : {2}", exception.GetType().ToString(), exception.Message, logFileName));
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                errorStringBuilder.AppendLine("");
                errorStringBuilder.AppendLine("InnerException :");
                errorStringBuilder.AppendLine(exception.ToString());
            }
            var errortext = errorStringBuilder.ToString();
            using (var writer = new System.IO.StreamWriter(logFileName, true))
            {
                writer.Write(errortext);
            }
            MessageBox.Show(compactErrorText, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
