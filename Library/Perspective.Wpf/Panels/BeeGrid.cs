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
using System.Windows.Media;
using Perspective.Core.Wpf;

namespace Perspective.Wpf.Panels
{
    /// <summary>
    /// A honeycomb layout grid panel.
    /// </summary>
    public class BeeGrid : BeePanel
    {
        /// <summary>
        /// Identifies the Column attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ColumnProperty =
            DependencyProperty.RegisterAttached(
                "Column",
                typeof(int),
                typeof(BeeGrid),
                new PropertyMetadata(0),
                DPHelper.IsIntValuePositive);

        /// <summary>
        /// Gets the value of the Column attached property from the specified FrameworkElement.
        /// </summary>
        /// <param name="element">The element from which to read the property value.</param>
        /// <returns>The value of the Column attached property</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static int GetColumn(FrameworkElement element)
        {
            return (int)element.GetValue(ColumnProperty);
        }

        /// <summary>
        /// Sets the value of the Column attached property to the specified FrameworkElement.
        /// </summary>
        /// <param name="element">The element on which to set the Column attached property.</param>
        /// <param name="value">The property value to set.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetColumn(FrameworkElement element, int value)
        {
            element.SetValue(ColumnProperty, value);
        }

        /// <summary>
        /// Identifies the Row attached dependency property.
        /// </summary>
        public static readonly DependencyProperty RowProperty =
            DependencyProperty.RegisterAttached(
                "Row",
                typeof(int),
                typeof(BeeGrid),
                new PropertyMetadata(0),
                DPHelper.IsIntValuePositive);

        /// <summary>
        /// Gets the value of the Row attached property from the specified FrameworkElement.
        /// </summary>
        /// <param name="element">The element from which to read the property value.</param>
        /// <returns>The value of the Row attached property</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static int GetRow(FrameworkElement element)
        {
            return (int)element.GetValue(RowProperty);
        }

        /// <summary>
        /// Sets the value of the Row attached property to the specified FrameworkElement.
        /// </summary>
        /// <param name="element">The element on which to set the Row attached property.</param>
        /// <param name="value">The property value to set.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetRow(FrameworkElement element, int value)
        {
            element.SetValue(RowProperty, value);
        }

        /// <summary>
        /// Arranges the children of the panel.
        /// </summary>
        /// <param name="points">A point array.</param>
        /// <param name="childSize">The size of a child.</param>
        /// <param name="actualColumnCount">The column count.</param>
        /// <param name="actualRowCount">The row count.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        protected override void ArrangeChildren(
            PointCollection points,
            Size childSize,
            int actualColumnCount,
            int actualRowCount)
        {
            foreach (UIElement element in Children)
            {
                FrameworkElement fe = element as FrameworkElement;
                int row = GetRow(fe);
                int col = GetColumn(fe);
                int i = row * (actualColumnCount) + col;
                if (i >= points.Count)
                {
                    throw new IndexOutOfRangeException("Invalid Row or Column value.");
                }
                Rect finalRect = new Rect(
                    points[i].X - (childSize.Width / 2),
                    points[i].Y - (childSize.Height / 2),
                    childSize.Width,
                    childSize.Height);
                element.Arrange(finalRect);
            }
        }
    }
}
