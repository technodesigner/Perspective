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
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using Perspective.Wpf.Drawers;
using Perspective.Core.Wpf;

namespace Perspective.Wpf.Shapes
{
    /// <summary>
    /// Draws an arrow.
    /// </summary>
    public class Arrow : CustomShape
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static Arrow()
        {
            Shape.StretchProperty.OverrideMetadata(
                typeof(Arrow), 
                new FrameworkPropertyMetadata(Stretch.Uniform));
        }

        /// <summary>
        /// Identifies the FormatRatio dependency property.
        /// </summary>
        public static readonly DependencyProperty FormatRatioProperty =
            DependencyProperty.Register(
                "FormatRatio", 
                typeof(double), 
                typeof(Arrow), 
                new FrameworkPropertyMetadata(
                    2.5, 
                    FrameworkPropertyMetadataOptions.AffectsRender),
                DPHelper.IsDoubleValuePositive);

        /// <summary>
        /// Gets or sets a value indicating the width/height ratio (the figure's format).
        /// Default value is 2.5.
        /// </summary>
        public double FormatRatio
        {
            get { return (double)GetValue(FormatRatioProperty); }
            set { SetValue(FormatRatioProperty, value); }
        }

        /// <summary>
        /// Identifies the HeadLengthRatio dependency property.
        /// </summary>
        public static readonly DependencyProperty HeadLengthRatioProperty =
            DependencyProperty.Register(
                "HeadLengthRatio", 
                typeof(double), 
                typeof(Arrow), 
                new FrameworkPropertyMetadata(
                    0.2, 
                    FrameworkPropertyMetadataOptions.AffectsRender),
                DPHelper.IsDoubleValuePositive);

        /// <summary>
        /// Gets or sets a value indicating the "head length/total length" ratio.
        /// Default value is 0.2.
        /// </summary>
        public double HeadLengthRatio
        {
            get { return (double)GetValue(HeadLengthRatioProperty); }
            set { SetValue(HeadLengthRatioProperty, value); }
        }

        /// <summary>
        /// Identifies the HeadWidthRatio dependency property.
        /// </summary>
        public static readonly DependencyProperty HeadWidthRatioProperty =
            DependencyProperty.Register(
                "HeadWidthRatio", 
                typeof(double), 
                typeof(Arrow), 
                new FrameworkPropertyMetadata(
                    2.0, 
                    FrameworkPropertyMetadataOptions.AffectsRender),
                DPHelper.IsDoubleValuePositive);

        /// <summary>
        /// Gets or sets a value indicating the "head width/body width" ratio.
        /// </summary>
        public double HeadWidthRatio
        {
            get { return (double)GetValue(HeadWidthRatioProperty); }
            set { SetValue(HeadWidthRatioProperty, value); }
        }

        private ArrowDrawer _arrowDrawer;

        /// <summary>
        /// Creates an ArrowDrawer object.
        /// </summary>
        /// <returns>An ArrowDrawer object.</returns>
        protected override Drawer CreateDrawer()
        {
            _arrowDrawer = new ArrowDrawer();
            return _arrowDrawer;
        }

        /// <summary>
        /// Initializes the ArrowDrawer object.
        /// </summary>
        protected override void InitializeDrawer()
        {
            _arrowDrawer.Initialize(
                FormatRatio,
                HeadLengthRatio,
                HeadWidthRatio,
                Width,
                Height,
                StrokeThickness);
        }
    }
}
