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
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Interop;
using System.Windows;

namespace Perspective.Core.Wpf
{
    /// <summary>
    /// A helper class for rendering operations.
    /// </summary>
    public class RenderingHelper
    {
        private static int _renderingTier;

        /// <summary>
        /// Gets the system rendering tier (0, 1 or 2).
        /// </summary>
        public static int RenderingTier
        {
            get { return _renderingTier; }
        }

        private static string _wpfSettingsRegistryPath = 
            @"SOFTWARE\Microsoft\Avalon.Graphics";
        private static string _disableHWAccelerationKey = "DisableHWAcceleration";

        private static void WriteDwordWpfSetting(string setting, int value)
        {
            using (RegistryKey wpfSettingsRegistryKey = 
                Registry.CurrentUser.CreateSubKey(_wpfSettingsRegistryPath))
            {
                wpfSettingsRegistryKey.SetValue(setting, value);
            }
        }

        private static int ReadDwordWpfSetting(string setting)
        {
            int returnValue = 0;
            using (RegistryKey wpfSettingsRegistryKey = 
                Registry.CurrentUser.CreateSubKey(_wpfSettingsRegistryPath))
            {
                object value = wpfSettingsRegistryKey.GetValue(setting);
                if (value != null)
                {
                    returnValue = (int)value;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static RenderingHelper()
        {
            _renderingTier = (RenderCapability.Tier >> 16);
        }

        /// <summary>
        /// Enables to turn off global hardware acceleration.
        /// </summary>
        public static bool IsHWAccelerationDisabled
        {
            get 
            {
                return (ReadDwordWpfSetting(_disableHWAccelerationKey) == 1); 
            }
            set 
            {
                int i = value ? 1 : 0;
                WriteDwordWpfSetting(_disableHWAccelerationKey, i);
            }
        }

        /// <summary>
        /// Enables to turn off/on hardware acceleration for a Window.
        /// </summary>
        /// <param name="visual">A visual element contained in the window.</param>
        /// <param name="renderMode">A RenderMode enumeration value.</param>
        public static void SetHwndHWAcceleration(Visual visual, RenderMode renderMode)
        {
            HwndSource hwndSource = PresentationSource.FromVisual(visual) as HwndSource;
            HwndTarget hwndTarget = hwndSource.CompositionTarget;
            hwndTarget.RenderMode = renderMode; 
        }
    }
}
