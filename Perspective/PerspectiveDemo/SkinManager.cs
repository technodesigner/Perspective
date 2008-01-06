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

namespace PerspectiveDemo
{
    /// <summary>
    /// A class for skin handling of Perspective application UI
    /// </summary>
    public class SkinManager : Perspective.Core.SkinManagerBase
    {
        private const string _defaultSkinBaseFolder = "Skins";

        internal string DefaultSkinBaseFolder
        {
            get { return _defaultSkinBaseFolder; }
        }

        private const string _defaultSkin = "PsycheRock";
        // private const string _defaultDictionary = "Resources";
        private const string _assemblyName = "PerspectiveDemo";
        private SkinManager()
        {
        }

        private static SkinManager _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets a singleton instance of SkinManager.
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
            // LoadSkin(_defaultSkinBaseFolder, _defaultSkinFolder, _defaultDictionary);
            LoadSkin(_defaultSkinBaseFolder, _defaultSkin);
        }

        /// <summary>
        /// Loads a skin.
        /// </summary>
        /// <param name="skinBaseFolder">Name of the base folder (i.e. "Skins").</param>
        /// <param name="skinFolder">Name of the skin's subfolder (i.e. "Generic").</param>
        /// <param name="dictionary">Name of the dictionary file (i.e. "Resources" without .xaml extension)</param>
        public void LoadSkin(string skinBaseFolder, string skinFolder, string dictionary)
        {
            LoadSkin(String.Format("{0}/{1}", skinBaseFolder, skinFolder), dictionary);
        }

        /// <summary>
        /// Loads a skin (from the default base folder and with the default resource file name).
        /// </summary>
        /// <param name="skinName">Name of the skin.</param>
        public override void LoadSkin(string skinName)
        {
            // LoadSkin(_defaultSkinBaseFolder, skinName, _defaultDictionary);
            LoadSkin(_defaultSkinBaseFolder, skinName);
        }

    }
}
