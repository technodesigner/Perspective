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
    /// A 3D grid panel for 3D elements.
    /// </summary>
    public class Space3D : XYSpaceBase3D
    {
        /// <summary>
        /// Identifies the Z attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ZProperty =
            DependencyProperty.RegisterAttached(
                "Z",
                typeof(int),
                typeof(Space3D),
                new PropertyMetadata(0));

        /// <summary>
        /// Gets the value of the Z attached property from the specified UIElement3D.
        /// </summary>
        /// <param name="element">The element from which to read the property value.</param>
        /// <returns>The value of the Z attached property</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static int GetZ(UIElement3D element)
        {
            return (int)element.GetValue(ZProperty);
        }

        /// <summary>
        /// Sets the value of the Z attached property to the specified UIElement3D.
        /// </summary>
        /// <param name="element">The element on which to set the Z attached property.</param>
        /// <param name="value">The property value to set.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetZ(UIElement3D element, int value)
        {
            element.SetValue(ZProperty, value);
        }

        /// <summary>
        /// Defines the transform to apply to the 3D element.
        /// </summary>
        /// <param name="element">The 3D element.</param>
        /// <returns>A TranslateTransform3D object.</returns>
        protected override Transform3D ComputeTransform(UIElement3D element) 
        {
            int x = GetX(element);
            int y = GetY(element);
            int z = GetZ(element);
            return new TranslateTransform3D(x, y, z);
        }
    }
}