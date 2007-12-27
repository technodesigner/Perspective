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

namespace Perspective.Core
{
    /// <summary>
    /// A class for skin handling.
    /// </summary>
    public abstract class SkinManagerBase
    {
        private string _dictionaryName = "";

        /// <summary>
        /// Gets or sets the name of the current resource dictionary
        /// </summary>
        protected string DictionaryName
        {
            get { return _dictionaryName; }
            set { _dictionaryName = value; }
        }

        private string _skinFolder;

        /// <summary>
        /// Gets or sets the name of the current skin folder
        /// </summary>
        public string SkinFolder
        {
            get { return _skinFolder; }
            set { _skinFolder = value; }
        }

        private Uri _dictionaryUri;

        /// <summary>
        /// Gets or sets the URI of the current resource dictionary.
        /// </summary>
        protected Uri DictionaryUri
        {
            get { return _dictionaryUri; }
            set { _dictionaryUri = value; }
        }

        /// <summary>
        /// Property to override to specify the assembly name of the inherited class (i.e. "Perspective.Wpf3D")
        /// </summary>
        /// <returns></returns>
        protected abstract string AssemblyName { get; }
        
        /// <summary>
        /// Loads a resource dictionary from a given assembly.
        /// </summary>
        /// <param name="dictionaryFileName">Name of the dictionary file (i.e. "Skins/Perspective" without .xaml extension)</param>
        /// <returns>A ResourceDictionary object.</returns>
        public ResourceDictionary DictionaryFromAssembly(string dictionaryFileName)
        {
            System.Uri uri = new System.Uri(
                String.Format(@"/{0};component/{1}.xaml", AssemblyName, dictionaryFileName),
                System.UriKind.Relative);
            ResourceDictionary rd = new ResourceDictionary();
            rd.Source = uri;
            return rd;
            // return (ResourceDictionary)Application.LoadComponent(uri);
        }

        /// <summary>
        /// Loads a given Dictionary at the Application level.
        /// </summary>
        /// <param name="dictionaryFolderName">Name of the dictionary folder (i.e. "Skins/Perspective")</param>
        /// <param name="dictionaryFileName">Name of the dictionary file (i.e. "Resources" without .xaml extension)</param>
        public void LoadDictionary(string dictionaryFolderName, string dictionaryFileName)
        {
            string fileName = System.IO.Path.Combine(dictionaryFolderName, dictionaryFileName);
            ResourceDictionary newDictionary = DictionaryFromAssembly(fileName);
            Collection<ResourceDictionary> dictionaries = Application.Current.Resources.MergedDictionaries;
            foreach (ResourceDictionary rd in dictionaries)
            {
                if (rd.Source == newDictionary.Source)
                {
                    dictionaries.Remove(rd);
                    break;
                }
            }
            
            dictionaries.Add(newDictionary);
            this._dictionaryUri = newDictionary.Source;

            EventHandler<EventArgs> handler = _dictionaryChanged;
            if (handler != null)
            {
                EventArgs e = new EventArgs();
                handler(this, e);
            }

            _dictionaryName = dictionaryFileName;
        }

        /// <summary>
        /// Loads a given skin.
        /// </summary>
        /// <param name="skinFolderName">Name of the skin folder (i.e. "Skins/Perspective")</param>
        /// <param name="dictionaryFileName">Name of the dictionary file (i.e. "Resources" without .xaml extension)</param>
        public virtual void LoadSkin(string skinFolderName, string dictionaryFileName)
        {
            _skinFolder = skinFolderName; // must be called before LoadDictionary (may be used in DictionaryChanged event handlers)
            LoadDictionary(skinFolderName, dictionaryFileName);
        }

        /// <summary>
        /// Loads a skin from the default skin folder and/or the default dictionary name.
        /// </summary>
        /// <param name="skinName">Name of the skin</param>
        public abstract void LoadSkin(string skinName);

        /// <summary>
        /// Loads the default skin.
        /// </summary>
        public abstract void LoadDefaultSkin();
        
        private event EventHandler<EventArgs> _dictionaryChanged;

        /// <summary>
        /// Occurs when the current skin has changed.
        /// </summary>
        public event EventHandler<EventArgs> DictionaryChanged
        {
            add
            {
                _dictionaryChanged += value;
            }

            remove
            {
                _dictionaryChanged -= value;
            }
        }
    }
}
