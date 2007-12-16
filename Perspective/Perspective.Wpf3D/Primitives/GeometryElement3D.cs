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
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace Perspective.Wpf3D.Primitives
{
    /// <summary>
    /// Basic abstract class for Perspective.Wpf3D elements with 
    /// an inner model.
    /// </summary>
    public abstract class GeometryElement3D : UIElement3D
    {
        /// <summary>
        /// Initializes a new instance of GeometryElement3D.
        /// </summary>
        protected GeometryElement3D()
        {
        }

        /// <summary>
        /// Initializes a new instance of GeometryElement3D.
        /// </summary>
        /// <param name="geometry">A Geometry3D object (i.e. MeshGeometry3D)</param>
        protected GeometryElement3D(Geometry3D geometry)
        {
            Geometry = geometry;
        }

        /// <summary>
        /// Gets or sets the element's model.
        /// This is a private property
        /// Pattern described here : http://blogs.msdn.com/wpf3d/archive/2007/09/05/subclassing-uielement3d.aspx
        /// </summary>
        private Model3D Model
        {
            get { return (Model3D)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        /// <summary>
        /// Identifies the Model dependency property.
        /// This is a private field
        /// Pattern described here : http://blogs.msdn.com/wpf3d/archive/2007/09/05/subclassing-uielement3d.aspx
        /// </summary>
        private static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register(
                "Model", 
                typeof(Model3D), 
                typeof(GeometryElement3D),
                new PropertyMetadata(ModelChanged));

        /// <summary>
        /// Callback called when the Model property's value has changed.
        /// Assign the Visual3DModel protected CLR property
        /// Pattern described here : http://blogs.msdn.com/wpf3d/archive/2007/09/05/subclassing-uielement3d.aspx
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void ModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GeometryElement3D element = (GeometryElement3D)d;
            element.Visual3DModel = (Model3D)e.NewValue;
        }

        /// <summary>
        /// Static constructor.
        /// Initialization of the default material.
        /// </summary>
        static GeometryElement3D()
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = Colors.Goldenrod;
            solidColorBrush.Opacity = 1.0;
        
            Material defaultMaterial = new DiffuseMaterial(solidColorBrush);

            BackMaterialProperty = DependencyProperty.Register(
                "BackMaterial", 
                typeof(Material), 
                typeof(GeometryElement3D),
                new PropertyMetadata(defaultMaterial, VisualPropertyChanged));

            MaterialProperty = DependencyProperty.Register(
                "Material",
                typeof(Material),
                typeof(GeometryElement3D),
                new PropertyMetadata(defaultMaterial, VisualPropertyChanged));
        }

        /// <summary>
        /// Gets or sets the element's back material.
        /// A default material is provided.
        /// </summary>
        public Material BackMaterial
        {
            get { return (Material)GetValue(BackMaterialProperty); }
            set { SetValue(BackMaterialProperty, value); }
        }

        /// <summary>
        /// Identifies the BackMaterial dependency property.
        /// </summary>
        public static readonly DependencyProperty BackMaterialProperty;

        /// <summary>
        /// Gets or sets the element's material.
        /// A default material is provided.
        /// </summary>
        public Material Material
        {
            get { return (Material)GetValue(MaterialProperty); }
            set { SetValue(MaterialProperty, value); }
        }

        /// <summary>
        /// Identifies the Material dependency property.
        /// </summary>
        public static readonly DependencyProperty MaterialProperty;

        /// <summary>
        /// Called by UIElement3D.InvalidateModel() to update the 3D model.
        /// Assignation of a new model to the Model property
        /// Pattern described here : http://blogs.msdn.com/wpf3d/archive/2007/09/05/subclassing-uielement3d.aspx
        /// </summary>
        protected override void OnUpdateModel()
        {
            GeometryModel3D model = new GeometryModel3D();
            // model.Geometry = BuildGeometry();
            model.Geometry = this.Geometry;
            model.Material = this.Material;
            model.BackMaterial = this.BackMaterial;
            Model = model;
        }

        /// <summary>
        /// Callback to call in subclasses when a visual dependency property value has changed (i.e. by databinding).
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        protected static void VisualPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GeometryElement3D element = (GeometryElement3D)d;
            element.InvalidateModel();
        }

        /// <summary>
        /// Gets or sets the element's geometry.
        /// </summary>
        protected Geometry3D Geometry
        {
            get { return (Geometry3D)GetValue(GeometryProperty); }
            set { SetValue(GeometryProperty, value); }
        }

        /// <summary>
        /// Identifies the Material dependency property.
        /// </summary>
        protected static readonly DependencyProperty GeometryProperty =
            DependencyProperty.Register(
                "Geometry", 
                typeof(Geometry3D), 
                typeof(GeometryElement3D),
                new PropertyMetadata(VisualPropertyChanged));
    }
}
