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
using System.Windows.Markup;
using System.Windows;
using System.Windows.Media;
using System.Resources;
using System.Reflection;

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// A markup extension to get a resource string from a resx.
    /// To use on a DependencyObject, in the context of a StringResourceDecorator.
    /// </summary>
    [MarkupExtensionReturnType(typeof(String))]
    public class ResourceStringExtension : MarkupExtension
    {
        /// <summary>
        /// Initializes a new instance of StringResourceExtension.
        /// </summary>
        public ResourceStringExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of StringResourceExtension.
        /// </summary>
        /// <param name="name">Name of the resource string.</param>
        public ResourceStringExtension(string name)
        {
            _name = name;
        }

        private string _name;

        /// <summary>
        /// Gets or sets the name of the string resource.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _baseName;

        /// <summary>
        /// Gets or sets the resx file base name for localization.
        /// This property is intended to be used only with elements without inheritance context (i.e. ContextMenu).
        /// In this case, the AssemblyName property must also be set, and The ResourceStringDecorator element is not used.
        /// </summary>
        public string BaseName
        {
            get { return _baseName; }
            set { _baseName = value; }
        }

        private string _assemblyName;

        /// <summary>
        /// Gets or sets the name of the assembly containing the BaseName resx file for localization.
        /// This property is intended to be used only with elements without inheritance context (i.e. ContextMenu).
        /// In this case, the BaseName property must also be set, and The ResourceStringDecorator element is not used.
        /// </summary>
        public string AssemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; }
        }

        /// <summary>
        /// Returns the value of the string resource.
        /// </summary>
        /// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
        /// <returns>The string value.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            string value = "";

            IProvideValueTarget target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (target != null)
            {
                DependencyObject d = (DependencyObject)target.TargetObject;
                if ( d != null)
                {
                    if (!String.IsNullOrEmpty(_assemblyName) && !String.IsNullOrEmpty(_baseName))
                    {
                        value = ResourceStringCoordinator.InitializeValue(
                            d,
                            target.TargetProperty,
                            _assemblyName,
                            _baseName,
                            _name);
                    }
                    else
                    {
                        value = ResourceStringDecorator.InitializeValue(
                            d,
                            target.TargetProperty,
                            _name);
                    }
                }
            }
            return value;
        }
    }
}
