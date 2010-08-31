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
using Perspective.Hosting;
using Perspective.Wpf.ResourceStrings;

namespace Perspective.Config
{
    /// <summary>
    /// Represents the configuration extension for the Perspective.exe application.
    /// </summary>
    public class Extension : Perspective.Hosting.Extension
    {
        private const string _title = "Configuration";
        private const string _iconKey = "ConfigIcon";

        private static string _assemblyName = "Perspective.Config";

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        public static string AssemblyNameConst
        {
            get { return Extension._assemblyName; }
        }

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        public override string AssemblyName
        {
            get
            {
                return _assemblyName;
            }
        }

        private List<PageInfo> _pageInfos;

        /// <summary>
        /// Gets the children PageInfos collection.
        /// </summary>
        public override List<PageInfo> PageInfos
        {
            get
            {
                return _pageInfos;
            }
        }

        /// <summary>
        /// Initializes a new instance of Extension.
        /// </summary>
        public Extension()
            : base()
        {
            Title = _title;
            IconKey = _iconKey;
            SortOrder = 90;

            ResourceAssembly.Register();
            ResourceStringManager resourceStringManager = new ResourceStringManager(
                AssemblyNameConst,
                "Strings.Extension");

            _pageInfos = new List<PageInfo>
            {
                new PageInfo(this)
                {
                    Title = resourceStringManager.GetString("About"),
                    IconKey = _iconKey,
                    PartialClassName = "View/About.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Skin",
                    IconKey = _iconKey,
                    PartialClassName = "View/SkinConfig.xaml"
                },
                new PageInfo(this)
                {
                    Title = resourceStringManager.GetString("Localization"),
                    IconKey = _iconKey,
                    PartialClassName = "View/LocalizationConfig.xaml"
                },
                new PageInfo(this)
                {
                    Title = "DPI",
                    IconKey = _iconKey,
                    PartialClassName = "View/DpiScaling.xaml"
                },
                new PageInfo(this)
                {
                    Title = resourceStringManager.GetString("Rendering"),
                    IconKey = _iconKey,
                    PartialClassName = "View/RenderingConfig.xaml"
                },
            };
            SkinManager.Current.LoadCurrentSkin();
        }
    }
}
