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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Media3D;

namespace Perspective.Wpf3D.Panels
{
    /// <summary>
    /// Base class for panel for 3D elements.
    /// </summary>
    [ContentProperty("Children")]
    public abstract class SpaceBase3D : UIElement3D
    {
        private readonly List<UIElement3D> _children;
        private readonly Dictionary<UIElement3D, Transform3D> _transforms;

        /// <summary>
        /// Initializes a new Panel3D object.
        /// </summary>
        protected SpaceBase3D()
        {
            _children = new List<UIElement3D>();
            _transforms = new Dictionary<UIElement3D, Transform3D>();
        }

        /// <summary>
        /// Gets the ModelUIElement3D object list.
        /// </summary>
        public List<UIElement3D> Children
        {
            get { return _children; }
        }

        /// <summary>
        /// Gets the dictionary of transforms applied to inner elements.
        /// </summary>
        protected Dictionary<UIElement3D, Transform3D> Transforms
        {
            get { return _transforms; }
        } 

        /// <summary>
        /// Overrides Visual3D.GetVisual3DChild(int index).
        /// </summary>
        protected override Visual3D GetVisual3DChild(int index)
        {
            return _children[index];
        }

        /// <summary>
        /// Overrides Visual3D.Visual3DChildrenCount.
        /// </summary>
        protected override int Visual3DChildrenCount
        {
            get
            {
                return _children.Count;
            }
        }

        /// <summary>
        /// Defines the transform to apply to the 3D element.
        /// </summary>
        /// <param name="element">The 3D element.</param>
        /// <returns></returns>
        protected abstract Transform3D ComputeTransform(UIElement3D element);

        /// <summary>
        /// Called by UIElement3D.InvalidateModel() to update the 3D model.
        /// </summary>
        protected override void OnUpdateModel()
        {
            foreach (UIElement3D element in Children)
            {
                if (Transforms.ContainsKey(element) && element.Transform != null)
                {
                    (element.Transform as Transform3DGroup).Children.Remove(Transforms[element]);
                    Transforms[element] = ComputeTransform(element);
                    (element.Transform as Transform3DGroup).Children.Add(Transforms[element]);
                }
                else
                {
                    Transforms.Add(element, ComputeTransform(element));
                    var tg = new Transform3DGroup();
                    if (element.Transform != null)
                    {
                        tg.Children.Add(element.Transform);
                    }
                    tg.Children.Add(Transforms[element]);
                    element.Transform = tg;
                }
            }
            base.OnUpdateModel();
        }
    }
}
