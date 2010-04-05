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

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// A class to load string resources by code.
    /// </summary>
    public class ResourceStringManager
    {
        private string _assemblyName;
        private string _baseName;

        /// <summary>
        /// Initializes a new instance of ResourceStringManager.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly containing the baseName resx file for localization.</param>
        /// <param name="baseName">The resx file base name for localization.</param>
        public ResourceStringManager(
            string assemblyName,
            string baseName)
        {
            _assemblyName = assemblyName;
            _baseName = baseName;
        }

        /// <summary>
        /// Returns the value of the specified String resource.
        /// </summary>
        /// <param name="resourceName">The name of the resource to get.</param>
        /// <returns>The value of the resource localized for the caller's current culture settings. If a match is not possible, a null reference is returned.</returns>
        public string GetString(string resourceName)
        {
            return ResourceStringCoordinator.GetString(
                _assemblyName,
                _baseName,
                resourceName);
        }
    }
}
