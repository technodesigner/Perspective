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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Perspective.Core.Wpf;

namespace Perspective.Wpf.Panels
{
    /// <summary>
    /// A honeycomb layout wrap panel.
    /// </summary>
    public class BeePanel : Panel
    {
        private double _rowHeight;
        private double _colWidth;
        private int _actualColumnCount;
        private int _actualRowCount;
        private bool _isInitialized;
        private Size _childSize = new Size();

        /// <summary>
        /// Identifies the ColumnCount dependency property. 
        /// </summary>
        private static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register(
                "ColumnCount",
                typeof(int),
                typeof(BeePanel),
                new PropertyMetadata(4, ColumnCountPropertyChanged),
                DPHelper.IsIntValuePositive);

        /// <summary>
        /// Gets or sets the number of columns. The default value is 4, and applies if the Width is not set.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return (int)base.GetValue(ColumnCountProperty);
            }
            set
            {
                base.SetValue(ColumnCountProperty, value);
            }
        }

        private static void ColumnCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BeePanel panel = (d as BeePanel);
            panel._isInitialized = false;
            panel.InvalidateArrange();
        }

        /// <summary>
        /// Identifies the RowCount dependency property.
        /// </summary>
        private static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register(
                "RowCount",
                typeof(int),
                typeof(BeePanel),
                new PropertyMetadata(4, RowCountPropertyChanged),
                DPHelper.IsIntValuePositive);

        /// <summary>
        /// Gets or sets the number of rows. The default value is 4, and applies if the Height is not set.
        /// </summary>
        public int RowCount
        {
            get
            {
                return (int)base.GetValue(RowCountProperty);
            }
            set
            {
                base.SetValue(RowCountProperty, value);
            }
        }

        private static void RowCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BeePanel panel = (d as BeePanel);
            panel._isInitialized = false;
            panel.InvalidateArrange();
        }

        /// <summary>
        /// Identifies the SideLength dependency property.
        /// </summary>
        private static readonly DependencyProperty SideLengthProperty =
            DependencyProperty.Register(
                "SideLength",
                typeof(double),
                typeof(BeePanel),
                new PropertyMetadata(100.0, SideLengthPropertyChanged),
                DPHelper.IsDoubleValuePositive);

        /// <summary>
        /// Gets or sets the length of an hexagon side (in pixels).
        /// </summary>
        public double SideLength
        {
            get
            {
                return (double)base.GetValue(SideLengthProperty);
            }
            set
            {
                base.SetValue(SideLengthProperty, value);
            }
        }

        private static void SideLengthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BeePanel panel = (d as BeePanel);
            panel._isInitialized = false;
            panel.InvalidateArrange();
        }

        private void Initialize()
        {
            if (!_isInitialized)
            {
                double sideLength = SideLength;
                _rowHeight = 2 * Math.Sin(GeometryHelper.DegreeToRadian(120.0)) * sideLength;
                _colWidth = 1.5 * sideLength;
                _actualColumnCount = Double.IsNaN(Width) ? ColumnCount : Convert.ToInt32((Width - 0.5 * sideLength) / _colWidth);
                _actualRowCount = (Double.IsNaN(Height)) ? RowCount : Convert.ToInt32((Height - 0.5 * _rowHeight) / _rowHeight);
                _childSize.Width = _childSize.Height = sideLength * 2;
                _isInitialized = true;
            }
        }

        /// <summary>
        /// Measures the size in layout required for child elements 
        /// and determines a size.
        /// </summary>
        /// <param name="availableSize">The available size that this element can give to child elements.</param>
        /// <returns>The size that this element determines it needs during layout, based on its calculations of child element sizes.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            Initialize();
            Size desiredPanelSize = new Size();
            if (!Double.IsNaN(Width))
            {
                desiredPanelSize.Width = this.Width;
            }
            else
            {
                desiredPanelSize.Width = SideLength * (1.5 * _actualColumnCount + 0.5);
            }
            if (!Double.IsNaN(Height))
            {
                desiredPanelSize.Height = this.Height;
            }
            else
            {
                desiredPanelSize.Height = _rowHeight * (_actualRowCount + 0.5);
            }
            foreach (FrameworkElement element in Children)
            {
                if (element != null)
                {
                    element.Measure(_childSize);
                }
            }
            return desiredPanelSize;
        }


        /// <summary>
        /// Provides the behavior for the "Arrange" pass of WPF layout.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this object should use to arrange itself and its children.</param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            PointCollection _points = new PointCollection();

            Initialize();

            double sideLength = SideLength;

            double yOffset = _rowHeight / 2.0;
            double xOffset = sideLength;

            double oddColOffset = _rowHeight / 2;

            for (int i = 0; i < _actualRowCount; i++)
            {
                for (int j = 0; j < _actualColumnCount; j++)
                {
                    Point p = new Point();
                    p.X = xOffset + _colWidth * j;
                    p.Y = yOffset + _rowHeight * i + (j % 2) * oddColOffset;
                    _points.Add(p);
                }
            }

            ArrangeChildren(_points, _childSize, _actualColumnCount, _actualRowCount);

            return finalSize;
        }

        /// <summary>
        /// Arranges the children of the panel.
        /// </summary>
        /// <param name="points">A point array.</param>
        /// <param name="childSize">The size of a child.</param>
        /// <param name="actualColumnCount">The column count.</param>
        /// <param name="actualRowCount">The row count.</param>
        protected virtual void ArrangeChildren(
            PointCollection points,
            Size childSize,
            int actualColumnCount,
            int actualRowCount)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (i < points.Count)
                {
                    Rect finalRect = new Rect(
                        points[i].X - (childSize.Width / 2),
                        points[i].Y - (childSize.Height / 2),
                        childSize.Width,
                        childSize.Height);

                    Children[i].Arrange(finalRect);
                }
            }
        }
    }
}
