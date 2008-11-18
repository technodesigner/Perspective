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
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Generic;

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// A Decorator class to specify the resx file base name for localization.
    /// To use with ResourceString markup extension.
    /// </summary>
    public class ResourceStringDecorator : Decorator
    {
        private List<ResourceLink> _localizedChildren;
        
        /// <summary>
        /// Initializes a new instance of ResourceStringDecorator.
        /// </summary>
        public ResourceStringDecorator()
        {
            // Registers Perspective.Wpf resources
            ResourceAssembly.Register();

            // LocalizedChildren refers to an instance field
            // So that it works fine when ResourceStringDecorator are imbricated
            // and that's why ResourceStringDecorator doesn't use ResourceStringCoordinator
            _localizedChildren = new List<ResourceLink>();
            SetValue(LocalizedChildrenProperty, _localizedChildren);

            CultureManager.Current.UICultureChanged += CultureManager_GlobalCultureChanged;
        }

        private void CultureManager_GlobalCultureChanged(object sender, Perspective.Core.ChangedEventArgs<string> e)
        {
            this.Refresh();
        }

        /// <summary>
        /// Gets or sets the name of the assembly containing the BaseName resx file for localization.
        /// </summary>
        public string AssemblyName
        {
            get { return (string)GetValue(AssemblyNameProperty); }
            set { SetValue(AssemblyNameProperty, value); }
        }

        /// <summary>
        /// Identifies the AssemblyName dependency property.
        /// </summary>
        public static readonly DependencyProperty AssemblyNameProperty =
            DependencyProperty.Register(
                "AssemblyName", 
                typeof(string), 
                typeof(ResourceStringDecorator),
                new UIPropertyMetadata(""));

                // 0.9.2
                //new UIPropertyMetadata(
                //    "",
                //    AssemblyNamePropertyChanged));

        // <summary>
        // Callback called when the AssemblyName property's value has changed.
        // </summary>
        // <param name="d">Sender object</param>
        // <param name="e">Callback arguments</param>
        //private static void AssemblyNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    d.InvalidateProperty(AssemblyNameProperty);
        //}

        /// <summary>
        /// Gets or sets the resx file base name for localization.
        /// </summary>
        public string BaseName
        {
            get { return (string)GetValue(BaseNameProperty); }
            set { SetValue(BaseNameProperty, value); }
        }

        /// <summary>
        /// Identifies the BaseName dependency property.
        /// </summary>
        public static readonly DependencyProperty BaseNameProperty =
            DependencyProperty.Register(
                "BaseName",
                typeof(string),
                typeof(ResourceStringDecorator),
                new FrameworkPropertyMetadata(
                    "",
                    BaseNamePropertyChanged
                    ));

        /// <summary>
        /// Callback called when the BaseName property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void BaseNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(d))
            {
                ((ResourceStringDecorator)d).ApplyResources((string)e.NewValue);
            }
        }

        private void ApplyResources(string baseName)
        {
            string fullBaseName = AssemblyName + "." + baseName;
            Assembly assembly = GetAssembly(AssemblyName);

            ResourceManager rm = new ResourceManager(
                fullBaseName,
                assembly);
            SetValue(ResourceManagerProperty, rm);

            // Refreshes the linked property values (if they exist).
            foreach (ResourceLink rl in _localizedChildren)
            {
                rl.ApplyResourceFrom(rm);
            }
        }

        private static Assembly GetAssembly(string name)
        {
            return CultureManager.Current.ResourceAssemblies[name];
        }

        /// <summary>
        /// Re-loads the ressources and refreshes the linked property values.
        /// May be called i.e. after a change of the current culture.
        /// </summary>
        public void Refresh()
        {
            ApplyResources(this.BaseName);
        }

        /// <summary>
        /// Gets the ResourceManager object.
        /// This is an inheritable attached dependency property.
        /// </summary>
        /// <param name="obj">A descendant dependency object.</param>
        /// <returns></returns>
        public static ResourceManager GetResourceManager(DependencyObject obj)
        {
            return (ResourceManager)obj.GetValue(ResourceManagerProperty);
        }

        /// <summary>
        /// Identifies the ResourceManager inheritable attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ResourceManagerProperty =
            DependencyProperty.RegisterAttached(
                "ResourceManager", 
                typeof(ResourceManager), 
                typeof(ResourceStringDecorator), 
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Gets the collection of the localized children.
        /// LocalizedChildren is an inheritable attached dependency property.
        /// </summary>
        /// <param name="obj">A descendant dependency object.</param>
        /// <returns></returns>
        public static List<ResourceLink> GetLocalizedChildren(DependencyObject obj)
        {
            return (List<ResourceLink>)obj.GetValue(LocalizedChildrenProperty);
        }

        /// <summary>
        /// Identifies the LocalizedChildren inheritable attached dependency property.
        /// </summary>
        public static readonly DependencyProperty LocalizedChildrenProperty =
            DependencyProperty.RegisterAttached(
                "LocalizedChildren",
                typeof(List<ResourceLink>),
                typeof(ResourceStringDecorator),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Returns a resource string value
        /// and registers the property 
        /// so It may be refreshed
        /// (by ResourceStringDecorator.Refresh()) after a current culture change
        /// </summary>
        /// <param name="d">The object which owns the property.</param>
        /// <param name="property">The property object. It may be a DependencyProperty (WPF property) or a PropertyInfo (CLR property).</param>
        /// <param name="resourceName">The name of the resource.</param>
        /// <returns>The resource string value.</returns>
        public static string InitializeValue(
            DependencyObject d,
            object property,
            string resourceName)
        {
            string value = "";
            ResourceManager rm = ResourceStringDecorator.GetResourceManager(d);
            if (rm != null)
            {
                value = rm.GetString(resourceName);

                // Register (links) the property value to the ResourceStringDecorator.LocalizedChildren dictionary
                // so the ResourceStringDecorator may refresh the property value 
                // (by ResourceStringDecorator.Refresh()) after a current culture change

                List<ResourceLink> localizedChildren = ResourceStringDecorator.GetLocalizedChildren(d);
                if (localizedChildren != null)
                {
                    if (property is DependencyProperty)
                    {
                        DepPropResourceLink dprl = new DepPropResourceLink(
                            d, resourceName, (DependencyProperty)property);
                        localizedChildren.Add(dprl);
                    }

                    if (property is PropertyInfo)
                    {
                        ClrPropResourceLink cprl = new ClrPropResourceLink(
                            d, resourceName, (PropertyInfo)property);
                        localizedChildren.Add(cprl);
                    }
                }
            }
            return value;
        }




        //public static string InitializeValue(
        //    DependencyObject d,
        //    object property,
        //    ResourceManager rm,
        //    string resourceName)
        //{
        //    string value = "";
        //    // ResourceManager rm = ResourceStringDecorator.GetResourceManager(d);
        //    if (rm != null)
        //    {
        //        value = rm.GetString(resourceName);

        //        // Register (links) the property value to the ResourceStringDecorator.LocalizedChildren dictionary
        //        // so the ResourceStringDecorator may refresh the property value 
        //        // (by ResourceStringDecorator.Refresh()) after a current culture change

        //        List<ResourceLink> localizedChildren = ResourceStringDecorator.GetLocalizedChildren(d);
        //        if (localizedChildren != null)
        //        {
        //            if (property is DependencyProperty)
        //            {
        //                DepPropResourceLink dprl = new DepPropResourceLink(
        //                    d, resourceName, (DependencyProperty)property);
        //                localizedChildren.Add(dprl);
        //            }

        //            if (property is PropertyInfo)
        //            {
        //                ClrPropResourceLink cprl = new ClrPropResourceLink(
        //                    d, resourceName, (PropertyInfo)property);
        //                localizedChildren.Add(cprl);
        //            }
        //        }
        //    }
        //    return value;
        //}
    }
}
