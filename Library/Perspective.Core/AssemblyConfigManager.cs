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
using System.Collections;
using System.IO.IsolatedStorage;
using System.Deployment.Application;

namespace Perspective.Core
{
    /// <summary>
    /// Provides support for assembly settings.
    /// For an installed application, the scope is : machine and assembly.
    /// For a ClickOnce application (XBAP), the scope is : application and user.
    /// </summary>
    public static class AssemblyConfigManager
    {
        private static string _perspectiveFilename = "Perspective.Core.dat";

        /// <summary>
        /// Static constructor. Loads the Settings dictionary from isolated storage.
        /// </summary>
        static AssemblyConfigManager()
        {
            LoadSettings();
        }

        private static Hashtable _settings = new Hashtable();

        /// <summary>
        /// Gets a dictionary containing the Perspective.Core assembly settings.
        /// </summary>
        public static Hashtable Settings
        {
            get { return _settings; }
        }

        /// <summary>
        /// Fills the Settings dictionary with the values stored in an isolated storage corresponding to the Perspective.Core assembly.
        /// </summary>
        public static void LoadSettings()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                IsolatedStorageHelper.LoadFromUserStoreForApplication(
                    _settings, 
                    _perspectiveFilename);
            }
            else
            {
                IsolatedStorageHelper.LoadForDomain(_settings,
                    IsolatedStorageScope.Assembly |
                    IsolatedStorageScope.Machine,
                    _perspectiveFilename);
            }
        }

        /// <summary>
        /// Saves the Settings dictionary in an isolated storage corresponding to the Perspective.Core assembly.
        /// </summary>
        public static void SaveSettings()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                IsolatedStorageHelper.SaveToUserStoreForApplication(
                    _settings,
                    _perspectiveFilename);
            }
            else
            {
                IsolatedStorageHelper.SaveForDomain(_settings,
                    IsolatedStorageScope.Assembly |
                    IsolatedStorageScope.Machine,
                    _perspectiveFilename);
            }
        }
    }
}
