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
using System.Text;
using System.Windows;
using Perspective.Core.Primitives;
using System.Windows.Input;
using System.Windows.Controls;
using Perspective.Core.Wpf;

namespace Perspective.Wpf3D.Primitives
{
    /// <summary>
    /// Represents a 3D element that has a value within a specific range.
    /// </summary>
    [Perspective.Core.SkinPart(Name = "PART_SelectionIndicator", NamedPartType = typeof(RangeBase3D))]
    public abstract class RangeBase3D : ContentControl3D, IRangeElement
    {
        static RangeBase3D()
        {
            ContentProperty.OverrideMetadata(
                typeof(RangeBase3D),
                new UIPropertyMetadata(
                    ContentPropertyChanged));
        }

        /// <summary>
        /// Callback called when the Content property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void ContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBase3D rb = (RangeBase3D)d;
            rb._selectionIndicator = (UIElement3D)rb.FindName("PART_SelectionIndicator");
            rb.AdjustSelectionIndicatorVisibility();
        }

        /// <summary>
        /// Gets or sets the Minimum possible Value of the range element. 
        /// This is a dependency property.
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        
        /// <summary>
        /// Identifies the Minimum dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                "Minimum",
                typeof(double),
                typeof(RangeBase3D),
                new PropertyMetadata(
                    0.0,
                    MinimumPropertyChanged,
                    MinimumCoerceValue),
                DoubleValidateValue);


        /// <summary>
        /// Gets or sets the highest possible Value of the range element. 
        /// This is a dependency property.
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Identifies the Maximum dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                "Maximum",
                typeof(double),
                typeof(RangeBase3D),
                new PropertyMetadata(
                    1.0,
                    MaximumPropertyChanged,
                    MaximumCoerceValue),
                DoubleValidateValue);

        /// <summary>
        /// Gets or sets the current magnitude of the range element. 
        /// This is a dependency property.
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Identifies the Value dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(double),
                typeof(RangeBase3D),
                new PropertyMetadata(
                    0.0,
                    ValuePropertyChanged,
                    ValueCoerceValue),
                DoubleValidateValue);

        /// <summary>
        /// Callback called when the Minimum property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void MinimumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.InvalidateProperty(RateProperty);
            d.InvalidateProperty(ValueProperty);
        }

        /// <summary>
        /// Callback called when the Maximum property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void MaximumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.InvalidateProperty(RateProperty);
            d.InvalidateProperty(ValueProperty);
        }

        /// <summary>
        /// Callback called when the Value property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.InvalidateProperty(RateProperty);
        }

        private static object MinimumCoerceValue(DependencyObject d, object value)
        {
            Object o = value;
            double maximum = ((RangeBase3D)d).Maximum;
            if (((double)value) > maximum)
            {
                o = maximum;
            }
            return o;
        }

        private static object MaximumCoerceValue(DependencyObject d, object value)
        {
            Object o = value;
            double minimum = ((RangeBase3D)d).Minimum;
            if (((double)value) < minimum)
            {
                o = minimum;
            }
            return o;
            // return ValueCoerceValue(d, value);
        }

        private static object ValueCoerceValue(DependencyObject d, object value)
        {
            RangeBase3D rb = ((RangeBase3D)d);
            double minimum = rb.Minimum;
            double maximum = rb.Maximum;
            if (((double)value) < minimum)
            {
                return minimum;
            }
            else if (((double)value) > maximum)
            {
                return maximum;
            }
            return value;
        }

        /// <summary>
        /// Gets the current magnitude rate of the range control. 
        /// This is a dependency property. 
        /// </summary>
        public double Rate
        {
            get { return (double)GetValue(RateProperty); }
        }

        /// <summary>
        /// Key of the Rate dependency property.
        /// </summary>
        protected static readonly DependencyPropertyKey RatePropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Rate", 
                typeof(double), 
                typeof(RangeBase3D), 
                new UIPropertyMetadata(
                    0.0,
                    null,
                    RateCoerceValue));

        /// <summary>
        /// Identifies the Rate dependency property.
        /// </summary>
        public static readonly DependencyProperty RateProperty = RatePropertyKey.DependencyProperty;

        private static object RateCoerceValue(DependencyObject d, object value)
        {
            RangeBase3D rb = (RangeBase3D)d;
            double rate = (rb.Value - rb.Minimum) / (rb.Maximum - rb.Minimum);
            return rate;
        }

        private static bool DoubleValidateValue(object value)
        {
            double d = (double)value;
            if (!double.IsNaN(d))
            {
                return !double.IsInfinity(d);
            }
            return false;
        }

        #region Mouse drag handling

        private Point _dragOriginPoint;
        // private Point _dragOriginScreenPoint;
        Viewport3D _viewport;
        double _yOrigin;

        /// <summary>
        /// Invoked when an unhandled MouseLeftButtonDown routed event is raised on this element.
        /// </summary>
        /// <param name="e">The MouseButtonEventArgs that contains the event data.</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Keyboard.Focus(this);
            // if (!System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                if (!((Keyboard.Modifiers & ModifierKeys.Control) > 0) && (!this.IsDragging))
                {
                    // Memorizations must occur before CaptureAndHideMouse() call
                    // GetCursorPos(out _dragOriginScreenPoint);
                    
                    // Mouse.GetPosition(
                    // _dragOriginScreenPoint = _viewport.PointToScreen(point);

                    _viewport = Helper3D.GetViewport3D(this);

                    _dragOriginPoint = e.GetPosition(this);

                    // CaptureAndHideMouse Moves the mouse
                    CaptureAndHideMouse();

                    base.SetValue(IsDraggingPropertyKey, true);
                    base.SetValue(IsSelectedPropertyKey, true);
                }
            }
            base.OnMouseLeftButtonDown(e);
        }

        /// <summary>
        /// Invoked when an unhandled MouseMove attached event reaches an element in its route that is derived from this class.
        /// </summary>
        /// <param name="e">The MouseEventArgs that contains the event data. </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.IsDragging)
            {
                if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
                {
                    UpdateValue(e.GetPosition(this));
                }
                else
                {
                    if (e.MouseDevice.Captured == this)
                    {
                        ReleaseAndShowMouse();
                    }
                    base.ClearValue(IsDraggingPropertyKey);
                }
            }
        }

        /// <summary>
        /// Invoked when an unhandled MouseLeftButtonUp attached event reaches an element in its route that is derived from this class.
        /// </summary>
        /// <param name="e">The MouseButtonEventArgs that contains the event data. </param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) > 0)
            {
                base.SetValue(IsSelectedPropertyKey, !this.IsSelected);
                _selectedByCtrl = this.IsSelected;
            }
            if (base.IsMouseCaptured && this.IsDragging)
            {
                e.Handled = true;
                base.ClearValue(IsDraggingPropertyKey);
                ReleaseAndShowMouse();
                UpdateValue(e.GetPosition(this));
            }
            base.OnMouseLeftButtonUp(e);
        }

        private void UpdateValue(Point position)
        {
            double rate = (_yOrigin - position.Y) / DragHeight;
            ApplyRate(rate);
        }

        /// <summary>
        /// Captures and hides the mouse.
        /// </summary>
        protected void CaptureAndHideMouse()
        {
            base.CaptureMouse();
            Mouse.OverrideCursor = Cursors.None;

            if (!System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                FullTrust_SetInitialMousePosition();
                _yOrigin = _dragOriginPoint.Y;
            }
            else
            {
                _yOrigin = _dragOriginPoint.Y + (DragHeight * Rate);
            }
        }

        /// <summary>
        /// Sets the initial mouse position.
        /// Must be called only in a full-trust context.
        /// </summary>
        private void FullTrust_SetInitialMousePosition()
        {
            // code specific to 3D
            // Viewport3D viewport = Helper3D.GetViewport3D(this);

            // GeneralTransform3DTo2D transform = this.TransformToAncestor(ancestor);
            // Point pScreen = transform.Transform(<3d point from the mesh in myVisual3D space>);

            Point p = new Point(
                _dragOriginPoint.X,
                _dragOriginPoint.Y - (DragHeight * Rate));
            // Point pScreen = viewport.PointToScreen(point);
            Point pScreen = _viewport.PointToScreen(p);
            SetCursorPos(
                Convert.ToInt32(pScreen.X),
                Convert.ToInt32(pScreen.Y));
        }

        /// <summary>
        /// Releases the mouse and shows it.
        /// </summary>
        protected void ReleaseAndShowMouse()
        {
            // SetCursorPos(_dragOriginScreenPoint.X, _dragOriginScreenPoint.Y);
            
            if (!System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                FullTrust_SetFinalMousePosition();
            }

            //if (!System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            //{
            //    Point pScreen = _viewport.PointToScreen(_dragOriginPoint);
            //    SetCursorPos(
            //        Convert.ToInt32(pScreen.X),
            //        Convert.ToInt32(pScreen.Y));
            //}

            Mouse.OverrideCursor = null; // restore the initial mouse cursor
            base.ReleaseMouseCapture();
        }

        /// <summary>
        /// Sets the final mouse position.
        /// Must be called only in a full-trust context.
        /// </summary>
        private void FullTrust_SetFinalMousePosition()
        {
            Point pScreen = _viewport.PointToScreen(_dragOriginPoint);
            SetCursorPos(
                Convert.ToInt32(pScreen.X),
                Convert.ToInt32(pScreen.Y));
        }

        /// <summary>
        /// Gets or sets the mouse drag height (in physical pixels) used in calculations.
        /// Default value is 100.0.
        /// </summary>
        public double DragHeight
        {
            get { return (double)GetValue(DragHeightProperty); }
            set { SetValue(DragHeightProperty, value); }
        }

        /// <summary>
        /// Identifies the DragHeight dependency property.
        /// </summary>
        public static readonly DependencyProperty DragHeightProperty =
            DependencyProperty.Register(
                "DragHeight",
                typeof(double),
                typeof(RangeBase3D),
                new PropertyMetadata(100.0));

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private struct POINT
        {
            public int X;
            public int Y;
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        private bool _selectedByCtrl;

        /// <summary>
        /// Invoked when an unhandled LostKeyboardFocus attached event reaches an element in its route that is derived from this class.
        /// </summary>
        /// <param name="e">The KeyboardFocusChangedEventArgs that contains event data.</param>
        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (!_selectedByCtrl)
            {
                // if the selection has occured by simple focus
                base.ClearValue(IsSelectedPropertyKey);
            }
        }

        /// <summary>
        /// Indicates if the range control is selected.
        /// This is a read-only dependency property.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
        }

        /// <summary>
        /// Key of the IsSelected dependency property.
        /// </summary>
        protected static readonly DependencyPropertyKey IsSelectedPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsSelected",
                typeof(bool),
                typeof(RangeBase3D),
                new PropertyMetadata(
                    false,
                    IsSelectedChanged));

        /// <summary>
        /// Identifies the IsSelected dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = IsSelectedPropertyKey.DependencyProperty;

        private UIElement3D _selectionIndicator;

        private void AdjustSelectionIndicatorVisibility()
        {
            if (_selectionIndicator != null)
            {
                _selectionIndicator.Visibility = IsSelected ? Visibility.Visible : Visibility.Hidden;
            }
        }

        /// <summary>
        /// Callback called when the IsSelected property's value has changed.
        /// </summary>
        /// <param name="d">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        public static void IsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBase3D rb = (RangeBase3D)d;
            rb.AdjustSelectionIndicatorVisibility();

            RangeElementSelectionManager.IsSelectedPropertyChanged(d, e);
        }

        private void ApplyRate(double rate)
        {
            double previousRate = Rate;
            if (IsKeyboardFocused)
            {
                Value = Minimum + ((Maximum - Minimum) * rate);
            }
            RangeElementSelectionManager.Current.ApplyRate(rate - previousRate);
        }

        #endregion


        #region Mouse wheel handling

        internal const double DefaultMouseWheelIncrement = 0.025;

        /// <summary>
        /// Gets or sets the mouse wheel increment.
        /// Default value is 0.025.
        /// </summary>
        public double MouseWheelIncrement
        {
            get { return (double)GetValue(MouseWheelIncrementProperty); }
            set { SetValue(MouseWheelIncrementProperty, value); }
        }

        /// <summary>
        /// Identifies the MouseWheelIncrement dependency property.
        /// </summary>
        public static readonly DependencyProperty MouseWheelIncrementProperty =
            DependencyProperty.Register(
                "MouseWheelIncrement",
                typeof(double),
                typeof(RangeBase3D),
                new PropertyMetadata(
                    DefaultMouseWheelIncrement));

        /// <summary>
        /// Invoked when an unhandled MouseWheel attached event reaches the element in its route 
        /// </summary>
        /// <param name="e">The MouseWheelEventArgs that contains event data.</param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(e.Delta);
            ApplyRate(e.Delta > 0.0 ?
                Rate + MouseWheelIncrement :
                Rate - MouseWheelIncrement);
        }
        #endregion
    }
}
