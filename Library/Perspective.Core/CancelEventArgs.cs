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
using System.ComponentModel;

namespace Perspective.Core
{
    /// <summary>
    /// A generic class to manage a CancelEventArgs argument with one parameter.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    public class CancelEventArgs<T> : CancelEventArgs
    {
        private T _parameter;

        /// <summary>
        /// Initializes a new instance of generic EventArgs.
        /// </summary>
        /// <param name="parameter">The value of the event argument.</param>
        public CancelEventArgs(T parameter)
            : base()
        {
            _parameter = parameter;
        }

        /// <summary>
        /// Gets the argument value.
        /// </summary>
        public T Parameter
        {
            get { return _parameter; }
        }
    }
}
