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

namespace Perspective.Wpf
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
    }

}
