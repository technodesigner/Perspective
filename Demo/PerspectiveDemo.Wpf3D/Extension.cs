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
using Perspective.Hosting;
using System.Collections.Generic;
using Perspective.Wpf.ResourceStrings;

namespace PerspectiveDemo.Wpf3D
{
    /// <summary>
    /// Represents the 3D demo extension for the Perspective.exe application.
    /// </summary>
    public class Extension : Perspective.Hosting.Extension
    {
        private const string _title = "Perspective 3D";
        private const string _iconKey = "Perspective3DIcon";

        private static string _assemblyName = "PerspectiveDemo.Wpf3D";

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
            SortOrder = 110;
            ResourceAssembly.Register();
            ResourceStringManager resourceStringManager = new ResourceStringManager(
                AssemblyNameConst,
                "Strings.Extension");
            _pageInfos = new List<PageInfo>
            {
                new PageInfo(this)
                {
                    Title = "XyzAxis3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/XyzAxis3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Box3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Box3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Box3D + image",
                    IconKey = _iconKey,
                    PartialClassName = "View/Box3DImageDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = resourceStringManager.GetString("OpenBox3D"),
                    IconKey = _iconKey,
                    PartialClassName = "View/Box3DOpenDemo.xaml"
                },            
                new PageInfo(this)
                {
                    Title = "Box3D + Transform3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Box3DTransformDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = resourceStringManager.GetString("Interactivity"),
                    IconKey = _iconKey,
                    PartialClassName = "View/InteractivityDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Square3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Square3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Square3D + animation",
                    IconKey = _iconKey,
                    PartialClassName = "View/Square3DAnimationDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Square3D + image",
                    IconKey = _iconKey,
                    PartialClassName = "View/Square3DImageDemo.xaml"
                },                
                new PageInfo(this)
                {
                    Title = "Polygon3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Polygon3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Polygon3D + image",
                    IconKey = _iconKey,
                    PartialClassName = "View/Polygon3DImageDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Bar3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Bar3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = resourceStringManager.GetString("Cylinder"),
                    IconKey = _iconKey,
                    PartialClassName = "View/CylinderDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Bar3D + image",
                    IconKey = _iconKey,
                    PartialClassName = "View/Bar3DImageDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Conical3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Conical3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Conical3D + image",
                    IconKey = _iconKey,
                    PartialClassName = "View/Conical3DImageDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Ring3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Ring3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Spherical3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Spherical3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Spherical3D + image",
                    IconKey = _iconKey,
                    PartialClassName = "View/Spherical3DImageDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Spherical3D + Transform3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Spherical3DTransformDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = resourceStringManager.GetString("BubbleChart"),
                    IconKey = _iconKey,
                    PartialClassName = "View/BubbleChartDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Isocahedron3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Isocahedron3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "TruncatedIsocahedron3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/TruncatedIsocahedron3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Football3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Football3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = resourceStringManager.GetString("PieSlice"),
                    IconKey = _iconKey,
                    PartialClassName = "View/PieSliceDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Gyroscope3D.xaml",
                    IconKey = _iconKey,
                    PartialClassName = "View/Gyroscope3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Arrow3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Arrow3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = resourceStringManager.GetString("Range3D"),
                    IconKey = _iconKey,
                    PartialClassName = "View/ButtonFaderKnob3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = resourceStringManager.GetString("Mixer"),
                    IconKey = _iconKey,
                    PartialClassName = "View/MixerDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = resourceStringManager.GetString("CustomKnob3D"),
                    IconKey = _iconKey,
                    PartialClassName = "View/CustomKnob3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Matrix3D",
                    IconKey = _iconKey,
                    PartialClassName = "View/Matrix3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "House3D + Export",
                    IconKey = _iconKey,
                    PartialClassName = "View/HouseDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "3D panels",
                    IconKey = _iconKey,
                    PartialClassName = "View/Panel3DDemo.xaml"
                }
            };
        }
    }
}
