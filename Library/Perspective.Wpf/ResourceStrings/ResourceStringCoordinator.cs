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
using System.Windows;

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// A class to manage the localization process of elements without inheritance context (i.e. ContextMenu),
    /// or to manage localization by code.
    /// Used by ResourceString markup extension, and by ResourceStringManager.
    /// </summary>
    internal static class ResourceStringCoordinator
    {
        private static Dictionary<string, ResourceSetManager> _managers = new Dictionary<string, ResourceSetManager>();

        private static ResourceSetManager GetManager(string assemblyName, string baseName)
        {
            ResourceSetManager rsm;

            string fullBaseName = assemblyName + "." + baseName;

            if (_managers.ContainsKey(fullBaseName))
            {
                rsm = _managers[fullBaseName];
            }
            else
            {
                rsm = new ResourceSetManager(
                    fullBaseName,
                    assemblyName);
            }
            return rsm;
        }

        /// <summary>
        /// Returns a resource string value
        /// and registers the property 
        /// so It may be refreshed
        /// (by ResourceStringDecorator.Refresh()) after a current culture change
        /// </summary>
        /// <param name="d">The object which owns the property.</param>
        /// <param name="property">The property object. It may be a DependencyProperty (WPF property) or a PropertyInfo (CLR property).</param>
        /// <param name="assemblyName">The name of the assembly containing the baseName resx file for localization.</param>
        /// <param name="baseName">The resx file base name for localization.</param>
        /// <param name="resourceName">The name of the resource.</param>
        /// <returns>The resource string value.</returns>
        internal static string InitializeValue(
            DependencyObject d,
            object property,
            string assemblyName,
            string baseName,
            string resourceName)
        {
            string value = "";
            ResourceSetManager rsm = GetManager(assemblyName, baseName);
            value = rsm.ResourceManager.GetString(resourceName);

            // Register (links) the property value to the ResourceSetManager.LocalizedObjects collection
            // so the ResourceStringCoordinator may refresh the property value 
            // (by the Refresh() method) after a current culture change

            if (property is DependencyProperty)
            {
                DepPropResourceLink dprl = new DepPropResourceLink(
                    d, resourceName, (DependencyProperty)property);
                rsm.LocalizedObjects.Add(dprl);
            }

            if (property is PropertyInfo)
            {
                ClrPropResourceLink cprl = new ClrPropResourceLink(
                    d, resourceName, (PropertyInfo)property);
                rsm.LocalizedObjects.Add(cprl);
            }
            return value;
        }

        /// <summary>
        /// Returns a resource string value (without any registration).
        /// </summary>
        /// <param name="assemblyName">The name of the assembly containing the baseName resx file for localization.</param>
        /// <param name="baseName">The resx file base name for localization.</param>
        /// <param name="resourceName">The name of the resource.</param>
        /// <returns>The resource string value.</returns>
        internal static string GetString(
            string assemblyName,
            string baseName,
            string resourceName)
        {
            ResourceSetManager rsm = GetManager(assemblyName, baseName);
            return rsm.ResourceManager.GetString(resourceName);
        }
    }
}
