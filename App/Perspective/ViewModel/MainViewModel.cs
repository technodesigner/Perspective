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
using Perspective.Model;
using Perspective.Core.Wpf.Data;
using Perspective.Hosting;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Perspective.ViewModel
{
    /// <summary>
    /// Manages for the View the data that represent the extensions.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private ExtensionModel _model;

        /// <summary>
        /// Updates the CurrentPageInfo property.
        /// </summary>
        public SignalCommand SetCurrentPageInfoCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of MainViewModel.
        /// </summary>
        public MainViewModel()
        {
            _model = new ExtensionModel();
            SetCurrentPageInfoCommand = new SignalCommand();
            SetCurrentPageInfoCommand.Executed += (sender, e) =>
            {
                if ((e.Parameter != null) && (e.Parameter is PageInfo))
                {
                    CurrentPageInfo = e.Parameter as PageInfo;
                }
            };
        }

        /// <summary>
        /// Gets the Extension objects list.
        /// </summary>
        public IEnumerable<Extension> Extensions
        {
            get
            {
                return _model.Extensions;
            }
        }

        private PageInfo _currentPageInfo;

        /// <summary>
        /// Gets the current PageInfo object.
        /// </summary>
        public PageInfo CurrentPageInfo
        {
            get
            {
                return _currentPageInfo;
            }
            private set
            {
                _currentPageInfo = value;
                NotifyPropertyChanged("CurrentPageInfo");
            }
        }

        public BitmapImage RandomImage
        {
            get
            {
                return _model.RandomImage;
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method called when a property value changes.
        /// </summary>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
