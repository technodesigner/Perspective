using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Perspective.Config.Model;
using Perspective.Core.Wpf.Data;
using System.ComponentModel;
using System.Windows;
using Perspective.Core;

namespace Perspective.Config.ViewModel
{
    public class SkinViewModel : INotifyPropertyChanged
    {
        private SkinModel _model;

        public List<string> Skins
        {
            get { return _model.Skins; }
        }

        /// <summary>
        /// Changes the current skin.
        /// </summary>
        public SignalCommand SetCurrentSkinCommand { get; private set; }

        //private static string _currentSkinKey = "CurrentSkin";

        public SkinViewModel()
        {
            _model = new SkinModel();

            // CurrentSkin = (string)AssemblyConfigManager.Settings[_currentSkinKey];
            //if (!String.IsNullOrEmpty(CurrentSkin))
            //{
            //    LoadApplicationSkin(CurrentSkin);
            //}
            //else
            //{
            //    SkinManager.Current.LoadDefaultSkin();
            //    Perspective.Wpf.SkinManager.Current.LoadDefaultSkin();
            //    Perspective.Wpf3D.SkinManager.Current.LoadDefaultSkin();
            //}

            SetCurrentSkinCommand = new SignalCommand();
            SetCurrentSkinCommand.Executed += (sender, e) =>
            {
                if ((e.Parameter != null) && (e.Parameter is string))
                {
                    CurrentSkin = e.Parameter as string;
                    // LoadApplicationSkin(CurrentSkin);
                    // AssemblyConfigManager.Settings[_currentSkinKey] = CurrentSkin;
                    // AssemblyConfigManager.SaveSettings();
                }
            };
        }

        //private void LoadApplicationSkin(string skin)
        //{
        //    SkinManager.Current.LoadSkin(skin);
        //    Perspective.Wpf.SkinManager.Current.LoadSkin(skin);
        //    Perspective.Wpf3D.SkinManager.Current.LoadSkin(skin);
        //}

        // private string _currentSkin;

        /// <summary>
        /// Gets the current skin.
        /// </summary>
        public string CurrentSkin
        {
            get
            {
                return _model.CurrentSkin;
            }
            internal set
            {
                _model.CurrentSkin = value;
                NotifyPropertyChanged("CurrentSkin");
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
