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
using System.Resources;

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// An abstract class to manage the assignment
    /// of a binary resource to something.
    /// </summary>
    public abstract class ResourceLink
    {
        private string _resourceName;

        /// <summary>
        /// Gets or sets the resource name.
        /// </summary>
        protected string ResourceName
        {
            get { return _resourceName; }
            set { _resourceName = value; }
        }

        /// <summary>
        /// Applies the resource.
        /// </summary>
        /// <param name="rm">A Resourcemanager object.</param>
        public abstract void ApplyResourceFrom(ResourceManager rm);

    }
}
