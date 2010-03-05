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

namespace Perspective.Core.Wpf
{
    /// <summary>
    /// An helper class for dependency properties.
    /// </summary>
    public static class DPHelper
    {
        /// <summary>
        /// Validation of a positive double value.
        /// </summary>
        /// <param name="value">Value to test.</param>
        /// <returns>Boolean value.</returns>
        public static bool IsDoubleValuePositive(object value)
        {
            return (((double)value) >= 0.0);
        }

        /// <summary>
        /// Validation of a double value which must be comprized between 0.0 and 0.5.
        /// </summary>
        /// <param name="value">Value to test.</param>
        /// <returns>Boolean value.</returns>
        public static bool IsDoubleValueBetween0AndDot5(object value)
        {
            double d = (double)value;
            return (d >= 0.0) && (d <= 0.5);
        }

        /// <summary>
        /// Validation of a positive int value.
        /// </summary>
        /// <param name="value">Value to test.</param>
        /// <returns>Boolean value.</returns>
        public static bool IsIntValuePositive(object value)
        {
            return (((int)value) >= 0);
        }

        /// <summary>
        /// Validation of an int value which must be greater or equal to 3.
        /// </summary>
        /// <param name="value">Value to test.</param>
        /// <returns>Boolean value.</returns>
        public static bool IsIntValueGreaterThan3(object value)
        {
            return (((int)value) >= 3);
        }

        /// <summary>
        /// Validation of an int value which must be greater or equal to 2.
        /// </summary>
        /// <param name="value">Value to test.</param>
        /// <returns>Boolean value.</returns>
        public static bool IsIntValueGreaterThan2(object value)
        {
            return (((int)value) >= 2);
        }
    }
}
