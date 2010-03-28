using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace Perspective.Hosting
{
    /// <summary>
    /// Base class for extension elements with name and icon
    /// </summary>
    public abstract class ItemBase
    {
        private string _title;

        /// <summary>
        /// Gets or sets the page info title.
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                // NotifyPropertyChanged("Title");
            }
        }

        private string _iconKey;

        /// <summary>
        /// Gets or sets the page info icon resource key.
        /// </summary>
        public string IconKey
        {
            get
            {
                return _iconKey;
            }
            set
            {
                _iconKey = value;
                // NotifyPropertyChanged("IconKey");
            }
        }

        /// <summary>
        /// Gets a DrawingImage containing the icon.
        /// </summary>
        public DrawingImage Icon
        {
            get
            {
                DrawingImage di = null;
                Object resource = Application.Current.Resources[IconKey];
                if (resource != null)
                {
                    if (resource is DrawingBrush)
                    {
                        DrawingBrush brush = resource as DrawingBrush;
                        di = new DrawingImage(brush.Drawing);
                    }
                    else if (resource is DrawingImage)
                    {
                        di = resource as DrawingImage;
                    }
                }
                return di;
            }
        }
    }
}
