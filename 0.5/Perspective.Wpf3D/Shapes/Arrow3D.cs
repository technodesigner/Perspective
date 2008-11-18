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
    /// A 3D arrow element.
    /// By default, the direction of the arrow is the Z axis, and the length is 1.0.
    /// Default radius of the body is 0.1.
    /// Default radius of the head is 0.2.
    /// </summary>
    public class Arrow3D : GeometryElement3D
    {
        private ArrowSculptor _sculptor = new ArrowSculptor();

        /// <summary>
        /// Called by UIElement3D.InvalidateModel() to update the 3D model.
        /// </summary>
        protected override void OnUpdateModel()
        {
            _sculptor.Initialize(Length);
            _sculptor.BuildMesh();
            Geometry = _sculptor.Mesh;
            base.OnUpdateModel();
        }

        /// <summary>
        /// Gets or sets the axis length.
        /// </summary>
        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        /// <summary>
        /// Identifies the Length dependency property.
        /// Default value is 1.0.
        /// </summary>
        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register(
                "Length",
                typeof(double),
                typeof(Arrow3D),
                new PropertyMetadata(
                    ArrowSculptor.DefaultLength,
                    VisualPropertyChanged));
    }
}
