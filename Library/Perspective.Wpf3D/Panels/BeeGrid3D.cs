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
using System.Windows.Media.Media3D;

namespace Perspective.Wpf3D.Panels
{
    /// <summary>
    /// A honeycomb layout system for 3D models.
    /// </summary>
    public class BeeGrid3D : Grid3D
    {
        /// <summary>
        /// Indicates if a cell has a tip base.
        /// </summary>
        public bool HasTriangleCellCap
        {
            get { return (bool)GetValue(HasTriangleCellCapProperty); }
            set { SetValue(HasTriangleCellCapProperty, value); }
        }

        /// <summary>
        /// Identifies the HasTriangleCellCap dependency property.
        /// </summary>
        public static readonly DependencyProperty HasTriangleCellCapProperty =
            DependencyProperty.Register("HasTriangleCellCap", typeof(bool), typeof(BeeGrid3D), new PropertyMetadata(false, BeeGrid3DPropertiesChanged));

        private static void BeeGrid3DPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BeeGrid3D panel = ((BeeGrid3D)d);
            panel.InvalidateModel();
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
            double offsetX = 0d;
            double offsetY = 0d;
            double minLength = Math.Cos(GeometryHelper.DegreeToRadian(30d));
            if (HasTriangleCellCap)
            {
                offsetX = minLength * (x * 2d + y);
                offsetY = y * 1.5;
            }
            else 
            {
                offsetX = x * 1.5;
                offsetY = minLength * (y * 2d + x);
            }
            return new TranslateTransform3D(offsetX, offsetY, 0);
        }
    }
}
