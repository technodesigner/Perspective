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

namespace Perspective.Core.Wpf.Converters
{
    /// <summary>
    /// A converter which throws conversion events.
    /// </summary>
    public class SignalConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return OnConverting(value, targetType, parameter, culture);
        }

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return OnConvertingBack(value, targetType, parameter, culture);
        }

        #endregion

        /// <summary>
        /// Fires the Converting event.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        protected object OnConverting(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object ret = null;
            if (_converting != null)
            {
                ConverterEventArgs e = new ConverterEventArgs(value, targetType, parameter, culture);
                _converting(this, e);
                ret = e.ConvertedValue;
            }
            return ret;
        }

        private event EventHandler<ConverterEventArgs> _converting;

        /// <summary>
        /// Event fired when the conversion occurs.
        /// </summary>
        public event EventHandler<ConverterEventArgs> Converting
        {
            add
            {
                _converting += value;
            }
            remove
            {
                _converting -= value;
            }
        }

        /// <summary>
        /// Fires the ConvertingBack event.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        protected object OnConvertingBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object ret = null;
            if (_convertingBack != null)
            {
                ConverterEventArgs e = new ConverterEventArgs(value, targetType, parameter, culture);
                _convertingBack(this, e);
                ret = e.ConvertedValue;
            }
            return ret;
        }

        private event EventHandler<ConverterEventArgs> _convertingBack;

        /// <summary>
        /// Event fired when the back conversion occurs.
        /// </summary>
        public event EventHandler<ConverterEventArgs> ConvertingBack
        {
            add
            {
                _convertingBack += value;
            }
            remove
            {
                _convertingBack -= value;
            }
        }    

    }
}
