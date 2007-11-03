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
    /// A class to handle points and triangles of a 3D conical model.
    /// Default radius is 1.0.
    /// By default, the direction of the cone is the Z axis, and the length is 1.0.
    /// </summary>
    public class ConicalSculptor : Sculptor
    {
        private int _circumferenceSideCount;
        private double _initialAngle;
        private double _roundingRate;

        /// <summary>
        /// Initializes a new instance of ConicalSculptor.
        /// </summary>
        public ConicalSculptor()
        {
        }

        /// <summary>
        /// Initializes a new instance of ConicalSculptor.
        /// </summary>
        /// <param name="circumferenceSideCount">Model circumference side count.</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees.</param>
        /// <param name="roundingRate">Angle rounding rate. The value must be comprized between 0.0 and 0.5.</param>
        public ConicalSculptor(int circumferenceSideCount, double initialAngle, double roundingRate)
        {
            Initialize(circumferenceSideCount, initialAngle, roundingRate);
        }

        /// <summary>
        /// Initializes an existing instance of ConicalSculptor.
        /// </summary>
        /// <param name="circumferenceSideCount">Model circumference side count.</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees.</param>
        /// <param name="roundingRate">Angle rounding rate. The value must be comprized between 0.0 and 0.5.</param>
        public void Initialize(int circumferenceSideCount, double initialAngle, double roundingRate)
        {
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
            PolygonSculptor pv1 = new PolygonSculptor(_circumferenceSideCount, _initialAngle);
            pv1.RoundingRate = _roundingRate;
            pv1.BuildTriangles(TriangleSideKind.Back);
            foreach (Point3DTriplet tpl in pv1.Triangles)
            {
                this.Triangles.Add(tpl);
            }
            foreach (Point3D p in pv1.Points)
            {
                this.Points.Add(p);
            }


            PolygonSculptor pv2 = new PolygonSculptor(_circumferenceSideCount, _initialAngle);
            pv2.RoundingRate = _roundingRate;
            pv2.Center = new Point3D(
                pv2.Center.X,
                pv2.Center.Y,
                pv2.Center.Z + 1.0);
            this.Points.Add(pv2.Center);
            pv2.BuildTriangles(TriangleSideKind.Front);
            foreach (Point3DTriplet tpl in pv2.Triangles)
            {
                this.Triangles.Add(tpl);
            }
        }
    }
}
