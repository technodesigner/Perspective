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
using System.Windows.Media.Media3D;
using Perspective.Wpf3D.Primitives;
using Perspective.Wpf3D.Sculptors;

namespace Perspective.Wpf3D.Shapes
{
    /// <summary>
    /// A 3D spherical element.
    /// Default radius is 1.0.
    /// </summary>
    public class Spherical3D : GeometryElement3D
    {
        /// <summary>
        /// Gets or sets the side count for half of the circumference.
        /// Default value is 20.
        /// Meridian count is twice this value.
        /// </summary>
        public int ParallelCount
        {
            get { return (int)GetValue(ParallelCountProperty); }
            set { SetValue(ParallelCountProperty, value); }
        }

        /// <summary>
        /// Identifies the ParallelCount dependency property.
        /// </summary>
        public static readonly DependencyProperty ParallelCountProperty =
            DependencyProperty.Register(
                "ParallelCount",
                typeof(int),
                typeof(Spherical3D),
                new PropertyMetadata(
                    SphericalSculptor.DefaultParallelCount,
                    VisualPropertyChanged));

        private SphericalSculptor _sculptor = new SphericalSculptor();

        /// <summary>
        /// Called by UIElement3D.InvalidateModel() to update the 3D model.
        /// </summary>
        protected override void OnUpdateModel()
        {
            _sculptor.Initialize(ParallelCount);
            _sculptor.BuildMesh();
            Geometry = _sculptor.Mesh;
            base.OnUpdateModel();
        }
    }
}
