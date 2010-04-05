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

namespace PerspectiveDemo.Wpf3D
{
    /// <summary>
    /// Represents the 3D demo extension for the Perspective.exe application.
    /// </summary>
    public class Extension : Perspective.Hosting.Extension
    {
        private const string _assemblyName = "PerspectiveDemo.Wpf3D";
        private const string _title = "Démo 3D";
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
                    Title = "XyzAxis3D",
                    IconKey = "PencilGreenIcon",
                    PartialClassName = "View/XyzAxis3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Box3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Box3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Box3D + image",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Box3DImageDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Open Box3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Box3DOpenDemo.xaml"
                },            
                new PageInfo(this)
                {
                    Title = "Box3D + Transform3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Box3DTransformDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Interactivity",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/InteractivityDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Square3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Square3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Square3D + animation",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Square3DAnimationDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Square3D + image",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Square3DImageDemo.xaml"
                },                
                new PageInfo(this)
                {
                    Title = "Polygon3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Polygon3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Polygon3D + image",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Polygon3DImageDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Bar3D",
                    IconKey = "PencilGreenIcon",
                    PartialClassName = "View/Bar3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Cylinder",
                    IconKey = "PencilGreenIcon",
                    PartialClassName = "View/CylinderDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Bar3D + image",
                    IconKey = "PencilGreenIcon",
                    PartialClassName = "View/Bar3DImageDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Conical3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Conical3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Conical3D + image",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Conical3DImageDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Ring3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Ring3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Spherical3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Spherical3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Spherical3D + image",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Spherical3DImageDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Spherical3D + Transform3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Spherical3DTransformDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Bubble chart",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/BubbleChartDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Isocahedron3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Isocahedron3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "TruncatedIsocahedron3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/TruncatedIsocahedron3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Football3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Football3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "PieSlice",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/PieSliceDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Gyroscope3D.xaml",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/Gyroscope3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Arrow3D",
                    IconKey = "PencilGreenIcon",
                    PartialClassName = "View/Arrow3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Range3D controls",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/ButtonFaderKnob3DDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Mixer device",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/MixerDemo.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Custom Knob3D",
                    IconKey = "PencilPurpleIcon",
                    PartialClassName = "View/CustomKnob3DDemo.xaml"
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
