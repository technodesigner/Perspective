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
using Perspective.Wpf3D.Primitives;
using System.Windows;
using System.Windows.Media.Media3D;
namespace Perspective.Wpf3D.Export
{
    /// <summary>
    /// 
    /// </summary>
    public interface I3dModelEncoder
    {
        /// <summary>
        /// Gets or sets the name of the model.
        /// </summary>
        string ModelName { get; set; }
        
        /// <summary>
        /// Gets or sets the Sculptor object.
        /// </summary>
        Sculptor Sculptor { get; set; }
        
        /// <summary>
        /// Gets or sets the Transform object applied to the points.
        /// </summary>
        Transform3D PointsTransform { get; set; }
        
        /// <summary>
        /// Saves the data.
        /// </summary>
        void Save();
    }
}
