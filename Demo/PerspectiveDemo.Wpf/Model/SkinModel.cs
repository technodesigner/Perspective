using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerspectiveDemo.Wpf.Model
{
    public class SkinModel
    {
        public List<string> Skins { get; private set; }

        public SkinModel()
        {
            Skins = new List<string>();
            Skins.Add("BlackAndWhite");
            Skins.Add("LaMoisson");
            Skins.Add("PsycheRock");
        }

        /// <summary>
        /// Gets the current skin.
        /// </summary>
        public string CurrentSkin
        {
            get
            {
                return SkinManager.Current.CurrentSkin;
            }
            internal set
            {
                SkinManager.Current.CurrentSkin = value;
            }
        }
    }
}
