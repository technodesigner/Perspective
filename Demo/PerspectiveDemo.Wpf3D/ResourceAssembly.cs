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
using System.Reflection;
using Perspective.Wpf.ResourceStrings;

namespace PerspectiveDemo.Wpf3D
{
    public static class ResourceAssembly
    {
        private static bool _registered;

        public static void Register()
        {
            if (!_registered)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                CultureManager.Current.ResourceAssemblies.Add(
                    Extension.AssemblyNameConst,
                    assembly);
                _registered = true;
            }
        }
    }
}
