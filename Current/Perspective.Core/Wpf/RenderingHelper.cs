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

        /// <summary>
        /// Static constructor.
        /// </summary>
        static RenderingHelper()
        {
            _renderingTier = (RenderCapability.Tier >> 16);
        }
    }
}
