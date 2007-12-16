using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace ODewit.Perspective.Primitives
{
    /// <summary>
    /// A class to handle points and triangles of an AxisBoard3D model.
    /// </summary>
    class AxisBoardSculptor : Sculptor
    {
        private Point3D _p110 = new Point3D(1, 1, 0);
        private Point3D _p_110 = new Point3D(-1, 1, 0);
        private Point3D _p_1_10 = new Point3D(-1, -1, 0);
        private Point3D _p1_10 = new Point3D(1, -1, 0);

        private Point3D _p10_1 = new Point3D(1, 0, -1);
        private Point3D _p_10_1 = new Point3D(-1, 0, -1);
        private Point3D _p_101 = new Point3D(-1, 0, 1);
        private Point3D _p101 = new Point3D(1, 0, 1);

        private Point3D _p011 = new Point3D(0, 1, 1);
        private Point3D _p0_11 = new Point3D(0, -1, 1);
        private Point3D _p0_1_1 = new Point3D(0, -1, -1);
        private Point3D _p01_1 = new Point3D(0, 1, -1);

        // private Point3DCollection[] _sidePoints = new Point3DCollection[3];
        private Point3DCollection[] _sidePoints = new Point3DCollection[1];

        private void InitializeSidePoints(int i, Point3D p1, Point3D p2, Point3D p3, Point3D p4)
        {
            this.Points.Add(p1);
            this.Points.Add(p2);
            this.Points.Add(p3);
            this.Points.Add(p4);

            _sidePoints[i] = new Point3DCollection();
            _sidePoints[i].Add(p1);
            _sidePoints[i].Add(p2);
            _sidePoints[i].Add(p3);
            _sidePoints[i].Add(p4);
        }

        public AxisBoardSculptor(): base()
        {
            InitializeSidePoints(0, _p110, _p_110, _p_1_10, _p1_10);
            // InitializeSidePoints(1, _p10_1, _p_10_1, _p_101, _p101);
            // InitializeSidePoints(2, _p011, _p0_11, _p0_1_1, _p01_1);
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
        /// <param name="transform">Transform3D object.</param>
        public override void Transform(Transform3D transform)
        {
            base.Transform(transform);
            foreach (Point3DCollection points in _sidePoints)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = transform.Transform(points[i]);
                }
            }
        }

    }
}
