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
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace Perspective.Core.Wpf
{
    /// <summary>
    /// A helper class for Drawing.
    /// </summary>
    public static class DrawingHelper
    {
        /// <summary>
        /// Builds the layout of an element (i.e. for a RenderTargetBitmap or an XpsDocumentWriter).
        /// </summary>
        /// <param name="uie">UIElement object.</param>
        /// <param name="mediaSize">Size of the display media.</param>
        public static void BuildLayout(UIElement uie, Size mediaSize)
        {
            uie.Measure(mediaSize);
            uie.Arrange(new Rect(mediaSize));
            uie.UpdateLayout();
        }

        /// <summary>
        /// Creates a new Color structure by using the specified HSL color channel values.
        /// Algorithm from http://www.w3.org/TR/2011/REC-css3-color-20110607/#hsl-color
        /// </summary>
        /// <param name="h">Hue (angle in degrees)</param>
        /// <param name="s">Saturation (0 to 1)</param>
        /// <param name="l">Lightness (0 to 1)</param>
        /// <returns></returns>
        public static Color ColorFromHsl(double h, double s, double l)
        {
            h /= 360d;
            double m2 = l <= 0.5d ? l * (s + 1d) : l + s - l * s;
            double m1 = l * 2d - m2;
            double r = HueToRGB(m1, m2, h + 1d / 3d);
            double g = HueToRGB(m1, m2, h);
            double b = HueToRGB(m1, m2, h - 1d / 3d);
            return Color.FromRgb((byte)(r * 255d), (byte)(g * 255d), (byte)(b * 255d));
        }

        static double HueToRGB(double m1, double m2, double h)
        {

            if (h < 0)
            {
                h += 1d;
            }
            if (h > 1d)
            {
                h -= 1d;
            }
            if (h * 6d < 1d)
            {
                return m1 + (m2 - m1) * h * 6d;
            }
            if (h * 2d < 1d)
            {
                return m2;
            }
            if (h * 3d < 2d)
            {
                return m1 + (m2 - m1) * (2d / 3d - h) * 6d;
            }
            return m1;
        }

    }

}
