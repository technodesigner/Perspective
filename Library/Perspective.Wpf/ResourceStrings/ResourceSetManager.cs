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
using System.Text;
using System.Resources;
using System.Reflection;

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// A class to manage the localization process of elements without inheritance context (i.e. ContextMenu).
    /// Used by ResourceStringCoordinator.
    /// </summary>
    internal class ResourceSetManager
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fullBaseName"></param>
        /// <param name="assemblyName"></param>
        internal ResourceSetManager(string fullBaseName, string assemblyName)
        {
            _localizedObjects = new List<ResourceLink>();
            ApplyResources(fullBaseName, assemblyName);
            CultureManager.Current.UICultureChanged += Current_UICultureChanged;
        }

        private void Current_UICultureChanged(object sender, Perspective.Core.ChangedEventArgs<string> e)
        {
            Refresh();
        }

        /// <summary>
        /// Re-loads the ressources and refreshes the linked property values.
        /// May be called i.e. after a change of the current culture.
        /// </summary>
        internal void Refresh()
        {
            ApplyResources(_fullBaseName, _assemblyName);

            // Refreshes the linked property values (if they exist).
            foreach (ResourceLink rl in _localizedObjects)
            {
                rl.ApplyResourceFrom(_resourceManager);
            }
        }

        private void ApplyResources(string fullBaseName, string assemblyName)
        {
            _fullBaseName = fullBaseName;
            _assemblyName = assemblyName;
            Assembly assembly = CultureManager.Current.ResourceAssemblies[assemblyName];
            _resourceManager = new ResourceManager(
                fullBaseName,
                assembly);

            // Refreshes the linked property values (if they exist).
            foreach (ResourceLink rl in _localizedObjects)
            {
                rl.ApplyResourceFrom(_resourceManager);
            }
        }

        private string _fullBaseName;

        /// <summary>
        /// Gets or sets the resx file full base name for localization.
        /// </summary>
        internal string FullBaseName
        {
            get { return _fullBaseName; }
        }

        private string _assemblyName;

        /// <summary>
        /// Gets the name of the assembly containing the BaseName resx file for localization.
        /// </summary>
        internal string AssemblyName
        {
            get { return _assemblyName; }
        }

        ResourceManager _resourceManager;

        /// <summary>
        /// Gets the ResourceManager object.
        /// </summary>
        internal ResourceManager ResourceManager
        {
            get { return _resourceManager; }
        }

        private List<ResourceLink> _localizedObjects;

        /// <summary>
        /// Gets the collection of the localized children.
        /// </summary>
        internal List<ResourceLink> LocalizedObjects
        {
            get { return _localizedObjects; }
        }
    }
}
