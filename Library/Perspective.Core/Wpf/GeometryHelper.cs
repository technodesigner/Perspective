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
using System.Windows;

namespace Perspective.Core.Wpf
{
    /// <summary>
    /// A helper class for geometry operations.
    /// </summary>
    public static class GeometryHelper
    {
        /// <summary>
        /// Converts a degree angle value in radian.
        /// </summary>
        /// <param name="degree">Angle value in degree.</param>
        /// <returns>Angle value in radian.</returns>
        public static double DegreeToRadian(double degree)
        {
            return (degree / 180.0) * Math.PI;
        }

        /// <summary>
        /// Converts a radian angle value in degree.
        /// </summary>
        /// <param name="radian">Angle value in radian.</param>
        /// <returns>Angle value in degree.</returns>
        public static double RadianToDegree(double radian)
        {
            return radian * 180.0 / Math.PI;
        }
    }
}
