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
using System.Windows.Media.Media3D;
using Perspective.Wpf3D.Primitives;

namespace Perspective.Wpf3D.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of a box model.
    /// Default size of a side is 1.0.
    /// </summary>
    public class BoxSculptor : Sculptor
    {
        private Point3D _p000 = new Point3D(0, 0, 0);
        private Point3D _p100 = new Point3D(1, 0, 0);
        private Point3D _p110 = new Point3D(1, 1, 0);
        private Point3D _p010 = new Point3D(0, 1, 0);
        private Point3D _p001 = new Point3D(0, 0, 1);
        private Point3D _p011 = new Point3D(0, 1, 1);
        private Point3D _p101 = new Point3D(1, 0, 1);
        private Point3D _p111 = new Point3D(1, 1, 1);

        private Point3DCollection[] _sidePoints = new Point3DCollection[6];

        /// <summary>
        /// Initializes a new instance of BoxSculptor.
        /// </summary>
        public BoxSculptor()
            : base()
        {
            this.Points.Add(_p000);
            this.Points.Add(_p100);
            this.Points.Add(_p110);
            this.Points.Add(_p010);
            this.Points.Add(_p001);
            this.Points.Add(_p011);
            this.Points.Add(_p101);
            this.Points.Add(_p111);

            _sidePoints[0] = new Point3DCollection();
            _sidePoints[0].Add(_p100);
            _sidePoints[0].Add(_p000);
            _sidePoints[0].Add(_p010);
            _sidePoints[0].Add(_p110);

            _sidePoints[1] = new Point3DCollection();
            _sidePoints[1].Add(_p000);
            _sidePoints[1].Add(_p001);
            _sidePoints[1].Add(_p011);
            _sidePoints[1].Add(_p010);

            _sidePoints[2] = new Point3DCollection();
            _sidePoints[2].Add(_p001);
            _sidePoints[2].Add(_p101);
            _sidePoints[2].Add(_p111);
            _sidePoints[2].Add(_p011);

            _sidePoints[3] = new Point3DCollection();
            _sidePoints[3].Add(_p101);
            _sidePoints[3].Add(_p100);
            _sidePoints[3].Add(_p110);
            _sidePoints[3].Add(_p111);

            _sidePoints[4] = new Point3DCollection();
            _sidePoints[4].Add(_p111);
            _sidePoints[4].Add(_p110);
            _sidePoints[4].Add(_p010);
            _sidePoints[4].Add(_p011);

            _sidePoints[5] = new Point3DCollection();
            _sidePoints[5].Add(_p000);
            _sidePoints[5].Add(_p100);
            _sidePoints[5].Add(_p101);
            _sidePoints[5].Add(_p001);
        }

        /// <summary>
        /// Building of the Triangles collections.
        /// </summary>
        protected override void CreateTriangles()
        {
            this.Triangles.Clear();
            foreach (Point3DCollection points in _sidePoints)
            {
                CreateSideTriangles(points, TriangleSideKind.Front);
            }
        }

        /// <summary>
        /// Applies a transformation to the points
        /// </summary>
        /// <param name="t">Transform3D object.</param>
        public override void Transform(Transform3D t)
        {
            base.Transform(t);
            foreach (Point3DCollection points in _sidePoints)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = t.Transform(points[i]);
                }
            }
        }
    }
}
