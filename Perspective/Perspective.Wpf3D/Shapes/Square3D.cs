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
using Perspective.Wpf3D.Sculptors;

namespace Perspective.Wpf3D.Shapes
{
    /// <summary>
    /// A 3D flat square model.
    /// Default size of a side is 1.0.
    /// </summary>
    public class Square3D : GeometryElement3D
    {
        private SquareSculptor _sculptor = new SquareSculptor();

        /// <summary>
        /// Called by UIElement3D.InvalidateModel() to update the 3D model.
        /// </summary>
        protected override void OnUpdateModel()
        {
            _sculptor.BuildMesh();
            Geometry = _sculptor.Mesh;
            base.OnUpdateModel();
        }
    }
}
