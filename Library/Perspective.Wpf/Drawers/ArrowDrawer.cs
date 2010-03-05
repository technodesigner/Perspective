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
using System.Windows;
using System.Windows.Media;

namespace Perspective.Wpf.Drawers
{
    /// <summary>
    /// A class to generate the points and segments of an arrow Pathfigure object.
    /// </summary>
    public class ArrowDrawer : Drawer
    {
        private double _formatRatio;
        private double _headLengthRatio;
        private double _headWidthRatio;

        /// <summary>
        /// Initializes a new instance of ArrowDrawer.
        /// </summary>
        /// <param name="formatRatio">A value indicating the width/height ratio (the figure's format).</param>
        /// <param name="headLengthRatio">A value indicating the "head length/total length" ratio.</param>
        /// <param name="headWidthRatio">A value indicating the "head width/body width" ratio.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public ArrowDrawer(
            double formatRatio,
            double headLengthRatio,
            double headWidthRatio,
            double width,
            double height,
            double strokeThickness) : base(width, height, strokeThickness)
        {
            if (formatRatio <= 0.0)
            {
                throw new ArgumentOutOfRangeException("formatRatio <= 0.0");
            }
            if (headLengthRatio <= 0.0)
            {
                throw new ArgumentOutOfRangeException("headLengthRatio <= 0.0");
            }
            if (headWidthRatio <= 0.0)
            {
                throw new ArgumentOutOfRangeException("headWidthRatio <= 0.0");
            }
            _formatRatio = formatRatio;
            _headLengthRatio = headLengthRatio;
            _headWidthRatio = headWidthRatio;
        }

        /// <summary>
        /// Generates the points and segments of the arrow Figure object.
        /// </summary>
        public override void BuildFigure()
        {
            Points.Clear();
            double headLength = _formatRatio * _headLengthRatio;
            double bodyLength = _formatRatio - headLength;
            double bodyHeight = _headWidthRatio != 0.0 ? 1.0 / _headWidthRatio : 1.0;
            double bodyTop = 0.5 - bodyHeight / 2;
            double bodyBottom = 0.5 + bodyHeight / 2;

            Points.Add(new Point(0.0, bodyTop));
            Points.Add(new Point(0.0, bodyBottom));
            Points.Add(new Point(bodyLength, bodyBottom));
            Points.Add(new Point(bodyLength, 1.0));
            Points.Add(new Point(_formatRatio, 0.5));
            Points.Add(new Point(bodyLength, 0.0));
            Points.Add(new Point(bodyLength, bodyTop));

            bool scaling = false;
            ScaleTransform transform = new ScaleTransform();
            if (!Double.IsNaN(Width))
            {
                transform.ScaleX = _formatRatio != 0.0 ? (Width - 2 * StrokeThickness) / _formatRatio : 1.0;
                scaling = true;
            }
            if (!Double.IsNaN(Height))
            {
                transform.ScaleY = Height - 2 * StrokeThickness;
                scaling = true;
            }

            Figure.Segments.Clear();
            if (scaling)
            {
                Points[0] = transform.Transform(Points[0]);
            }
            Figure.StartPoint = Points[0];
            for (int i = 0; i < Points.Count; i++)
            {
                if (i < Points.Count - 1)
                {
                    if (scaling)
                    {
                        Points[i + 1] = transform.Transform(Points[i + 1]);
                    }
                    LineSegment segment = new LineSegment(Points[i + 1], true);
                    Figure.Segments.Add(segment);
                }
            }
        }
    }
}
