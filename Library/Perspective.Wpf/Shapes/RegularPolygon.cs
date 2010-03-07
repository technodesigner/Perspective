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
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using Perspective.Core.Wpf;
using Perspective.Wpf.Primitives;
using Perspective.Wpf.Drawers;

namespace Perspective.Wpf.Shapes
{
    /// <summary>
    /// Draws a roundable regular polygon.
    /// </summary>
    public class RegularPolygon : CustomShape
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static RegularPolygon()
        {
            Shape.StretchProperty.OverrideMetadata(
                typeof(RegularPolygon), 
                new FrameworkPropertyMetadata(Stretch.Uniform));
        }

        /// <summary>
        /// Identifies the InitialAngle dependency property.
        /// </summary>
        private static readonly DependencyProperty InitialAngleProperty =
            DependencyProperty.Register(
                "InitialAngle",
                typeof(double),
                typeof(RegularPolygon),
                new FrameworkPropertyMetadata(
                    0.0, 
                    FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the initial angle (first point), in degrees.
        /// </summary>
        public double InitialAngle
        {
            get
            {
                return (double)base.GetValue(InitialAngleProperty);
            }
            set
            {
                base.SetValue(InitialAngleProperty, value);
            }
        }

        /// <summary>
        /// Identifies the SideCount dependency property.
        /// </summary>
        private static readonly DependencyProperty SideCountProperty =
            DependencyProperty.Register(
                "SideCount",
                typeof(int),
                typeof(RegularPolygon),
                new FrameworkPropertyMetadata(
                    6, 
                    FrameworkPropertyMetadataOptions.AffectsRender),
                DPHelper.IsIntValueGreaterThan3);

        /// <summary>
        /// Gets or sets the side count of the circumference.
        /// </summary>
        public int SideCount
        {
            get
            {
                return (int)base.GetValue(SideCountProperty);
            }
            set
            {
                base.SetValue(SideCountProperty, value);
            }
        }

        /// <summary>
        /// Identifies the RoundingRate dependency property.
        /// </summary>
        public static readonly DependencyProperty RoundingRateProperty =
            DependencyProperty.Register(
                "RoundingRate",
                typeof(double),
                typeof(RegularPolygon),
                new FrameworkPropertyMetadata(
                    0.0, 
                    FrameworkPropertyMetadataOptions.AffectsRender),
                DPHelper.IsDoubleValueBetween0AndDot5);

        /// <summary>
        /// Gets or sets the shape's angle rounding rate.
        /// The value must be comprized between 0.0 and 0.5.
        /// Default value is 0.0.
        /// </summary>
        public double RoundingRate
        {
            get { return (double)GetValue(RoundingRateProperty); }
            set { SetValue(RoundingRateProperty, value); }
        }

        /// <summary>
        /// Identifies the StretchLikeUnrounded dependency property.
        /// </summary>
        public static readonly DependencyProperty StretchLikeUnroundedProperty =
            DependencyProperty.Register(
                "StretchLikeUnrounded", 
                typeof(bool), 
                typeof(RegularPolygon), 
                new FrameworkPropertyMetadata(
                    true, 
                    FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// For a rounded shape, gets or sets a value indicating if the figure stretches in the same space than the unrounded one.
        /// </summary>
        public bool StretchLikeUnrounded
        {
            get { return (bool)GetValue(StretchLikeUnroundedProperty); }
            set { SetValue(StretchLikeUnroundedProperty, value); }
        }

        private RegularPolygonDrawer _regularPolygonDrawer;

        /// <summary>
        /// Creates a RegularPolygonDrawer object.
        /// </summary>
        /// <returns>A RegularPolygonDrawer object.</returns>
        protected override Drawer CreateDrawer()
        {
            _regularPolygonDrawer = new RegularPolygonDrawer();
            return _regularPolygonDrawer;
        }

        /// <summary>
        /// Initializes the RegularPolygonDrawer object.
        /// </summary>
        protected override void InitializeDrawer()
        {
            _regularPolygonDrawer.Initialize(
                InitialAngle,
                SideCount,
                RoundingRate,
                StretchLikeUnrounded,
                Width,
                Height,
                StrokeThickness);
        }
    }
}
