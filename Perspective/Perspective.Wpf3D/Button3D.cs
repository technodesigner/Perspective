using System;
using Perspective.Wpf3D.Primitives;


namespace Perspective.Wpf3D
{
    /// <summary>
    /// A push button 3D control.
    /// </summary>
    public class Button3D : ButtonBase3D
    {
        /// <summary>
        /// Gets the name (without path and extension) of the XAML file that describes the appearence of the control.
        /// </summary>
        protected override string ContentFileName
        {
            get
            {
                return "Button3D";
            }
        }
    }
}
