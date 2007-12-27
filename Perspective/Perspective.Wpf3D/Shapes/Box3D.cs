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
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Perspective.Wpf3D.Primitives;
using Perspective.Wpf3D.Sculptors;

namespace Perspective.Wpf3D.Shapes
{
    /// <summary>
    /// A 3D box element.
    /// The same material is used for all the faces through the Material property.
    /// Default size of a side is 1.0.
    /// </summary>
    public class Box3D : GeometryElement3D
    {
        private BoxSculptor _sculptor = new BoxSculptor();

        /// <summary>
        /// Called by UIElement3D.InvalidateModel() to update the 3D model.
        /// </summary>
        protected override void OnUpdateModel()
        {
            _sculptor.Initialize(VisibleSides);
            _sculptor.BuildMesh();
            Geometry = _sculptor.Mesh;
            base.OnUpdateModel();
        }

        /// <summary>
        /// Gets or sets the sides of the model.
        /// Default is BoxSides.All
        /// XAML usage : VisibleSides="Back,Left,Right,Up,Down"
        /// </summary>
        public BoxSides VisibleSides
        {
            get { return (BoxSides)GetValue(VisibleSidesProperty); }
            set { SetValue(VisibleSidesProperty, value); }
        }

        /// <summary>
        /// Identifies the VisibleSides dependency property.
        /// </summary>
        public static readonly DependencyProperty VisibleSidesProperty =
            DependencyProperty.Register(
                "VisibleSides", 
                typeof(BoxSides), 
                typeof(Box3D), 
                new UIPropertyMetadata(
                    BoxSides.All,
                    VisualPropertyChanged));
    }
}
