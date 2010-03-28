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
using System.Windows.Media;
using System.Windows;

namespace Perspective.Hosting
{
    /// <summary>
    /// Represents the metadata associated with a WPF page of an extension assembly.
    /// </summary>
    public class PageInfo : ItemBase
    // public class PageInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new PageInfo instance.
        /// </summary>
        /// <param name="extension">The Extension container.</param>
        public PageInfo(Extension extension)
        {
            _extension = extension;
        }

        //private string _title;

        ///// <summary>
        ///// Gets or sets the page info title.
        ///// </summary>
        //public string Title
        //{
        //    get
        //    {
        //        return _title;
        //    }
        //    set
        //    {
        //        _title = value;
        //        // NotifyPropertyChanged("Title");
        //    }
        //}

        //private string _iconKey;

        ///// <summary>
        ///// Gets or sets the page info icon resource key.
        ///// </summary>
        //public string IconKey
        //{
        //    get
        //    {
        //        return _iconKey;
        //    }
        //    set
        //    {
        //        _iconKey = value;
        //        // NotifyPropertyChanged("IconKey");
        //    }
        //}

        private Extension _extension;

        /// <summary>
        /// Gets the extension container.
        /// </summary>
        public Extension Extension
        {
            get { return _extension; }
        }

        ///// <summary>
        ///// Gets a DrawingImage containing the icon.
        ///// </summary>
        //public DrawingImage Icon
        //{
        //    get
        //    {
        //        DrawingImage di = null;
        //        Object resource = Application.Current.Resources[IconKey];
        //        if (resource != null)
        //        {
        //            if (resource is DrawingBrush)
        //            {
        //                DrawingBrush brush = resource as DrawingBrush;
        //                di = new DrawingImage(brush.Drawing);
        //            }
        //            else if (resource is DrawingImage)
        //            {
        //                di = resource as DrawingImage;
        //            }
        //        }
        //        return di;
        //    }
        //}

        private Uri _uri = null;

        /// <summary>
        /// Gets the relative URI of the page.
        /// </summary>
        public Uri Uri
        {
            get
            {
                if (_uri == null)
                {
                    _uri = new System.Uri(
                        String.Format(@"/{0};component/{1}", _extension.AssemblyName, _partialClassName),
                        System.UriKind.Relative);
                }
                return _uri;
            }
        }

        private string _partialClassName;

        /// <summary>
        /// Gets or sets the class name of the page to load, including the complementary namespace (but not the full namespace).
        /// </summary>
        public string PartialClassName
        {
            get
            {
                return _partialClassName;
            }
            set
            {
                _partialClassName = value;
                _uri = null;
                // NotifyPropertyChanged("PartialClassName");
            }
        }

        //#region INotifyPropertyChanged Members

        ///// <summary>
        ///// Occurs when a property value changes.
        ///// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;

        ///// <summary>
        ///// Method called when a property value changes.
        ///// </summary>
        //protected void NotifyPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        //#endregion
    }
}
