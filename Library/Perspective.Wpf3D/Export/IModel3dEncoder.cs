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
    public interface I3dModelEncoder
    {
        string ModelName { get; set; }
        Sculptor Sculptor { get; set; }
        Transform3D PointsTransform { get; set; }
        void Save();
    }
}
