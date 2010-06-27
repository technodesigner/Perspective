using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Perspective.Core.Wpf.Data;
using Perspective.Wpf.ResourceStrings;
using Perspective.Config.Model;
using Perspective.Config.Entities;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading;

namespace Perspective.Config.ViewModel
{
    public class LocalizationViewModel : INotifyPropertyChanged
    {
        private LocalizationModel _model;

        public ObservableCollection<LocaleInfo> LocaleInfos
        {
            get { return _model.LocaleInfos; }
        }

        /// <summary>
        /// Changes the current culture.
        /// </summary>
        public SignalCommand SetCurrentLocaleInfoCommand { get; private set; }

        public LocalizationViewModel()
        {
            _model = new LocalizationModel();
            foreach (LocaleInfo localeInfo in LocaleInfos)
            {
                if (localeInfo.CultureName == Thread.CurrentThread.CurrentCulture.Name)
                {
                    CurrentLocaleInfo = localeInfo;
                    break;
                }
            }
            CultureManager.Current.UICultureChanged += new EventHandler<Perspective.Core.ChangedEventArgs<string>>(Current_UICultureChanged);

            SetCurrentLocaleInfoCommand = new SignalCommand();
            SetCurrentLocaleInfoCommand.Executed += (sender, e) =>
            {
                if ((e.Parameter != null) && (e.Parameter is LocaleInfo))
                {
                    CurrentLocaleInfo = e.Parameter as LocaleInfo;
                    CultureManager.Current.UICulture = CurrentLocaleInfo.CultureName;
                }
            };
        }

        void Current_UICultureChanged(object sender, Perspective.Core.ChangedEventArgs<string> e)
        {
            _model.Refresh();
        }

        private LocaleInfo _currentLocaleInfo;

        /// <summary>
        /// Gets the current LocaleInfo object.
        /// </summary>
        public LocaleInfo CurrentLocaleInfo
        {
            get
            {
                return _currentLocaleInfo;
            }
            private set
            {
                _currentLocaleInfo = value;
                NotifyPropertyChanged("CurrentLocaleInfo");
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
