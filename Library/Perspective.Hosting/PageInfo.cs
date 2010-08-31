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
    /// Represents the metadata associated with a page of an extension assembly.
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

        private Extension _extension;

        /// <summary>
        /// Gets the extension container.
        /// </summary>
        public Extension Extension
        {
            get { return _extension; }
        }

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
            }
        }

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        public override string AssemblyName
        {
            get
            {
                return _extension.AssemblyName;
            }
        }
    }
}
