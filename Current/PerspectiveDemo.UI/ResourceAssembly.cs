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
using System.Text;
using Perspective.Wpf.ResourceStrings;
using System.Reflection;

namespace PerspectiveDemo.UI
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
                    "PerspectiveDemo.UI",
                    assembly);
                _registered = true;
            }
        }
    }
}
