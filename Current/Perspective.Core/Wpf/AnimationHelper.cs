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
using System.Text;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace Perspective.Core.Wpf
{
    /// <summary>
    /// A helper class for animation.
    /// </summary>
    public static class AnimationHelper
    {
        /// <summary>
        /// Returns a MediaElement's root Clock controller
        /// (Only root clocks can be controlled,
        /// so this function solves a problem when MediaElement.Clock is not a root clock)
        /// </summary>
        /// <param name="player">A MediaPlayer element</param>
        /// <returns>A MediaElement's root Clock controller.</returns>
        public static ClockController GetController(MediaElement player)
        {
            ClockController controller = null;
            Clock clock = player.Clock;
            if (clock.HasControllableRoot)
            {
                while (clock != null)
                {
                    if (clock.Controller != null)
                    {
                        controller = clock.Controller;
                        break;
                    }
                    clock = clock.Parent;
                }
            }
            return controller;
        }
    }
}
