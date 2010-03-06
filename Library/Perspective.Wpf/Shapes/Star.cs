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
using Perspective.Wpf.Primitives;
using Perspective.Core.Wpf;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using Perspective.Wpf.Drawers;

namespace Perspective.Wpf.Shapes
{
    /// <summary>
    /// Draws a star.
    /// </summary>
    public class Star : CustomShape
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static Star()
        {
            Shape.StretchProperty.OverrideMetadata(
                typeof(Star), 
                new FrameworkPropertyMetadata(Stretch.Uniform));
        }

        /// <summary>
        /// Identifies the BranchCount dependency property.
        /// </summary>
        public static readonly DependencyProperty BranchCountProperty =
            DependencyProperty.Register(
                "BranchCount", 
                typeof(int), 
                typeof(Star), 
                new FrameworkPropertyMetadata(
                    6,
                    FrameworkPropertyMetadataOptions.AffectsRender),
                DPHelper.IsIntValueGreaterThan2);

        /// <summary>
        /// Gets or sets the branch count of the star.
        /// </summary>
        public int BranchCount
        {
            get { return (int)GetValue(BranchCountProperty); }
            set { SetValue(BranchCountProperty, value); }
        }

        /// <summary>
        /// Identifies the BranchWidth dependency property.
        /// </summary>
        public static readonly DependencyProperty BranchWidthProperty =
            DependencyProperty.Register(
                "BranchWidth", 
                typeof(double), 
                typeof(Star), 
                new FrameworkPropertyMetadata(
                    0.2,
                    FrameworkPropertyMetadataOptions.AffectsRender), 
                DPHelper.IsDoubleValuePositive);

        /// <summary>
        /// Gets or sets the width of a star branch.
        /// </summary>
        public double BranchWidth
        {
            get { return (double)GetValue(BranchWidthProperty); }
            set { SetValue(BranchWidthProperty, value); }
        }

        /// <summary>
        /// Identifies the InitialAngle dependency property.
        /// </summary>
        private static readonly DependencyProperty InitialAngleProperty =
            DependencyProperty.Register(
                "InitialAngle",
                typeof(double),
                typeof(Star),
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
        /// Creates a StarDrawer object.
        /// </summary>
        /// <returns>A StarDrawer object.</returns>
        protected override Drawer CreateDrawer()
        {
            return new StarDrawer(
                BranchCount,
                BranchWidth,
                InitialAngle,
                Width,
                Height,
                StrokeThickness);
        }
    }
}
