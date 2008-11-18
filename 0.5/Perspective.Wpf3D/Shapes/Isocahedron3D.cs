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
    /// A 3D isocahedron element.
    /// Default radius is 1.0.
    /// </summary>
    public class Isocahedron3D : GeometryElement3D
    {

        /// <summary>
        /// Indicates if the isocahedron is truncated.
        /// Default value is false
        /// </summary>
        public bool Truncated
        {
            get { return (bool)GetValue(TruncatedProperty); }
            set { SetValue(TruncatedProperty, value); }
        }

        /// <summary>
        /// Identifies the Truncated dependency property.
        /// </summary>
        public static readonly DependencyProperty TruncatedProperty =
            DependencyProperty.Register(
                "Truncated",
                typeof(bool),
                typeof(Isocahedron3D),
                new UIPropertyMetadata(false));

        /// <summary>
        /// Called by UIElement3D.InvalidateModel() to update the 3D model.
        /// </summary>
        protected override void OnUpdateModel()
        {
            Sculptor sculptor;
            if (Truncated)
            {
                sculptor = new TruncatedIsocahedronSculptor();
            }
            else
            {
                sculptor = new IsocahedronSculptor();
            }
            sculptor.BuildMesh();
            Geometry = sculptor.Mesh;
            base.OnUpdateModel();
        }
    }
}
