﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Perspective.Wpf.ResourceStrings;

namespace PerspectiveDemo.Wpf
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