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
    /// A class to handle points and triangles of a spherical model.
    /// Default radius is 1.0.
    /// </summary>
    public class SphericalSculptor : Sculptor
    {
        /// <summary>
        /// Initializes a new instance of SphericalSculptor.
        /// </summary>
        public SphericalSculptor() { }

        /// <summary>
        /// Initializes an existing instance of SphericalSculptor.
        /// </summary>
        /// <param name="parallelCount">Parallel Count.</param>
        public void Initialize(int parallelCount)
        {
            if (parallelCount < 2)
            {
                throw new ArgumentException("parallelCount < 2");
            }
            _parallelCount = parallelCount;
            CreatePoints();
        }
        /// <summary>
        /// Initializes the Points and Triangles collections.
        /// Called By Sculptor.BuildMesh()
        /// </summary>
        protected override void CreateTriangles()
        {
            if (Points.Count == 0)
            {
                CreatePoints(this.ParallelCount);
            }
            if ((Triangles.Count == 0) && (Points.Count > 0))
            {
                BuildTriangles();
            }
        }

        internal const int DefaultParallelCount = 20;
        private int _parallelCount = DefaultParallelCount;

        /// <summary>
        /// Gets or sets the side count for half of the circumference.
        /// </summary>
        public int ParallelCount
        {
            get { return _parallelCount; }
            set { _parallelCount = value; }
        }

        /// <summary>
        /// Creates the structure points.
        /// </summary>
        /// <param name="parallelCount">Side count for half of the circumference.</param>
        public void CreatePoints(int parallelCount)
        {
            _parallelCount = parallelCount;
            CreatePoints();
        }

        /// <summary>
        /// Creates the points.
        /// </summary>
        public void CreatePoints()
        {
            this.Points.Clear();

            // theta : vertical slices
            // phi : horizontal slices

            double thetaStep = Math.PI / (_parallelCount + 1);
            double phiStep = Math.PI / (_parallelCount + 1);
            double theta = 0.0;
            double phi = 0.0;

            for (int i = 0; i <= ((_parallelCount + 1) * 2); i++)
            {
                theta = (thetaStep * i);
                for (int j = 0; j <= _parallelCount + 1; j++)
                {
                    phi = phiStep * j;
                    Point3D p = new Point3D();
                    p.X = Math.Sin(theta) * Math.Sin(phi);
                    p.Y = Math.Cos(phi);
                    p.Z = Math.Cos(theta) * Math.Sin(phi);
                    this.Points.Add(p);
                }
            }
        }

        /// <summary>
        /// Builds the structure triangles.
        /// </summary>
        public void BuildTriangles()
        {
            if (Points.Count < 2)
            {
                throw new ArgumentException("Points.Count < 2");
            }
            this.Triangles.Clear();
            for (int i = 0; i <= Points.Count - 1; i++)
            {
                int rank = i / (_parallelCount + 2);
                int index = i % (_parallelCount + 2);
                if (rank >= 1)
                {
                    if (index > 0)
                    {
                        this.Triangles.Add(new Point3DTriplet(
                            Points[i],
                            Points[i - _parallelCount - 2],
                            Points[i - _parallelCount - 1]));
                    }
                    if (index < _parallelCount + 1)
                    {
                        this.Triangles.Add(new Point3DTriplet(
                            Points[i],
                            Points[i - _parallelCount - 1],
                            Points[i + 1]));

                    }
                }
            }
        }

    }
}
