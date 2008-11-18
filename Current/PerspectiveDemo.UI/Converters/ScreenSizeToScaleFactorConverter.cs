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
using System.Windows.Data;
using Perspective.Core;
using System.Windows;
using Perspective.Wpf;

namespace PerspectiveDemo.UI.Converters
{
    [ValueConversion(typeof(string), typeof(double))]
    public class ScreenSizeToScaleFactorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double scaleFactor = 0.0;
            if (!String.IsNullOrEmpty((string)value))
            {
                double diagonalScreenSize = System.Convert.ToDouble(value, culture);
                Point scale = DipHelper.GetScreenIndependentScaleFactor(diagonalScreenSize);
                Point physicalDpi = DipHelper.GetPhysicalDpi(diagonalScreenSize);
                string sParameter = (string)parameter;
                if (sParameter == "ScaleY")
                {
                    scaleFactor = scale.Y;
                }
                else if (sParameter == "ScaleX")
                {
                    scaleFactor = scale.X;
                }
                else if (sParameter == "PhysicalDpi")
                {
                    scaleFactor = physicalDpi.X;
                }
            }
            scaleFactor = Math.Round(scaleFactor, 2, MidpointRounding.ToEven);
            return scaleFactor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
