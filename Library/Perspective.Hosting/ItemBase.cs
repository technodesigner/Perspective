﻿//------------------------------------------------------------------
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

        /// <summary>
        /// Property to override to specify the assembly name of the inherited class (i.e. "PerspectiveDemo.Wpf3D")
        /// </summary>
        /// <returns></returns>
        public abstract string AssemblyName { get; }
    }
}
