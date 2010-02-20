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
using System.Windows;
using Perspective.Core;

namespace Perspective.Wpf
{
    /// <summary>
    /// A helper class for DIP (Device Independent Pixels) conversion and scaling operations.
    /// </summary>
    public static class DipHelper
    {

        /// <summary>
        /// Static constructor.
        /// </summary>
        static DipHelper()
        {
            if (AssemblyConfigManager.Settings[_screenSizeKey] == null)
            {
                AssemblyConfigManager.Settings[_screenSizeKey] = 17.0;
            }

            if (AssemblyConfigManager.Settings[_screenIndependentScaleXKey] == null)
            {
                AssemblyConfigManager.Settings[_screenIndependentScaleXKey] = 1.0;
            }
            else
            {
                ScreenIndependentScaleTransform.ScaleX = (double)AssemblyConfigManager.Settings[_screenIndependentScaleXKey];
            }

            if (AssemblyConfigManager.Settings[_screenIndependentScaleYKey] == null)
            {
                AssemblyConfigManager.Settings[_screenIndependentScaleYKey] = 1.0;
            }
            else
            {
                ScreenIndependentScaleTransform.ScaleY = (double)AssemblyConfigManager.Settings[_screenIndependentScaleYKey];
            }

            if (AssemblyConfigManager.Settings[_screenSizeKey] == null)
            {
                AssemblyConfigManager.Settings[_screenSizeKey] = 17.0;
            }            
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
        /// <param name="cm">A centimeter value.</param>
        /// <returns>A DIP value.</returns>
        public static double CmToDip(double cm)
        {
            return (cm * 96.0 / 2.54);
        }

        /// <summary>
        /// Converts inches to DIP (Device Independant Pixels).
        /// </summary>
        /// <param name="inch">An inch value.</param>
        /// <returns>A DIP value.</returns>
        public static double InchToDip(double inch)
        {
            return (inch * 96.0);
        }

        /// <summary>
        /// Converts font points to DIP (Device Independant Pixels).
        /// </summary>
        /// <param name="pt">A font point value.</param>
        /// <returns>A DIP value.</returns>
        public static double PtToDip(double pt)
        {
            return (pt * 96.0 / 72.0);
        }

        /// <summary>
        /// Converts DIP (Device Independant Pixels) to centimeters.
        /// </summary>
        /// <param name="dip">A DIP value.</param>
        /// <returns>A centimeter value.</returns>
        public static double DipToCm(double dip)
        {
            return (dip * 2.54 / 96.0);
        }

        /// <summary>
        /// Converts DIP (Device Independant Pixels) to millimeters.
        /// </summary>
        /// <param name="dip">A DIP value.</param>
        /// <returns>A millimeter value.</returns>
        public static double DipToMm(double dip)
        {
            return DipToCm(dip) * 10.0;
        }

        /// <summary>
        /// Gets the system DPI scale factor (compared to 96 dpi).
        /// From http://blogs.msdn.com/jaimer/archive/2007/03/07/getting-system-dpi-in-wpf-app.aspx
        /// Should not be called before the Loaded event (else XamlException mat throw)
        /// </summary>
        /// <returns>A Point object containing the X- and Y- scale factor.</returns>
        private static Point GetSystemDpiFactor()
        {
            PresentationSource source = PresentationSource.FromVisual(Application.Current.MainWindow);
            Matrix m = source.CompositionTarget.TransformToDevice;
            return new Point(m.M11, m.M22);
        }

        private const double _dpiBase = 96.0;

        /// <summary>
        /// Gets the system configured DPI.
        /// </summary>
        /// <returns>A Point object containing the X- and Y- DPI.</returns>
        public static Point GetSystemDpi()
        {
            Point sysDpiFactor = GetSystemDpiFactor();
            return new Point(
                sysDpiFactor.X * _dpiBase,
                sysDpiFactor.Y * _dpiBase);
        }

        /// <summary>
        /// Gets the physical pixel density (DPI) of the screen.
        /// </summary>
        /// <param name="diagonalScreenSize">Size - in inch - of the diagonal of the screen.</param>
        /// <returns>A Point object containing the X- and Y- DPI.</returns>
        public static Point GetPhysicalDpi(double diagonalScreenSize)
        {
            Point sysDpiFactor = GetSystemDpiFactor();
            double pixelScreenWidth = SystemParameters.PrimaryScreenWidth * sysDpiFactor.X;
            double pixelScreenHeight = SystemParameters.PrimaryScreenHeight * sysDpiFactor.Y;
            double formatRate = pixelScreenWidth / pixelScreenHeight;

            double inchHeight = diagonalScreenSize / Math.Sqrt(formatRate * formatRate + 1.0);
            double inchWidth = formatRate * inchHeight;

            double xDpi = Math.Round(pixelScreenWidth / inchWidth);
            double yDpi = Math.Round(pixelScreenHeight / inchHeight);

            return new Point(xDpi, yDpi);
        }

        /// <summary>
        /// Converts a DPI into a scale factor (compared to system DPI).
        /// </summary>
        /// <param name="dpi">A Point object containing the X- and Y- DPI to convert.</param>
        /// <returns>A Point object containing the X- and Y- scale factor.</returns>
        public static Point DpiToScaleFactor(Point dpi)
        {
            Point sysDpi = GetSystemDpi();
            return new Point(
                dpi.X / sysDpi.X,
                dpi.Y / sysDpi.Y);
        }

        /// <summary>
        /// Gets the scale factor to apply to a WPF application
        /// so that 96 DIP always equals 1 inch on the screen (whatever the system DPI).
        /// </summary>
        /// <param name="diagonalScreenSize">Size - in inch - of the diagonal of the screen</param>
        /// <returns>A Point object containing the X- and Y- scale factor.</returns>
        public static Point GetScreenIndependentScaleFactor(double diagonalScreenSize)
        {
            return DpiToScaleFactor(GetPhysicalDpi(diagonalScreenSize));
        }

        private static string _screenIndependentScaleXKey = "ScreenIndependentScaleX";
        private static string _screenIndependentScaleYKey = "ScreenIndependentScaleY";
        private static string _screenSizeKey = "ScreenSize";

        /// <summary>
        /// Gets or sets the screen independent X scale factor.
        /// It must be initialized to a real value.
        /// </summary>
        public static double ScreenIndependentScaleX
        {
            get { return (double)AssemblyConfigManager.Settings[_screenIndependentScaleXKey]; }
            set 
            {
                _screenIndependentScaleTransform.ScaleX = value;
                AssemblyConfigManager.Settings[_screenIndependentScaleXKey] = value; 
            }
        }

        /// <summary>
        /// Gets or sets the screen independent Y scale factor.
        /// It must be initialized to a real value.
        /// </summary>
        public static double ScreenIndependentScaleY
        {
            get { return (double)AssemblyConfigManager.Settings[_screenIndependentScaleYKey]; }
            set 
            {
                _screenIndependentScaleTransform.ScaleY = value;
                AssemblyConfigManager.Settings[_screenIndependentScaleYKey] = value; 
            }
        }

        private static ScaleTransform _screenIndependentScaleTransform = new ScaleTransform();

        /// <summary>
        /// Gets the screen independent scale transform object.
        /// It must be initialized to a real scale factor.
        /// </summary>
        public static ScaleTransform ScreenIndependentScaleTransform
        {
            get { return DipHelper._screenIndependentScaleTransform; }
        }

        /// <summary>
        /// Gets or sets the machine diagonal screen size.
        /// It must be initialized to a real value.
        /// </summary>
        public static double ScreenSize
        {
            get { return (double)AssemblyConfigManager.Settings[_screenSizeKey]; }
            set { AssemblyConfigManager.Settings[_screenSizeKey] = value; }
        }
    }
}
