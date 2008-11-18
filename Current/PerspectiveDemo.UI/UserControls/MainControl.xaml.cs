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
using System.Threading;
using Perspective.Wpf;
using Perspective.Wpf.ResourceStrings;
using System.Windows.Interop;
using Perspective.Core.Wpf;
using System.Windows.Threading;

namespace PerspectiveDemo.UI.UserControls
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        public MainControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            hlDpiScaling.IsEnabled = !System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed;
            
            string skin = (string)Application.Current.Properties["Skin"];
            if (!String.IsNullOrEmpty(skin))
            {
                LoadSkin(skin);
                switch (skin)
                {
                    case "LaMoisson":
                        rbLaMoisson.IsChecked = true;
                        break;
                    case "PsycheRock":
                        rbPsycheRock.IsChecked = true;
                        break;
                    case "BlackAndWhite":
                        rbBlackAndWhite.IsChecked = true;
                        break;
                }
            }
            else
            {
                SkinManager.Current.LoadDefaultSkin();
                Perspective.Wpf.SkinManager.Current.LoadDefaultSkin();
#if DN35
                Perspective.Wpf3D.SkinManager.Current.LoadDefaultSkin();
#endif
            }

            if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "fr")
            {
                rbFrench.IsChecked = true;
            }
            else
            {
                rbEnglish.IsChecked = true;
            }

            if (!BrowserInteropHelper.IsBrowserHosted)
            {
                tabControl1.LayoutTransform = DipHelper.ScreenIndependentScaleTransform;
            }

            if (BrowserInteropHelper.IsBrowserHosted)
            {
                string pageToOpenParam = XbapHelper.PageParameterValue;
                if (!String.IsNullOrEmpty(pageToOpenParam))
                {
                    // 0.9.2 : Pages like Box3D.xaml have been renamed as Box3DDemo.xaml
                    // but their URL have already been published, so we must be compatible
                    string s3D = "3D.xaml";
                    if (pageToOpenParam.EndsWith(s3D))
                    {
                        pageToOpenParam = pageToOpenParam.Replace(s3D, "3DDemo.xaml");
                    }

                    if (AppCommands.OpenPageCommand.CanExecute(pageToOpenParam, this))
                    {
                        AppCommands.OpenPageCommand.Execute(pageToOpenParam, this);
                    }
                }
            }
        }

        private void OpenPageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter != null)
            {
                string page = e.Parameter.ToString();
                if (!String.IsNullOrEmpty(page))
                {
                    string pageUriName = String.Format(@"/PerspectiveDemo.UI;component/Pages/{0}", page);
                    frame1.Source = new Uri(pageUriName, UriKind.RelativeOrAbsolute);
                    if (page != "pWpf/DpiScaling.xaml")
                    {
                        frame1.LayoutTransform = DipHelper.ScreenIndependentScaleTransform;
                    }
                    else
                    {
                        frame1.LayoutTransform = null;
                    }
                }
            }
        }

        private void LoadSkinCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter != null)
            {
                string skin = e.Parameter.ToString();
                if (!String.IsNullOrEmpty(skin))
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    try
                    {
                        LoadSkin(skin);
                        Application.Current.Properties["Skin"] = skin;
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                    }
                }
            }
        }

        private void LoadSkin(string skin)
        {
            SkinManager.Current.LoadSkin(skin);
            Perspective.Wpf.SkinManager.Current.LoadSkin(skin);
#if DN35
            Perspective.Wpf3D.SkinManager.Current.LoadSkin(skin);
#endif
        }

        private void SetCulture_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter != null)
            {
                string culture = e.Parameter.ToString();
                if (!String.IsNullOrEmpty(culture))
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    try
                    {
                        CultureManager.Current.UICulture = culture;
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                    }
                }
            }
        }
    }
}
