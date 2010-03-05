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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace Perspective.Wpf.Primitives
{
    /// <summary>
    /// An abstract class to build a custom shape through a Drawer object.
    /// </summary>
    public abstract class CustomShape : Shape
    {
        private PathGeometry _geometry = new PathGeometry();
        private Drawer _drawer;
        private bool _forceGeometryBuilding = false;

        /// <summary>
        /// Gets the shape's geometry object.
        /// </summary>
        protected PathGeometry Geometry
        {
            get { return _geometry; }
        }

        /// <summary>
        /// A property changed callback to indicate that the geometry building is required.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        protected static void DependencyPropertyChangedAndForceRebuild(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CustomShape)._forceGeometryBuilding = true;
        }

        /// <summary>
        /// Creates the Geometry object through the Drawer object.
        /// </summary>
        private void EnsureGeometry()
        {
            _drawer = CreateDrawer();
            if (_drawer != null)
            {
                _drawer.BuildFigure();
                _geometry.Figures.Add(_drawer.Figure);
            }
            _forceGeometryBuilding = false;          
        }

        /// <summary>
        /// A method to override to create the Drawer object.
        /// </summary>
        /// <returns>A Drawer object.</returns>
        protected abstract Drawer CreateDrawer();

        /// <summary>
        /// Gets a value that represents the Geometry of the Shape.
        /// </summary>
        protected override System.Windows.Media.Geometry DefiningGeometry
        {
            get
            {
                if ( _forceGeometryBuilding ||
                    (_drawer == null) ||
                    (_drawer.Figure.Segments.Count == 0))
                {
                    EnsureGeometry();
                }             
                return _geometry;
            }
        }
    }
}
