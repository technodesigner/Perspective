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
using Perspective.Core;
using System.Threading;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// A class to use to change current UI culture ; 
    /// it automatically refreshes all the attributes marked with ResourceString markup extension
    /// (because ResourceStringDecorator uses the UICultureChanged event).
    /// </summary>
    public class CultureManager
    {
        private CultureManager()
        {
        }

        private static CultureManager _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets a singleton instance of CultureManager.
        /// </summary>
        public static CultureManager Current
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CultureManager();
                    }
                    return _instance;
                }
            }
        }

        private Dictionary<string, Assembly> _resourceAssemblies = new Dictionary<string,Assembly>();

        /// <summary>
        /// Gets the resource assemblies collection.
        /// </summary>
        public Dictionary<string, Assembly> ResourceAssemblies
        {
            get { return _resourceAssemblies; }
        }

        private string _UICulture;

        /// <summary>
        /// Gets or sets the global UI culture for the thread
        /// (internally affects Thread.CurrentUICulture).
        /// </summary>
        public string UICulture
        {
            get { return _UICulture; }
            set 
            {
                if (value != _UICulture)
                {
                    string oldValue = _UICulture;
                    _UICulture = value;
                    Thread.CurrentThread.CurrentUICulture =
                        new CultureInfo(_UICulture);

                    // (Note : initialy, this property affected also Thread.CurrentCulture, 
                    // but it was not compatible with partial trust application)                    
                    //if (!ApplicationDeployment.IsNetworkDeployed)
                    //{
                    //    Thread.CurrentThread.CurrentCulture =
                    //        new CultureInfo(_UICulture);
                    //}

                    OnUICultureChanged(oldValue, _UICulture);
                }
            }
        }

        /// <summary>
        /// Fires the UICultureChanged event.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected void OnUICultureChanged(string oldValue, string newValue)
        {
            if (_UICultureChanged != null)
            {
                ChangedEventArgs<String> e = new ChangedEventArgs<String>(oldValue, newValue);
                _UICultureChanged(this, e);
            }
        }

        private event EventHandler<ChangedEventArgs<String>> _UICultureChanged;

        /// <summary>
        /// Event fired when the UICulture property has changed.
        /// </summary>
        public event EventHandler<ChangedEventArgs<String>> UICultureChanged
        {
            add
            {
                _UICultureChanged += value;
            }
            remove
            {
                _UICultureChanged -= value;
            }
        }
    }
}
