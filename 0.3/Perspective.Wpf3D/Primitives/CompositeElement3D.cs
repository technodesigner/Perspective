using System;
using System.Windows.Media.Media3D;
using System.Windows;
// using System.ComponentModel;
// using System.Windows.Markup;
using System.Collections.Generic;

namespace ODewit.Perspective.Primitives
{
    /// <summary>
    /// 
    /// </summary>
    // [ContentProperty("Children")]
    public class CompositeElement3D : UIElement3D
    {
        /*
            private readonly Visual3DCollection _children;

    // Methods
    public CompositeElement3D()
    {
        // Le lien magique : un constructeur internal...
        this._children = new Visual3DCollection(this);
    }

    protected override Visual3D GetVisual3DChild(int index)
    {
        return this._children[index];
    }

    // Properties
    protected Visual3DCollection Children
    {
        get
        {
            // base.VerifyAPIReadOnly();
            return this._children;
        }
    }

    protected override int Visual3DChildrenCount
    {
        get
        {
            return this._children.Count;
        }
    }
         * */


        private readonly List<UIElement3D> _children;

        public CompositeElement3D()
        {
            this._children = new List<UIElement3D>();
        }

        protected override Visual3D GetVisual3DChild(int index)
        {
            return this._children[index];
        }

        // [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        protected List<UIElement3D> Children
        {
            get
            {
                return this._children;
            }
        }

        protected override int Visual3DChildrenCount
        {
            get
            {
                return this._children.Count;
            }
        }

        /*
        /// <summary>
        /// Gets or sets the element's material.
        /// </summary>
        public Material Material
        {
            get { return (Material)GetValue(MaterialProperty); }
            set { SetValue(MaterialProperty, value); }
        }

        /// <summary>
        /// Identifies the Material dependency property.
        /// </summary>
        public static readonly DependencyProperty MaterialProperty =
            GeometryElement3D.MaterialProperty.AddOwner(
                typeof(CompositeElement3D),
                new PropertyMetadata(MaterialChanged));

        /// <summary>
        /// Callback called when the Material property value has changed.
        /// Assign the material to the inner 3D objects.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Callback arguments</param>
        internal static void MaterialChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CompositeElement3D ce = ((CompositeElement3D)d);
            foreach (UIElement3D v in ce._children)
            {
                if (v is GeometryElement3D)
                {
                    GeometryElement3D ge = (GeometryElement3D)v;
                    ge.Material = ce.Material;
                    ge.InvalidateModel();
                }
            }
        }
         * 
         * */
    }
}
