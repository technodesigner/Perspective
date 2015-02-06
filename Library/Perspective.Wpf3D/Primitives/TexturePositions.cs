using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perspective.Wpf3D.Primitives
{
    /// <summary>
    /// An enumeration to identify the position of a texture (i.e. for a Bar3D).
    /// </summary>
    public enum TexturePositions
    {
        /// <summary>
        /// The texture is applied on the ends of the model.
        /// </summary>
        OnEnds,

        /// <summary>
        /// The texture is applied on the sides of the model.s
        /// </summary>
        OnSides
    }
}
