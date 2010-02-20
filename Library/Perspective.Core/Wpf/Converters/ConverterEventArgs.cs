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

namespace Perspective.Core.Wpf.Converters
{
    /// <summary>
    /// Provides event data for SignalConverter related events. 
    /// </summary>
    public class ConverterEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type of the target.</param>
        /// <param name="parameter">Parameter of the converter.</param>
        /// <param name="culture">Current culture of the converter.</param>
        public ConverterEventArgs(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            _value = value;
            _targetType = targetType;
            _parameter = parameter;
            _culture = culture;
        }

        private object _value;

        /// <summary>
        /// Gets the value to convert.
        /// </summary>
        public object Value
        {
            get { return _value; }
        }

        private object _convertedValue;

        /// <summary>
        /// Gets the converted value.
        /// </summary>
        public object ConvertedValue
        {
            get { return _convertedValue; }
            set { _convertedValue = value; }
        }
        
        private Type _targetType;

        /// <summary>
        /// Gets the type of the target object.
        /// </summary>
        public Type TargetType
        {
            get { return _targetType; }
        }

        private object _parameter;

        /// <summary>
        /// Gets the parameter of the converter.
        /// </summary>
        public object Parameter
        {
            get { return _parameter; }
        }

        private System.Globalization.CultureInfo _culture;

        /// <summary>
        /// Gets the current culture of the converter.
        /// </summary>
        public System.Globalization.CultureInfo Culture
        {
            get { return _culture; }
        }
    }
}
