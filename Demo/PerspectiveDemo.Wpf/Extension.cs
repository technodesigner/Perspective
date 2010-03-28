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
using Perspective.Hosting;

namespace PerspectiveDemo.Wpf
{
    /// <summary>
    /// Represents the WPF demo extension for the Perspective.exe application.
    /// </summary>
    public class Extension : Perspective.Hosting.Extension
    {
        private const string _assemblyName = "PerspectiveDemo.Wpf";
        private const string _title = "Démo Perspective WPF";
        private const string _iconKey = "PencilIcon";
        private List<PageInfo> _pageInfos;

        /// <summary>
        /// Initializes a new instance of Extension.
        /// </summary>
        public Extension()
            : base()
        {
            Title = _title;
            IconKey = _iconKey;
            _pageInfos = new List<PageInfo>
            {
                new PageInfo(this)
                {
                    Title = "DPI scaling",
                    IconKey = "PencilIcon",
                    PartialClassName = "View/DpiScalingDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Page 2",
                    IconKey = "PencilBlueIcon",
                    PartialClassName = "View/RangeControlDemo.xaml"
                }
            };
        }

        /// <summary>
        /// Gets the children PageInfos collection.
        /// </summary>
        public override List<PageInfo> PageInfos
        {
            get
            {
                return _pageInfos;
            }
        }

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        public override string AssemblyName
        {
            get
            {
                return _assemblyName;
            }
        }
    }
}
