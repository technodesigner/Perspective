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
using Perspective.Wpf3D.Primitives;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace Perspective.Wpf3D.Controls
{
    /// <summary>
    /// A 3D slider class compatible with range element multiselection.
    /// It supports Mouse wheel.
    /// </summary>
    public class Fader3D : RangeBase3D
    {
        /// <summary>
        /// Gets the name (without path and extension) of the XAML file that describes the appearence of the control.
        /// </summary>
        protected override string ContentFileName
        {
            get
            {
                return "Fader3D";
            }
        }

    }
}
