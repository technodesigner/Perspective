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
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;

namespace Perspective.Wpf3D.Primitives
{
    /// <summary>
    /// A helper class for 3D operations.
    /// </summary>
    public static class Helper3D
    {
        /// <summary>
        /// Duplicate two Point3DCollection objects.
        /// </summary>
        /// <param name="from">Original collection.</param>
        /// <param name="to">Recipient collection.</param>
        public static void ClonePoints(Point3DCollection from, Point3DCollection to)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }
            to.Clear();
            CopyPoints(from, to);
        }

        /// <summary>
        /// Copy the points of a Point3DCollection objects in an other one.
        /// </summary>
        /// <param name="from">Original collection.</param>
        /// <param name="to">Recipient collection.</param>
        public static void CopyPoints(Point3DCollection from, Point3DCollection to)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }
            if (to == null)
            {
                throw new ArgumentNullException("to");
            } 
            foreach (Point3D p in from)
            {
                to.Add(p);
            }
        }

        /// <summary>
        /// Convert a degree angle value in radian.
        /// </summary>
        /// <param name="degree">Angle value in degree.</param>
        /// <returns>Angle value in radian.</returns>
        public static double DegreeToRadian(double degree)
        {
            return (degree / 180.0) * Math.PI;
        }

        /// <summary>
        /// Rounds a vertex of a triangle.
        /// </summary>
        /// <param name="pA">First point.</param>
        /// <param name="pB">Second point. Vertex to round</param>
        /// <param name="pC">Third point.</param>
        /// <param name="roundingRate">Vertex rounding rate. The value must be comprized between 0.0 and 0.5.</param>
        /// <returns></returns>
        public static Point3DCollection RoundCorner(Point3D pA, Point3D pB, Point3D pC, double roundingRate)
        {
            if (!((pA.Z == pB.Z) && (pB.Z == pC.Z))
                )
            {
                throw new ArgumentOutOfRangeException("pA");
            }

            if ((roundingRate < 0.0)
                || (roundingRate > 0.5)
                )
            {
                throw new ArgumentOutOfRangeException("roundingRate");
            }

            Point3DCollection points = new Point3DCollection();

            int roundingDefinition = (int)(roundingRate * 40.0);

            Vector3D v1 = new Vector3D();
            v1 = pA - pB;
            v1.X = Math.Round(v1.X, 3);
            v1.Y = Math.Round(v1.Y, 3);
            v1.Z = Math.Round(v1.Z, 3);
            Point3D p1 = Point3D.Add(pB, Vector3D.Multiply(v1, roundingRate));

            Vector3D v2 = new Vector3D();
            v2 = pC - pB;
            v2.X = Math.Round(v2.X, 3);
            v2.Y = Math.Round(v2.Y, 3);
            v2.Z = Math.Round(v2.Z, 3);
            Point3D p2 = Point3D.Add(pB, Vector3D.Multiply(v2, roundingRate));

            // v1 is the normal vector for the linear curve
            // v1.X*x + v1.Y*y + c1 = 0;
            // p1 is woned by this curve so
            double c1 = -(v1.X * p1.X) - (v1.Y * p1.Y);

            // same for v2 and p2
            double c2 = -(v2.X * p2.X) - (v2.Y * p2.Y);

            // center for the arc that owns p1 and p2
            Point3D center = new Point3D();

            if (v1.Y == 0.0)
            {
                if (v1.X == 0.0)
                {
                    throw new InvalidOperationException();
                }
                center.X = -c1 / v1.X;
                if (v2.Y == 0.0)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    center.Y = (-c2 - v2.X * center.X) / v2.Y;
                }
            }
            else
            {
                if (v2.Y == 0.0)
                {
                    if (v2.X == 0.0)
                    {
                        throw new InvalidOperationException();
                    }
                    center.X = -c2 / v2.X;
                }
                else
                {
                    center.X = (c1 / v1.Y - c2 / v2.Y) / (v2.X / v2.Y - v1.X / v1.Y);
                }
                center.Y = (-c1 - v1.X * center.X) / v1.Y;
            }
            center.Z = pB.Z;

            // angle of the arc between p1 and p2
            // 360 - 180 - Vector3D.AngleBetween(v1, v2)
            double angleArc = Helper3D.DegreeToRadian(180 - Vector3D.AngleBetween(v1, v2));

            // angle of each part
            double angleStep = angleArc / roundingDefinition;

            Vector3D vRadius = p1 - center;

            double angleBaseDeg = Vector3D.AngleBetween(new Vector3D(1, 0, 0), vRadius);
            // necessar adjustment because of Vector3D.AngleBetween() - see documentation
            if (p1.Y < 0.0)
            {
                angleBaseDeg = 360 - angleBaseDeg;
            }
            double angleBase = Helper3D.DegreeToRadian(angleBaseDeg);

            points.Add(p1);
            // points of the arc
            for (int j = 1; j <= roundingDefinition - 1; j++)
            {
                double angle = angleBase + (angleStep * j);
                Point3D p = new Point3D();
                p.X = center.X + Math.Cos(angle) * vRadius.Length;
                p.Y = center.Y + Math.Sin(angle) * vRadius.Length;
                p.Z = pB.Z;
                points.Add(p);
            }
            points.Add(p2);

            return points;
        }

        /// <summary>
        /// Add points, triangle indices and normals to a mesh from a triangle.
        /// </summary>
        /// <param name="mesh">MeshGeometry3D object.</param>
        /// <param name="p1">First point.</param>
        /// <param name="p2">Second point.</param>
        /// <param name="p3">Third point.</param>
        public static void BuildTriangleMesh(MeshGeometry3D mesh, Point3D p1, Point3D p2, Point3D p3)
        {
            // Points passés dans le sens inverse des aiguilles d'une montre (vu de l'extérieur)
            // Afin de bien gérer les vecteurs normaux, il faut
            // avoir une position par triangle pour chaque point

            if (mesh == null)
            {
                throw new ArgumentNullException("mesh");
            } 
            
            mesh.Positions.Add(p1);
            int p1Index = mesh.Positions.Count - 1;
            mesh.Positions.Add(p2);
            int p2Index = mesh.Positions.Count - 1;
            mesh.Positions.Add(p3);
            int p3Index = mesh.Positions.Count - 1;

            mesh.TriangleIndices.Add(p1Index);
            mesh.TriangleIndices.Add(p2Index);
            mesh.TriangleIndices.Add(p3Index);

            Vector3D normal = CalculateNormal(p1, p2, p3);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
        }

        // From : 
        // http://www.kindohm.com/technical/WPF3DTutorial.htm
        // http://www.limsi.fr/Individu/jacquemi/IG-TR-7-8-9/surf-maillage-vn.html
        private static Vector3D CalculateNormal(Point3D p1, Point3D p2, Point3D p3)
        {
            Vector3D v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            Vector3D v2 = new Vector3D(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

            return Vector3D.CrossProduct(v1, v2);
        }

        /// <summary>
        /// Rotation of a point around one of the 3 axes according to a given angle 
        /// Remark : use this method only as a last resort, 
        /// and prefer the WPF transformations, which use the GPU
        /// </summary>
        /// <param name="p">Point to rotate</param>
        /// <param name="angle">Angle (in radians)</param>
        /// <param name="rotationAxis">Rotation axis : X, Y ou Z</param>
        /// <returns>A new Point3D object corresponding to the rotation</returns>
        public static Point3D RotatePoint(Point3D p, double angle, AxisDirection rotationAxis)
        {
            Point3D pResult = new Point3D(0, 0, 0);
            switch (rotationAxis)
            {
                case AxisDirection.X:
                    pResult = new Point3D(
                        p.X,
                        p.Y * Math.Cos(angle) + p.Z * Math.Sin(angle),
                        p.Y * Math.Sin(angle) + p.Z * Math.Cos(angle));
                    break;
                case AxisDirection.Y:
                    pResult = new Point3D(
                        p.Z * Math.Sin(angle) + p.X * Math.Cos(angle),
                        p.Y,
                        p.Z * Math.Cos(angle) + p.X * Math.Sin(angle));
                    break;
                case AxisDirection.Z:
                    pResult = new Point3D(
                        p.X * Math.Cos(angle) + p.Y * Math.Sin(angle),
                        p.X * Math.Sin(angle) + p.Y * Math.Cos(angle),
                        p.Z);
                    break;
            }
            return pResult;
        }

        //public static Point PointToScreen(Point point, Visual v)
        //{
        //    MouseDevice

        //    PresentationSource presentationSource = PresentationSource.FromVisual(v);
        //    if (presentationSource == null)
        //    {
        //        throw new InvalidOperationException(SR.Get(SRID.Visual_NoPresentationSource, new object[0]));
        //    }
        //    GeneralTransform transform = this.TransformToAncestor(presentationSource.RootVisual);
        //    if ((transform == null) || !transform.TryTransform(point, out point))
        //    {
        //        throw new InvalidOperationException(SR.Get(SRID.Visual_CannotTransformPoint, new object[0]));
        //    }
        //    point = PointUtil.RootToClient(point, presentationSource);
        //    point = PointUtil.ClientToScreen(point, presentationSource);
        //    return point;
        //}

        
        /// <summary>
        /// (Inspired by VisualTreeHelper.GetContainingVisual2D(DependencyObject reference))
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Viewport3D GetViewport3D(Visual3D v)
        {
            Viewport3D viewport = null;
            DependencyObject d = (DependencyObject)v;
            while (d != null)
            {
                viewport = d as Viewport3D;
                if (viewport != null)
                {
                    return viewport;
                }
                d = VisualTreeHelper.GetParent(d);
            }
            return viewport;
        }



    }
}
