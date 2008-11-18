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
using System.Windows.Media;

namespace Perspective.Core
{
    /// <summary>
    /// A helper class for rendering and scaling operations.
    /// </summary>
    public static class RenderingHelper
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
            int _renderingTier = (RenderCapability.Tier >> 16);
        }

        /// <summary>
        /// Converts millimeters to DIP (Device Independant Pixels).
        /// </summary>
        /// <param name="mm">A millimeter value.</param>
        /// <returns>A DIP value.</returns>
        public static double MmToDip(double mm)
        {
            return CmToDip(mm / 10.0);
        }

        /// <summary>
        /// Converts centimeters to DIP (Device Independant Pixels).
        /// </summary>
        /// <param name="mm">A centimeter value.</param>
        /// <returns>A DIP value.</returns>
        public static double CmToDip(double cm)
        {
            return (cm * 96.0 / 2.54);
        }

        /// <summary>
        /// Converts inches to DIP (Device Independant Pixels).
        /// </summary>
        /// <param name="mm">An inch value.</param>
        /// <returns>A DIP value.</returns>
        public static double InchToDip(double inch)
        {
            return (inch * 96.0);
        }

        /// <summary>
        /// Converts font points to DIP (Device Independant Pixels).
        /// </summary>
        /// <param name="mm">A font point value.</param>
        /// <returns>A DIP value.</returns>
        public static double PtToDip(double pt)
        {
            return (pt * 96.0 / 72.0);
        }

        /// <summary>
        /// Converts DIP (Device Independant Pixels) to centimeters.
        /// </summary>
        /// <param name="mm">A DIP value.</param>
        /// <returns>A centimeter value.</returns>
        public static double DipToCm(double dip)
        {
            return (dip * 2.54 / 96.0);
        }

        /// <summary>
        /// Converts DIP (Device Independant Pixels) to millimeters.
        /// </summary>
        /// <param name="mm">A DIP value.</param>
        /// <returns>A millimeter value.</returns>
        public static double DipToMm(double dip)
        {
            return DipToCm(dip) * 10.0;
        }


    }
}
