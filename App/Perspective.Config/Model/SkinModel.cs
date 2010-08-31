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

namespace Perspective.Config.Model
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
