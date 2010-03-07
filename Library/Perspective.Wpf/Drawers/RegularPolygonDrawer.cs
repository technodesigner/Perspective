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
using System.Windows;
using System.Windows.Media;

namespace Perspective.Wpf.Drawers
{
    /// <summary>
    /// A class to generate the points and segments of a roundabale regular polygon Pathfigure object.
    /// </summary>
    public class RegularPolygonDrawer : Drawer
    {
        private double _initialAngle;
        private int _sideCount;
        private double _roundingRate;
        private bool _stretchLikeUnrounded;

        /// <summary>
        /// Initializes a new instance of RegularPolygonDrawer.
        /// </summary>
        public RegularPolygonDrawer()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of RegularPolygonDrawer.
        /// </summary>
        /// <param name="initialAngle">The initial angle (first point), in degrees.</param>
        /// <param name="sideCount">The side count of the circumference.</param>
        /// <param name="roundingRate">The shape's angle rounding rate. The value must be comprized between 0.0 and 0.5.</param>
        /// <param name="stretchLikeUnrounded">For a rounded figure, indicates if the figure stretches in the same space than the unrounded one.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public RegularPolygonDrawer(
            double initialAngle,
            int sideCount,
            double roundingRate,
            bool stretchLikeUnrounded,
            double width,
            double height,
            double strokeThickness) : base()
        {
            Initialize(initialAngle, sideCount, roundingRate, stretchLikeUnrounded, width, height, strokeThickness);
        }

        /// <summary>
        /// Initializes a RegularPolygonDrawer object.
        /// </summary>
        /// <param name="initialAngle">The initial angle (first point), in degrees.</param>
        /// <param name="sideCount">The side count of the circumference.</param>
        /// <param name="roundingRate">The shape's angle rounding rate. The value must be comprized between 0.0 and 0.5.</param>
        /// <param name="stretchLikeUnrounded">For a rounded figure, indicates if the figure stretches in the same space than the unrounded one.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public void Initialize(
            double initialAngle,
            int sideCount,
            double roundingRate,
            bool stretchLikeUnrounded,
            double width,
            double height,
            double strokeThickness)
        {
            if ((roundingRate < 0.0) || (roundingRate > 0.5))
            {
                throw new ArgumentOutOfRangeException("roundingRate");
            }
            if (sideCount < 3)
            {
                throw new ArgumentOutOfRangeException("sideCount < 3");
            }
            _initialAngle = initialAngle;
            _sideCount = sideCount;
            _roundingRate = roundingRate;
            _stretchLikeUnrounded = stretchLikeUnrounded;
            base.Initialize(width, height, strokeThickness);
        }

        /// <summary>
        /// Generates the points and segments of the polygonal Figure object.
        /// </summary>
        public override void BuildFigure()
        {
            Points.Clear();
            double initialAngleRad = GeometryHelper.DegreeToRadian(_initialAngle);
            double angle1Rad = 2 * Math.PI / _sideCount;
            double angleRad = 0.0;
            double areaWidth = Double.IsNaN(Width) ? 0.0 : Width / 2;
            double areaHeight = Double.IsNaN(Height) ? 0.0 : Height / 2;
            for (int i = 1; i <= _sideCount; i++)
            {
                angleRad = (angle1Rad * i) + initialAngleRad;
                Point p = new Point();
                p.X = areaWidth == 0.0 ? 
                    Math.Cos(angleRad) : 
                    areaWidth + Math.Cos(angleRad) * (areaWidth - StrokeThickness / 2);
                p.Y = areaHeight == 0.0 ? 
                    Math.Sin(angleRad) :
                    areaHeight + Math.Sin(angleRad) * (areaHeight - StrokeThickness / 2);
                Points.Add(p);
            }
            Figure.Segments.Clear();
            if (_roundingRate == 0.0)
            {
                Figure.StartPoint = Points[0];
                for (int i = 0; i < Points.Count; i++)
                {
                    if (i < Points.Count - 1)
                    {
                        LineSegment segment = new LineSegment(Points[i + 1], true);
                        Figure.Segments.Add(segment);
                    }
                }
            }
            else
            {
                Point currentPoint = Figure.StartPoint;
                for (int i = 0; i < Points.Count; i++)
                {
                    Point nextPoint = (i < Points.Count - 1) ? Points[i + 1] : Points[0];
                    Point previousPoint = (i == 0) ? Points[Points.Count - 1] : Points[i - 1];
                    double pointFactor = 1.0 - _roundingRate;
                    if (i == 0)
                    {
                        Vector vStart = Points[i] - previousPoint;
                        Figure.StartPoint = Point.Add(previousPoint, Vector.Multiply(vStart, pointFactor));
                        currentPoint = Figure.StartPoint;
                    }

                    if (_stretchLikeUnrounded)
                    {
                        // unstroked segment are included to make the shape stretch in the same space than the unrounded one
                        Figure.Segments.Add(new LineSegment(nextPoint, false));
                        Figure.Segments.Add(new LineSegment(currentPoint, false));
                    }

                    Vector vEnd = Points[i] - nextPoint;
                    Point endPoint = Point.Add(nextPoint, Vector.Multiply(vEnd, pointFactor));

                    // QuadraticBezierSegment oofers a better rounding than BezierSegment
                    // BezierSegment bezierSegment = new BezierSegment(points[i], points[i], endPoint, true);
                    QuadraticBezierSegment bezierSegment = new QuadraticBezierSegment(Points[i], endPoint, true);

                    Figure.Segments.Add(bezierSegment);

                    if (i < Points.Count - 1)
                    {
                        LineSegment segment = new LineSegment();
                        Vector vLineEnd = nextPoint - Points[i];
                        segment.Point = Point.Add(Points[i], Vector.Multiply(vLineEnd, pointFactor));
                        currentPoint = segment.Point;
                        Figure.Segments.Add(segment);
                    }
                }
            }
        }
    }
}
