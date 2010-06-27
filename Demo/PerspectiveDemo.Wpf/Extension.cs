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
        private const string _title = "Perspective WPF";
        private const string _iconKey = "Perspective2DIcon";

        private static string _assemblyName = "PerspectiveDemo.Wpf";

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        public static string AssemblyNameConst
        {
            get { return Extension._assemblyName; }
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

        private List<PageInfo> _pageInfos;

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
        /// Initializes a new instance of Extension.
        /// </summary>
        public Extension()
            : base()
        {
            Title = _title;
            IconKey = _iconKey;
            SortOrder = 100;
            _pageInfos = new List<PageInfo>
            {
                new PageInfo(this)
                {
                    Title = "Shapes",
                    IconKey = _iconKey,
                    PartialClassName = "View/ShapeDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Matrix",
                    IconKey = _iconKey,
                    PartialClassName = "View/MatrixDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Range controls",
                    IconKey = _iconKey,
                    PartialClassName = "View/RangeControlDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "BeePanel / BeeGrid",
                    IconKey = _iconKey,
                    PartialClassName = "View/BeePanelDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "MayaEase animation",
                    IconKey = _iconKey,
                    PartialClassName = "View/MayaEaseDemo.xaml"
                }
            };
            ResourceAssembly.Register();
        }
    }
}
