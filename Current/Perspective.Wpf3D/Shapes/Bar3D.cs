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
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Perspective.Wpf3D.Primitives;
using Perspective.Wpf3D.Sculptors;

namespace Perspective.Wpf3D.Shapes
{
    /// <summary>
    /// A 3D bar element.
    /// By default, the direction of the bar is the Z axis, and the length is 1.0.
    /// Default radius is 1.0.
    /// </summary>
    public class Bar3D : PolygonalElement3D
    {
        /// <summary>
        /// Static constructor.
        /// No default back material for 3D closed shapes.
        /// </summary>
        static Bar3D()
        {
            BackMaterialProperty.OverrideMetadata(
                typeof(Bar3D),
                new PropertyMetadata(
                    null, 
                    VisualPropertyChanged));
        }

        // private BarSculptor _sculptor = new BarSculptor();

        /// <summary>
        /// Called by UIElement3D.InvalidateModel() to update the 3D model.
        /// </summary>
        protected override void OnUpdateModel()
        {
            Sculptor = new BarSculptor(SideCount, InitialAngle, RoundingRate);
            // _sculptor.Initialize(SideCount, InitialAngle, RoundingRate);
            //_sculptor.BuildMesh();
            //Geometry = _sculptor.Mesh;
            base.OnUpdateModel();
        }
    }
}
