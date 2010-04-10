using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Perspective.Wpf.ResourceStrings;

namespace Perspective.Config
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
                    //"Perspective.Config",
                    Extension.AssemblyNameConst,
                    assembly);
                _registered = true;
            }
        }
    }
}
