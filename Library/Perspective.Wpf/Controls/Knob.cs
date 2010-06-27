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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Runtime.InteropServices;
using Perspective.Core.Primitives;

namespace Perspective.Wpf.Controls
{
    /// <summary>
    /// A knob (rotative button) class, compatible with range element multiselection.
    /// <see cref="RangeElementSelectionManager"/>
    /// It supports Mouse wheel.
    /// </summary>
    public class Knob : RangeBase, IRangeElement
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static Knob()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Knob),
                new FrameworkPropertyMetadata(typeof(Knob))); // FrameworkPropertyMetadata is required

            ValueProperty.OverrideMetadata(
                typeof(Knob),
                new FrameworkPropertyMetadata(
                    ValuePropertyChanged));

            MinimumProperty.OverrideMetadata(
                typeof(Knob),
                new FrameworkPropertyMetadata(
                    MinimumPropertyChanged));

            MaximumProperty.OverrideMetadata(
                typeof(Knob),
                new FrameworkPropertyMetadata(
                    MaximumPropertyChanged));
        }

        /// <summary>
        /// Callback called when the Value property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.InvalidateProperty(AngleProperty);
        }

        /// <summary>
        /// Callback called when the Minimum property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void MinimumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.InvalidateProperty(AngleProperty);
        }

        /// <summary>
        /// Callback called when the Maximum property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void MaximumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.InvalidateProperty(AngleProperty);
        }

        /// <summary>
        /// Gets the angle from the bottom of the knob circumference (clockwise).
        /// </summary>
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
        }

        private static readonly DependencyPropertyKey AnglePropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Angle",
                typeof(double),
                typeof(Knob),
                new PropertyMetadata(
                    30.0,
                    null, 
                    AngleCoerceValue));
        
        /// <summary>
        /// Identifies the Angle dependency property.
        /// </summary>
        public static readonly DependencyProperty AngleProperty = AnglePropertyKey.DependencyProperty;

        private static object AngleCoerceValue(DependencyObject d, object value)
        {
            Knob k = ((Knob)d);
            double walkAngle = k.WalkAngle;
            return k.CurrentRate * walkAngle + ((360.0 - walkAngle) / 2.0);
        }

        /// <summary>
        /// Gets or sets the walkangle of the knob.
        /// (in french : Angle de débattement)
        /// </summary>
        public double WalkAngle
        {
            get { return (double)GetValue(WalkAngleProperty); }
            set { SetValue(WalkAngleProperty, value); }
        }

        /// <summary>
        /// Identifies the WalkAngle dependency property.
        /// </summary>
        public static readonly DependencyProperty WalkAngleProperty =
            DependencyProperty.Register(
                "WalkAngle", 
                typeof(double), 
                typeof(Knob),
                new PropertyMetadata(
                    300.0, 
                    WalkAnglePropertyChanged,
                    WalkAngleCoerceValue),
                WalkAngleValidateValue);

        private static void WalkAnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Knob k = (Knob)d;
            k.InvalidateProperty(Knob.AngleProperty);
        }

        private static object WalkAngleCoerceValue(DependencyObject d, object value)
        {
            double walkAngle = (double)value;
            //if (walkAngle < 0.0)
            //{
            //    walkAngle = 0.0;
            //}
            //if (walkAngle > 360.0)
            //{
            //    walkAngle = 360.0;
            //}
            Knob k = ((Knob)d);
            if ((k.Angle * 2 + walkAngle) > 360.0)
            {
                walkAngle = 360.0 - k.Angle * 2;
            }
            return walkAngle;
        }

        /// <summary>
        /// Validation of the WalkAngle value.
        /// </summary>
        /// <param name="value">Value to test.</param>
        /// <returns>Boolean value.</returns>
        private static bool WalkAngleValidateValue(object value)
        {
            double d = (double)value;
            return (d > 0.0) && (d < 360.0) && 
                (!double.IsNaN(d)) && (!double.IsInfinity(d));
        }

        /// <summary>
        /// Indicates if the knob is selected.
        /// This is a read-only dependency property.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
        }

        private static readonly DependencyPropertyKey IsSelectedPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsSelected", 
                typeof(bool), 
                typeof(Knob), 
                new PropertyMetadata(
                    false,
                    RangeElementSelectionManager.IsSelectedPropertyChanged));

        /// <summary>
        /// Identifies the IsSelected dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = IsSelectedPropertyKey.DependencyProperty;

        #region Mouse drag handling

        /// <summary>
        /// Indicates if a drag and drop operation occurs on the knob.
        /// This is a read-only dependency property.
        /// </summary>
        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }

        private static readonly DependencyPropertyKey IsDraggingPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsDragging",
                typeof(bool),
                typeof(Knob),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the IsDragging dependency property.
        /// </summary>
        public static readonly DependencyProperty IsDraggingProperty = IsDraggingPropertyKey.DependencyProperty;

        private Point _dragOriginPoint;
        double _yOrigin;

        /// <summary>
        /// Invoked when an unhandled MouseLeftButtonDown routed event is raised on this element.
        /// </summary>
        /// <param name="e">The MouseButtonEventArgs that contains the event data.</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.Focus();
            if (!((Keyboard.Modifiers & ModifierKeys.Control) > 0) && (!this.IsDragging))
            {
                // Memorizations must occur before CaptureAndHideMouse() call
                // GetCursorPos(out _dragOriginScreenPoint);
                _dragOriginPoint = e.GetPosition(this);

                // CaptureAndHideMouse Moves the mouse
                CaptureAndHideMouse();

                base.SetValue(IsDraggingPropertyKey, true);
                base.SetValue(IsSelectedPropertyKey, true);
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

        private double CurrentRate
        {
            get
            {
                return (Value - Minimum) / (Maximum - Minimum);
            }
        }

        private void CaptureAndHideMouse()
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
                _yOrigin = _dragOriginPoint.Y + (DragHeight * CurrentRate);
            }

            //Point point = new Point(
            //    _dragOriginPoint.X,
            //    _dragOriginPoint.Y - (DragHeight * CurrentRate));
            //Point pScreen = this.PointToScreen(point);
            //SetCursorPos(
            //    Convert.ToInt32(pScreen.X),
            //    Convert.ToInt32(pScreen.Y));
        }

        /// <summary>
        /// Sets the initial mouse position.
        /// Must be called only in a full-trust context.
        /// </summary>
        private void FullTrust_SetInitialMousePosition()
        {
            Point p = new Point(
                _dragOriginPoint.X,
                _dragOriginPoint.Y - (DragHeight * CurrentRate));
            Point pScreen = this.PointToScreen(p);
            SetCursorPos(
                Convert.ToInt32(pScreen.X),
                Convert.ToInt32(pScreen.Y));
        }

        private void ReleaseAndShowMouse()
        {
            if (!System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                FullTrust_SetFinalMousePosition();
            }
            // SetCursorPos(_dragOriginScreenPoint.X, _dragOriginScreenPoint.Y);
            Mouse.OverrideCursor = null; // restore the initial mouse cursor
            base.ReleaseMouseCapture();
        }

        /// <summary>
        /// Sets the final mouse position.
        /// Must be called only in a full-trust context.
        /// </summary>
        private void FullTrust_SetFinalMousePosition()
        {
            Point pScreen = this.PointToScreen(_dragOriginPoint);
            SetCursorPos(
                Convert.ToInt32(pScreen.X),
                Convert.ToInt32(pScreen.Y));
        }

        private bool _selectedByCtrl;

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

        /// <summary>
        /// Invoked when an unhandled LostKeyboardFocus attached event reaches an element in its route that is derived from this class.
        /// </summary>
        /// <param name="e">The KeyboardFocusChangedEventArgs that contains event data.</param>
        protected override void  OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (!_selectedByCtrl)
            {
                // if the selection has occured by simple focus
                base.ClearValue(IsSelectedPropertyKey);
            }
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
                typeof(Knob), 
                new PropertyMetadata(100.0));

        private void UpdateValue(Point position)
        {
            // double rate = (_dragOriginPoint.Y - position.Y) / DragHeight;
            double rate = (_yOrigin - position.Y) / DragHeight;
            ApplyRate(rate);
        }

        private void ApplyRate(double rate)
        {
            double previousRate = CurrentRate;
            if (IsKeyboardFocused)
            {
                Value = Minimum + ((Maximum - Minimum) * rate);
            }
            RangeElementSelectionManager.Current.ApplyRate(rate - previousRate);
        }

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private struct POINT
        {
            public int X;
            public int Y;
        }
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

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
                typeof(Knob),
                new PropertyMetadata(
                    Knob.DefaultMouseWheelIncrement));

        /// <summary>
        /// Invoked when an unhandled MouseWheel attached event reaches the element in its route 
        /// </summary>
        /// <param name="e">The MouseWheelEventArgs that contains event data.</param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            ApplyRate(e.Delta > 0.0 ?
                CurrentRate + MouseWheelIncrement :
                CurrentRate - MouseWheelIncrement);
        }
        #endregion
    }
}
