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
using Perspective.Wpf3D.Primitives;

namespace Perspective.Wpf3D.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of a square model.
    /// Default size of a side is 1.0.
    /// </summary>
    public class SquareSculptor : Sculptor
    {
        Point3D _p000 = new Point3D(0, 0, 0);
        Point3D _p100 = new Point3D(1, 0, 0);
        Point3D _p110 = new Point3D(1, 1, 0);
        Point3D _p010 = new Point3D(0, 1, 0);

        /// <summary>
        /// Initializes a new instance of SquareSculptor.
        /// </summary>
        public SquareSculptor()
            : base()
        {
            this.Points.Add(_p000);
            this.Points.Add(_p100);
            this.Points.Add(_p110);
            this.Points.Add(_p010);
        }

        /// <summary>
        /// Initializes the Points and Triangles collections.
        /// Called By Sculptor.BuildMesh()
        /// </summary>
        protected override void CreateTriangles()
        {
            this.Triangles.Clear();
            CreateSideTriangles(Points, TriangleSideKind.Front);
        }

        /// <summary>
        /// A method for building the TextureCoordinates collection of the mesh.
        /// </summary>
        protected override void CreateTextureCoordinates()
        {
            //// cf. http://blogs.inetium.com/blogs/mhodnick/archive/2006/04/13/65.aspx
            //// attention, les coordonnées X, Y en 2D sont inversées par rapport à notre repère 3D

            Mesh.TextureCoordinates.Add(new Point(0, 1));
            Mesh.TextureCoordinates.Add(new Point(1, 1));
            Mesh.TextureCoordinates.Add(new Point(1, 0));

            Mesh.TextureCoordinates.Add(new Point(0, 1));
            Mesh.TextureCoordinates.Add(new Point(1, 0));
            Mesh.TextureCoordinates.Add(new Point(0, 0));
        }

    }
}
