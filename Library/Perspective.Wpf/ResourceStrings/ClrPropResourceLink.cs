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
using System.Reflection;

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// A class to manage the application of a binary resource to a CLR property value.
    /// It uses the reflection model.
    /// </summary>
    public class ClrPropResourceLink : ResourceLink
    {
        /// <summary>
        /// Initializes a new ClrPropResourceLink object.
        /// </summary>
        /// <param name="target">Target object.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="pi">PropertyInfo  object.</param>
        public ClrPropResourceLink(object target, string resourceName, PropertyInfo pi)
        {
            _target = target;
            base.ResourceName = resourceName;
            _property = pi;
        }

        private object _target;

        /// <summary>
        /// Gets the target object.
        /// </summary>
        public object Target
        {
            get { return _target; }
        }

        private PropertyInfo _property;

        /// <summary>
        /// Gets the property identifier.
        /// </summary>
        public PropertyInfo Property
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
        /// It uses the reflection model.
        /// </summary>
        /// <param name="rm">A Resourcemanager object.</param>
        public override void ApplyResourceFrom(System.Resources.ResourceManager rm)
        {
            _property.SetValue(_target, rm.GetString(ResourceName), null);
        }

    }
}
