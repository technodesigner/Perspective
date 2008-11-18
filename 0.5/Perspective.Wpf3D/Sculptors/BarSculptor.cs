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
    /// Default radius is 1.0.
    /// By default, the direction of the bar is the Z axis, and the length is 1.0.
    /// </summary>
    public class BarSculptor : Sculptor
    {
        private int _circumferenceSideCount;
        private double _initialAngle;
        private double _roundingRate;

        /// <summary>
        /// Initializes an existing instance of BarSculptor.
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
            PolygonSculptor ps1 = new PolygonSculptor(_circumferenceSideCount, _initialAngle);
            ps1.RoundingRate = _roundingRate;
            ps1.BuildTriangles(TriangleSideKind.Back);
            foreach (Point3D p in ps1.Points)
            {
                this.Points.Add(p);
            }
            foreach (Point3DTriplet tpl in ps1.Triangles)
            {
                this.Triangles.Add(tpl);
            }

            PolygonSculptor ps2 = new PolygonSculptor(_circumferenceSideCount, _initialAngle);
            ps2.Center = new Point3D(
                ps2.Center.X,
                ps2.Center.Y,
                ps2.Center.Z + 1.0);
            for (int i = 0; i < ps2.Points.Count; i++)
            {
                ps2.Points[i] = new Point3D(
                    ps2.Points[i].X,
                    ps2.Points[i].Y,
                    ps2.Points[i].Z + 1.0);
                this.Points.Add(ps2.Points[i]);
            }
            ps2.RoundingRate = _roundingRate;
            ps2.BuildTriangles(TriangleSideKind.Front);
            foreach (Point3D p in ps2.Points)
            {
                this.Points.Add(p);
            }
            foreach (Point3DTriplet tpl in ps2.Triangles)
            {
                this.Triangles.Add(tpl);
            }

            for (int i = 0; i < ps1.Points.Count; i++)
            {
                if (i < ps1.Points.Count - 1)
                {
                    this.Triangles.Add(new Point3DTriplet(ps1.Points[i], ps2.Points[i + 1], ps2.Points[i]));
                    this.Triangles.Add(new Point3DTriplet(ps1.Points[i], ps1.Points[i + 1], ps2.Points[i + 1]));
                }
                else
                {
                    this.Triangles.Add(new Point3DTriplet(ps1.Points[i], ps2.Points[0], ps2.Points[i]));
                    this.Triangles.Add(new Point3DTriplet(ps1.Points[i], ps1.Points[0], ps2.Points[0]));
                }
            }
        }
    }
}
