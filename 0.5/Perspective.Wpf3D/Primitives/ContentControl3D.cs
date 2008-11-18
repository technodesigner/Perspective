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
using System.Windows.Media.Media3D;
using System.ComponentModel;
using System.Windows.Markup;
using System.Xml;
using System.Collections;


namespace Perspective.Wpf3D.Primitives
{
    /// <summary>
    /// Represents a 3D control with a single piece of content, loaded from a skin.
    /// </summary>
    public abstract class ContentControl3D : UIElement3D
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static ContentControl3D()
        {
            FocusableProperty.OverrideMetadata(
                typeof(ContentControl3D),
                new UIPropertyMetadata(true));
        }

        /// <summary>
        /// Constructor.
        /// Loads a default content.
        /// </summary>
        protected ContentControl3D()
        {
            SkinManager.Current.DictionaryChanged += SkinManager_SkinChanged;
            Content = LoadDefaultContent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loadDefaultContent">Indicates if a default content has to be loaded.</param>
        protected ContentControl3D(bool loadDefaultContent)
        {
            SkinManager.Current.DictionaryChanged += SkinManager_SkinChanged;
            if (loadDefaultContent)
            {
                Content = LoadDefaultContent();
            }
        }

        /// <summary>
        /// Gets or sets the content of a ContentControl3D.
        /// This is a dependency property.
        /// </summary>
        public UIElement3D Content
        {
            get { return (UIElement3D)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// Identifies the Content dependency property.
        /// </summary>        
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                "Content",
                typeof(UIElement3D),
                typeof(ContentControl3D),
                new UIPropertyMetadata(
                    null,
                    OnContentChanged,
                    CoerceContent));

        /// <summary>
        /// Callback called when the Content property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContentControl3D c = (ContentControl3D)d;
            if (e.OldValue != null)
            {
                c.RemoveVisual3DChild((UIElement3D)e.OldValue);
            }
            c.AddVisual3DChild((UIElement3D)e.NewValue);
        }

        private static object CoerceContent(DependencyObject d, object value)
        {
            ContentControl3D c = (ContentControl3D)d;
            if (c._contentJustLoadedFromSkin || c.OverridesDefaultSkin)
            {
                c._contentJustLoadedFromSkin = false;
                return value;
            }
            else
            {
                return c.Content;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the control uses a default skin or not. 
        /// This is a dependency property.
        /// </summary>
        public bool OverridesDefaultSkin
        {
            get { return (bool)GetValue(OverridesDefaultSkinProperty); }
            set { SetValue(OverridesDefaultSkinProperty, value); }
        }

        /// <summary>
        /// Identifies the OverridesDefaultSkin dependency property.
        /// </summary>
        public static readonly DependencyProperty OverridesDefaultSkinProperty =
            DependencyProperty.Register(
                "OverridesDefaultSkin",
                typeof(bool),
                typeof(ContentControl3D),
                new UIPropertyMetadata(false));

        private void SkinManager_SkinChanged(object sender, EventArgs e)
        {
            if (!OverridesDefaultSkin)
            {
                Content = LoadDefaultContent();
            }
        }

        private INameScope _namescope;
        private bool _contentJustLoadedFromSkin;

        private UIElement3D LoadDefaultContent()
        {
            String contentFileRelativeName;
            Hashtable h = (Hashtable)Application.Current.TryFindResource("ContentFiles");
            // if (h != null)
            if ((h != null) && (h[ContentFileName] != null))
            {
                //contentFileRelativeName = System.IO.Path.Combine(
                //    SkinManager.Current.DefaultSkinBaseFolder,
                //    (string)h[ContentFileName]);
                contentFileRelativeName = System.IO.Path.Combine(
                    SkinManager.Current.SkinFolder,
                    (string)h[ContentFileName]);
            }
            else
            {
                //contentFileRelativeName = System.IO.Path.Combine(
                //    SkinManager.Current.SkinFolder,
                //    ContentFileName);
                contentFileRelativeName = System.IO.Path.Combine(
                    SkinManager.Current.DefaultSkinBaseFolder,
                    ContentFileName);
            }
            //System.Uri uri = new System.Uri(
            //    String.Format(
            //        @"/Perspective.Wpf3D;component/{0}.xaml",
            //        System.IO.Path.Combine(SkinManager.Current.SkinFolder,
            //        ContentFileName)),
            //    System.UriKind.Relative);

            System.Uri uri = new System.Uri(
                String.Format(
                    @"/Perspective.Wpf3D;component/{0}.xaml",
                    contentFileRelativeName),
                System.UriKind.Relative);

            // The resource has to be marked as x:Shared="false"
            UIElement3D content = (UIElement3D)Application.LoadComponent(uri);

            _namescope = NameScope.GetNameScope(content); // must occur before assigning Content
            _contentJustLoadedFromSkin = true; // must occur before assigning Content
            return content;
        }
        
        /// <summary>
        /// Gets the name (without path and extension) of the XAML file that describes the appearence of the control.
        /// </summary>
        protected abstract string ContentFileName { get;  }

        /// <summary>
        /// Overrides Visual3D.GetVisual3DChild(int index)
        /// </summary>
        protected override Visual3D GetVisual3DChild(int index)
        {
            return Content;
        }

        /// <summary>
        /// Overrides Visual3D.Visual3DChildrenCount
        /// </summary>
        protected override int Visual3DChildrenCount
        {
            get
            {
                if (Content == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        /// <summary>
        /// Returns an object that has the provided identifying XAML name.
        /// </summary>
        /// <param name="name">The name identifier for the object being requested.</param>
        /// <returns>The object, if found. Returns nullNothingnullptra null reference if no object of that name was found.</returns>
        protected object FindName(string name)
        {
            if (_namescope != null)
            {
                return _namescope.FindName(name);
            }
            return null;
        }
        
        /// <summary>
        /// Indicates if a drag and drop operation occurs on the control.
        /// This is a read-only dependency property.
        /// </summary>
        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
        }

        /// <summary>
        /// Key of the IsDragging dependency property.
        /// </summary>
        protected static readonly DependencyPropertyKey IsDraggingPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsDragging",
                typeof(bool),
                typeof(ContentControl3D),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the IsDragging dependency property.
        /// </summary>
        public static readonly DependencyProperty IsDraggingProperty = IsDraggingPropertyKey.DependencyProperty;
    }
}
