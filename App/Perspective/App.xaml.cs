using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using Perspective.Core;
using System.Text;
using System.IO;

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
            string logFilePath = Path.Combine(LocalUserAppDataPath, logFileName);
            var errorStringBuilder = new StringBuilder();
            var exception = e.Exception;
            errorStringBuilder.AppendLine(String.Format("{0}", DateTime.Now));
            errorStringBuilder.AppendLine("");
            errorStringBuilder.AppendLine(exception.ToString());
            var compactErrorText = (String.Format("{0} : {1}\n\nLog : {2}\n(%LOCALAPPDATA%\\{3}\\Perspective\\{4})", 
                exception.GetType().ToString(), 
                exception.Message, 
                logFilePath,
                Perspective.Core.LibraryInfo.Company,
                logFileName));
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                errorStringBuilder.AppendLine("");
                errorStringBuilder.AppendLine("InnerException :");
                errorStringBuilder.AppendLine(exception.ToString());
            }
            errorStringBuilder.AppendLine("");
            errorStringBuilder.AppendLine("--------------------------------------------------------------------------------");
            errorStringBuilder.AppendLine("");
            var errortext = errorStringBuilder.ToString();
            using (var writer = new System.IO.StreamWriter(logFilePath, true))
            {
                writer.Write(errortext);
            }
            MessageBox.Show(compactErrorText, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        /// <summary>
        /// Obtient le chemin d'accès aux données d'application d'un utilisateur local non itinérant.
        /// Si le chemin néxiste pas, il est créé.
        /// 
        /// Gets the path for the application data of a local, non-roaming user.
        /// If the path does not exist, one is created.
        /// </summary>
        public static string LocalUserAppDataPath
        {
            get
            {
                var folderName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                folderName = Path.Combine(folderName, String.Format(@"{0}\{1}", Perspective.Core.LibraryInfo.Company, "Perspective"));
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
                return folderName;
            }
        }
    }
}
