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
    /// A class to handle points and triangles of a 3D arrow model.
    /// Default radius of the head is 0.2.
    /// Default radius of the body is 0.1.
    /// By default, the direction of the arrow is the Z axis, and the length is 1.0.
    /// </summary>
    public class ArrowSculptor : Sculptor
    {
        internal const double DefaultLength = 1.0;
        private double _length = DefaultLength;

        /// <summary>
        /// Initializes an existing instance of XyzAxisSculptor.
        /// </summary>
        /// <param name="length">Length of each axis.</param>
        public void Initialize(double length)
        {
            _length = length;
        }

        /// <summary>
        /// Initializes the Points and Triangles collections.
        /// Called By Sculptor.BuildMesh()
        /// </summary>
        protected override void CreateTriangles()
        {
            // ConicalSculptor conicalSculptor = new ConicalSculptor(4, 0.0, 0.5);
            ConicalSculptor conicalSculptor = new ConicalSculptor(40, 0.0, 0.0);
            conicalSculptor.BuildMesh();
            Transform3DGroup tgHead = new Transform3DGroup();
            tgHead.Children.Add(new ScaleTransform3D(0.2, 0.2, 0.2));
            tgHead.Children.Add(new TranslateTransform3D(0.0, 0.0, _length - 1.0 + 0.8));
            conicalSculptor.Transform(tgHead);
            CopyFrom(conicalSculptor);

            BarSculptor barSculptor = new BarSculptor();
            // barSculptor.Initialize(4, 0.0, 0.5);
            barSculptor.Initialize(40, 0.0, 0.0);
            barSculptor.BuildMesh();
            barSculptor.Transform(new ScaleTransform3D(0.1, 0.1, _length - 1.0 + 0.8));
            CopyFrom(barSculptor);
        }
    }
}
