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
using System.Windows;
using System.Collections.ObjectModel;

namespace Perspective.Wpf
{
    /// <summary>
    /// A class for skin handling of Perspective.Wpf controls.
    /// </summary>
    public class SkinManager : Perspective.Core.SkinManager
    {
        private const string _defaultSkinFolder = "Skins";
        private const string _defaultThemeFolder = "Themes";
        private const string _defaultTheme = "Generic";
        private const string _assemblyName = "Perspective.Wpf";

        private SkinManager()
        {
        }

        private static SkinManager _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets a signleton instance of SkinManager.
        /// </summary>
        public static SkinManager Current
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SkinManager();
                    }
                    return _instance;
                }
            }
        }

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        protected override string AssemblyName
        {
            get
            {
                return _assemblyName;
            }
        }

        /// <summary>
        /// Loads the default skin.
        /// </summary>
        public override void LoadDefaultSkin()
        {
            UnloadSkin();
        }

        /// <summary>
        /// Loads a given skin.
        /// </summary>
        /// <param name="skinFolderName">Name of the skin folder (i.e. "Skins/Perspective")</param>
        /// <param name="dictionaryFileName">Name of the dictionary file (i.e. "Resources" without .xaml extension)</param>
        public override void LoadSkin(string skinFolderName, string dictionaryFileName)
        {
            UnloadSkin();
            base.LoadSkin(skinFolderName, dictionaryFileName);
        }

        /// <summary>
        /// Loads a skin from the default skin folder and/or the default dictionary name.
        /// </summary>
        /// <param name="skinName">Name of the skin</param>
        public override void LoadSkin(string skinName)
        {
            LoadSkin(_defaultSkinFolder, skinName);
        }

        private void UnloadSkin()
        {
            if (!String.IsNullOrEmpty(SkinFolder))
            {
                Collection<ResourceDictionary> dictionaries = Application.Current.Resources.MergedDictionaries;
                foreach (ResourceDictionary rd in dictionaries)
                {
                    if (rd.Source == this.DictionaryUri)
                    {
                        dictionaries.Remove(rd);
                        SkinFolder = _defaultThemeFolder;
                        DictionaryName = _defaultTheme;
                        this.DictionaryUri = null;
                        break;
                    }
                }
            }
        }
    }
}
