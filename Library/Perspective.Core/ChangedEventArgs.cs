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

namespace Perspective.Core
{
    /// <summary>
    /// A generic class to manage an event argument
    /// with 2 values (OldValue and NewValue), 
    /// corresponding to the values of a property before and after a change.
    /// </summary>
    /// <typeparam name="T">The type of the 2 values.</typeparam>
    public class ChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of EventArgs.
        /// </summary>
        /// <param name="oldValue">The value before the change.</param>
        /// <param name="newValue">The value after the change.</param>
        public ChangedEventArgs(T oldValue, T newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        private T _oldValue;

        /// <summary>
        /// Gets the value before the change.
        /// </summary>
        public T OldValue
        {
            get { return _oldValue; }
        }

        private T _newValue;

        /// <summary>
        /// Gets the value after the change.
        /// </summary>
        public T NewValue
        {
            get { return _newValue; }
        }
    }
}
