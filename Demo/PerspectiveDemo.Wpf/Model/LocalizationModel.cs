using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerspectiveDemo.Wpf.Entities;
using Perspective.Wpf.ResourceStrings;
using System.Collections.ObjectModel;

namespace PerspectiveDemo.Wpf.Model
{
    public class LocalizationModel
    {
        private ObservableCollection<LocaleInfo> _localeInfos;
        private ResourceStringManager _resourceStringManager;


        public LocalizationModel()
        {
            _resourceStringManager = new ResourceStringManager(
                "PerspectiveDemo.Wpf",
                "Model.Strings.LocalizationModel");
            _localeInfos = new ObservableCollection<LocaleInfo>();
            Refresh();
        }

        public ObservableCollection<LocaleInfo> LocaleInfos
        {
            get { return _localeInfos; }
        }

        public void Refresh()
        {
            _localeInfos.Clear();
            _localeInfos.Add(
                new LocaleInfo()
                {
                    Title = _resourceStringManager.GetString("English"),
                    CultureName = "en-US"
                });
            _localeInfos.Add(
                new LocaleInfo()
                {
                    Title = _resourceStringManager.GetString("French"),
                    CultureName = "fr-FR"
                });
            // Console.WriteLine(_resourceStringManager.GetString("French"));
        }
    }
}
