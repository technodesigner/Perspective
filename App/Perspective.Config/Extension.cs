using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Perspective.Hosting;

namespace Perspective.Config
{
    /// <summary>
    /// Represents the WPF demo extension for the Perspective.exe application.
    /// </summary>
    public class Extension : Perspective.Hosting.Extension
    {
        private const string _title = "Configuration";
        private const string _iconKey = "PencilIcon";

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
            _pageInfos = new List<PageInfo>
            {
                new PageInfo(this)
                {
                    Title = "Skin",
                    IconKey = "PencilIcon",
                    PartialClassName = "View/SkinConfig.xaml"
                },
                new PageInfo(this)
                {
                    Title = "Localization",
                    IconKey = "PencilIcon",
                    PartialClassName = "View/LocalizationConfig.xaml"
                }
            };
            ResourceAssembly.Register();
            SkinManager.Current.LoadCurrentSkin();
        }
    }
}
