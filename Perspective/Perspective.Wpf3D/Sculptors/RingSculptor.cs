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
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Perspective.Wpf3D.Primitives;

namespace Perspective.Wpf3D.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of a 3D bar model.
    /// Default radius is 10.0.
    /// </summary>
    public class RingSculptor : Sculptor
    {
        private int _circumferenceSideCount;
        private double _initialAngle;
        private double _roundingRate;
        private double _radius;
        private int _segmentCount;

        /// <summary>
        /// Initializes an existing instance of RingSculptor.
        /// </summary>
        /// <param name="radius">Ring radius.</param>
        /// <param name="segmentCount">Ring bar segment count</param>
        /// <param name="circumferenceSideCount">Ring slice circumference side count.</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees. Applies to the ring slice.</param>
        /// <param name="roundingRate">Angle rounding rate. The value must be comprized between 0.0 and 0.5. Applies to the ring slice.</param>
        public void Initialize(
            double radius,
            int segmentCount,
            int circumferenceSideCount,
            double initialAngle,
            double roundingRate)
        {
            _radius = radius;
            _segmentCount = segmentCount;
            _circumferenceSideCount = circumferenceSideCount;
            _initialAngle = initialAngle;
            _roundingRate = roundingRate;
        }

        /// <summary>
        /// Initializes the Points and Triangles collections.
        /// Called By Sculptor.BuildMesh()
        /// </summary>
        protected override void CreateTriangles()
        {
            Vector3D radiusVector = new Vector3D(_radius, 0, 0);

            PolygonSculptor pv1 = new PolygonSculptor(_circumferenceSideCount, _initialAngle);
            pv1.RoundingRate = _roundingRate;
            pv1.RoundCorner();
            int circumferencePointCount = pv1.Points.Count;
            foreach (Point3D p in pv1.Points)
            {
                Point3D p1 = new Point3D(p.X, p.Y, p.Z) + radiusVector;
                this.Points.Add(p1);
            }

            double thetaStep = 2 * Math.PI / _segmentCount;
            double theta = 0.0;

            for (int i = 1; i <= _segmentCount; i++)
            {
                theta = (thetaStep * i);
                Vector3D thetaVector = new Vector3D(
                    Math.Cos(theta),
                    0,
                    Math.Sin(theta));

                for (int i1 = 0; i1 < circumferencePointCount; i1++)
                {
                    Point3D p2 = Helper3D.RotatePoint(Points[i1], theta, AxisDirection.Y);
                    this.Points.Add(p2);
                }

                for (int j = 0; j < circumferencePointCount; j++)
                {
                    int index1Min = (i - 1) * circumferencePointCount;
                    int index1 = index1Min + j;
                    int index2Min = i * circumferencePointCount;
                    int index2 = index2Min + j;
                    int indexLimit = index2Min + circumferencePointCount - 1;

                    if (index2 < indexLimit)
                    {
                        this.Triangles.Add(new Point3DTriplet(Points[index1], Points[index2 + 1], Points[index2]));
                        this.Triangles.Add(new Point3DTriplet(Points[index1], Points[index1 + 1], Points[index2 + 1]));
                    }
                    else
                    {
                        this.Triangles.Add(new Point3DTriplet(Points[index1], Points[index2Min], Points[index2]));
                        this.Triangles.Add(new Point3DTriplet(Points[index1], Points[index1Min], Points[index2Min]));
                    }
                }
            }
        }
    }
}
