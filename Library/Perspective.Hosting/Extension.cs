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
using System.ComponentModel.Composition;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Perspective.Hosting
{
    /// <summary>
    /// Represents an extension for the Perspective.exe application.
    /// </summary>
    [InheritedExport]
    public abstract class Extension : ItemBase
    {
        public Extension()
        {
            LoadIconsDictionary();
        }

        /// <summary>
        /// Property to override to specify the assembly name of the inherited class (i.e. "Perspective.Wpf3D")
        /// </summary>
        /// <returns></returns>
        public abstract string AssemblyName { get; }

        private void LoadIconsDictionary()
        {
            System.Uri uri = new System.Uri(
                String.Format(@"/{0};component/icons.xaml", AssemblyName),
                System.UriKind.Relative);
            ResourceDictionary newDictionary = new ResourceDictionary();
            newDictionary.Source = uri;

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
        }

        /// <summary>
        /// Property to override to get the children PageInfos collection.
        /// </summary>
        public abstract List<PageInfo> PageInfos
        {
            get;
        }
    }
}
