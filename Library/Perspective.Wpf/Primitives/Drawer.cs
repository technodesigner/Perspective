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
using System.Windows.Media;

namespace Perspective.Wpf.Primitives
{
    /// <summary>
    /// An abstract class to generate the points and segments of a Pathfigure object.
    /// </summary>
    public abstract class Drawer
    {
        private PointCollection _points = new PointCollection();

        /// <summary>
        /// Gets the drawer's points collection.
        /// </summary>
        protected PointCollection Points
        {
            get { return _points; }
        }

        private PathFigure _figure = new PathFigure();

        /// <summary>
        /// Gets the drawer's PathFigure object.
        /// </summary>
        public PathFigure Figure
        {
            get { return _figure; }
        }

        private double _width = double.NaN;

        /// <summary>
        /// Gets the width of the drawing area (if specified in the constructor).
        /// </summary>
        public double Width
        {
            get { return _width; }
        }

        private double _height = double.NaN;

        /// <summary>
        /// Gets the height of the drawing area (if specified in the constructor).
        /// </summary>
        public double Height
        {
            get { return _height; }
        }

        private double _strokeThickness;

        /// <summary>
        /// Gets the stroke thickness of the drawing (if specified in the constructor).
        /// </summary>
        public double StrokeThickness
        {
            get { return _strokeThickness; }
        }

        /// <summary>
        /// Initializes a new instance of Drawer.
        /// </summary>
        private Drawer()
        {
            _figure.IsClosed = true;
        }

        /// <summary>
        /// Initializes a new instance of Drawer.
        /// </summary>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public Drawer(
            double width,
            double height,
            double strokeThickness) : this()
        {
            _width = width;
            _height = height;
            _strokeThickness = strokeThickness;            
        }

        /// <summary>
        /// A method to override to generate the points and segments of the Figure object.
        /// </summary>
        public abstract void BuildFigure();

    }
}
