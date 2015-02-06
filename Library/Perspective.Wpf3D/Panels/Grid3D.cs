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
using Perspective.Core.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Media3D;

namespace Perspective.Wpf3D.Panels
{
    /// <summary>
    /// A grid panel for 3D elements.
    /// </summary>
    public class Grid3D : Panel3D
    {
        /// <summary>
        /// Identifies the X attached dependency property.
        /// </summary>
        public static readonly DependencyProperty XProperty =
            DependencyProperty.RegisterAttached(
                "X",
                typeof(int),
                typeof(Grid3D),
                new PropertyMetadata(0));

        /// <summary>
        /// Gets the value of the X attached property from the specified UIElement3D.
        /// </summary>
        /// <param name="element">The element from which to read the property value.</param>
        /// <returns>The value of the X attached property</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static int GetX(UIElement3D element)
        {
            return (int)element.GetValue(XProperty);
        }

        /// <summary>
        /// Sets the value of the X attached property to the specified UIElement3D.
        /// </summary>
        /// <param name="element">The element on which to set the X attached property.</param>
        /// <param name="value">The property value to set.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetX(UIElement3D element, int value)
        {
            element.SetValue(XProperty, value);
        }

        /// <summary>
        /// Identifies the Y attached dependency property.
        /// </summary>
        public static readonly DependencyProperty YProperty =
            DependencyProperty.RegisterAttached(
                "Y",
                typeof(int),
                typeof(Grid3D),
                new PropertyMetadata(0));

        /// <summary>
        /// Gets the value of the Y attached property from the specified UIElement3D.
        /// </summary>
        /// <param name="element">The element from which to read the property value.</param>
        /// <returns>The value of the Y attached property</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static int GetY(UIElement3D element)
        {
            return (int)element.GetValue(YProperty);
        }

        /// <summary>
        /// Sets the value of the Y attached property to the specified UIElement3D.
        /// </summary>
        /// <param name="element">The element on which to set the Y attached property.</param>
        /// <param name="value">The property value to set.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetY(UIElement3D element, int value)
        {
            element.SetValue(YProperty, value);
        }

        /// <summary>
        /// Defines the transform to apply to the 3D element.
        /// </summary>
        /// <param name="element">The 3D element.</param>
        /// <returns></returns>
        protected override Transform3D ComputeTransform(UIElement3D element) 
        {
            int x = GetX(element);
            int y = GetY(element);
            return new TranslateTransform3D(x, y, 0);
        }
    }
}