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
using Perspective.Core.Wpf.Converters;

namespace Perspective.Core.Wpf.Data
{
    /// <summary>
    /// A binding class which throws conversion events. It prevents to write converter classes.
    /// </summary>
    public class SignalBinding : System.Windows.Data.Binding
    {
        /// <summary>
        /// Initializes a new instance of Binding.
        /// </summary>
        public SignalBinding()
        {
            SignalConverter converter = new SignalConverter();

#if DN35
            converter.Converting += (sender, e) =>
#else
            converter.Converting += delegate(object sender, ConverterEventArgs e)
#endif
            {
                e.ConvertedValue = OnConverting(e);
            };


#if DN35
            converter.ConvertingBack += (sender, e) =>
#else
            converter.ConvertingBack += delegate(object sender, ConverterEventArgs e)
#endif
            {
                e.ConvertedValue = OnConvertingBack(e);
            };

            this.Converter = converter;
        }

        /// <summary>
        /// Fires the Converting event.
        /// </summary>
        /// <returns>A converted value.</returns>
        protected object OnConverting(ConverterEventArgs e)
        {
            object ret = null;
            if (_converting != null)
            {
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
        /// <returns>A converted value.</returns>
        protected object OnConvertingBack(ConverterEventArgs e)
        {
            object ret = null;
            if (_convertingBack != null)
            {
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
