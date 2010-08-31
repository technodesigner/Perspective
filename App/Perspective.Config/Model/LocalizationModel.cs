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
using System.Linq;
using System.Text;
using Perspective.Config.Entities;
using Perspective.Wpf.ResourceStrings;
using System.Collections.ObjectModel;

namespace Perspective.Config.Model
{
    public class LocalizationModel
    {
        private ObservableCollection<LocaleInfo> _localeInfos;
        private ResourceStringManager _resourceStringManager;


        public LocalizationModel()
        {
            _resourceStringManager = new ResourceStringManager(
                Extension.AssemblyNameConst,
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
        }
    }
}
