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
using System.Windows;

namespace Perspective.Core
{
    /// <summary>
    /// A generic class to manage one routed event argument.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RoutedEventArgs<T> : RoutedEventArgs
    {
        private T _value;

        /// <summary>
        /// Initializes a new instance of generic RoutedEventArgs.
        /// </summary>
        /// <param name="value">The value of the event argument.</param>
        public RoutedEventArgs(T value) : base()
        {
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of generic RoutedEventArgs.
        /// </summary>
        /// <param name="routedEvent">The routed event identifier for this instance of the RoutedEventArgs class.</param>
        /// <param name="value">The value of the event argument.</param>
        public RoutedEventArgs(RoutedEvent routedEvent, T value) : base(routedEvent)
        {
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of generic RoutedEventArgs.
        /// </summary>
        /// <param name="routedEvent">The routed event identifier for this instance of the RoutedEventArgs class.</param>
        /// <param name="source">An alternate source that will be reported when the event is handled. This pre-populates the Source property.</param>
        /// <param name="value">The value of the event argument.</param>
        public RoutedEventArgs(RoutedEvent routedEvent, object source, T value)
            : base(routedEvent, source)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the argument value.
        /// </summary>
        public T Value
        {
            get { return _value; }
        }
    }
}
