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
    /// A generic class to manage an event argument with one parameter.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    public class EventArgs<T> : EventArgs
    {
        private T _parameter;

        /// <summary>
        /// Initializes a new instance of generic EventArgs.
        /// </summary>
        /// <param name="parameter">The event parameter object.</param>
        public EventArgs(T parameter)
            : base()
        {
            _parameter = parameter;
        }

        /// <summary>
        /// Gets the parameter object.
        /// </summary>
        public T Parameter
        {
            get { return _parameter; }
        }
    }
}
