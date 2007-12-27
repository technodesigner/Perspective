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
    /// A 3D ring model.
    /// Default radius is 10.0.
    /// </summary>
    public class Ring3D : PolygonalElement3D
    {

        /// <summary>
        /// Gets or sets the ring radius.
        /// Default value is 10.0.
        /// </summary>
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        /// <summary>
        /// Identifies the Radius dependency property.
        /// </summary>
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(
                "Radius",
                typeof(double),
                typeof(Ring3D),
                new UIPropertyMetadata(10.0, VisualPropertyChanged));

        /// <summary>
        /// Gets or sets the ring bar segment count.
        /// Default value is 40. Minimum value is 3.
        /// </summary>
        public int SegmentCount
        {
            get { return (int)GetValue(SegmentCountProperty); }
            set { SetValue(SegmentCountProperty, value); }
        }

        /// <summary>
        /// Identifies the SegmentCount dependency property.
        /// </summary>
        public static readonly DependencyProperty SegmentCountProperty =
            DependencyProperty.Register(
                "SegmentCount",
                typeof(int),
                typeof(Ring3D),
                new UIPropertyMetadata(40, VisualPropertyChanged),
                IsValidSegmentCountValue);

        /// <summary>
        /// Validation of the SegmentCount value.
        /// </summary>
        /// <param name="value">Value to test.</param>
        /// <returns>Boolean value.</returns>
        private static bool IsValidSegmentCountValue(object value)
        {
            int i = (int)value;
            return i >= 3;
        }

        private RingSculptor _sculptor = new RingSculptor();

        /// <summary>
        /// Called by UIElement3D.InvalidateModel() to update the 3D model.
        /// </summary>
        protected override void OnUpdateModel()
        {
            _sculptor.Initialize(Radius, SegmentCount, SideCount, InitialAngle, RoundingRate);
            _sculptor.BuildMesh();
            Geometry = _sculptor.Mesh;
            base.OnUpdateModel();
        }
    }
}
