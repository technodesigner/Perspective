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

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// A class to manage the application of a binary resource to a DependencyProperty value.
    /// </summary>
    public class DepPropResourceLink : ResourceLink
    {
        /// <summary>
        /// Initializes a new DepPropResourceLink object.
        /// </summary>
        /// <param name="target">Target object.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="dp">DependencyProperty object.</param>
        public DepPropResourceLink(DependencyObject target, string resourceName, DependencyProperty dp)
        {
            _target = target;
            base.ResourceName = resourceName;
            _property = dp;
        }

        private DependencyObject _target;

        /// <summary>
        /// Gets the target object.
        /// </summary>
        public DependencyObject Target
        {
            get { return _target; }
        }

        private DependencyProperty _property;

        /// <summary>
        /// Gets the dependency property identifier.
        /// </summary>
        public DependencyProperty Property
        {
            get { return _property; }
        }

        /// <summary>
        /// Gets the string resource name.
        /// </summary>
        public new string ResourceName
        {
            get { return base.ResourceName; }
        }

        /// <summary>
        /// Applies the resource to the target's property.
        /// </summary>
        /// <param name="rm">A Resourcemanager object.</param>
        public override void ApplyResourceFrom(System.Resources.ResourceManager rm)
        {
            _target.SetValue(this.Property, rm.GetString(this.ResourceName));
        }
    }
}
