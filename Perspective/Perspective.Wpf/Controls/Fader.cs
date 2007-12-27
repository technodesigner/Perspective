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
using System.Windows.Input;
using Perspective.Core.Primitives;

namespace Perspective.Wpf.Controls
{
    /// <summary>
    /// A slider class compatible with range element multiselection.
    /// <see cref="RangeElementSelectionManager"/>
    /// It supports Mouse wheel.
    /// </summary>
    public class Fader : System.Windows.Controls.Slider, IRangeElement
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static Fader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Fader),
                new FrameworkPropertyMetadata(typeof(Fader)));

            ValueProperty.OverrideMetadata(
                typeof(Fader),
                new FrameworkPropertyMetadata(
                    0.0,
                    OnValueChanged));
        }
        
        /// <summary>
        /// Callback called when the Value property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Fader s = (Fader)d;
            if (s.IsKeyboardFocused || s.IsKeyboardFocusWithin)
            {
                double previousRate = s.GetRate((double)e.OldValue);
                double rate = s.GetRate(s.Value);
                double effectiveRate = rate - previousRate;
                RangeElementSelectionManager.Current.ApplyRate(effectiveRate);
            }
        }

        /// <summary>
        /// Return the rate (between Minimum and Maximum) of a given value.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns>The current value rate.</returns>
        private double GetRate(double value)
        {
            return (value - Minimum) / (Maximum - Minimum);
        }

        #region Selection handling

        /// <summary>
        /// Indicates if the slider is selected.
        /// This is a read-only property.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
        }

        private static readonly DependencyPropertyKey IsSelectedPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsSelected",
                typeof(bool),
                typeof(Fader),
                new PropertyMetadata(
                    false,
                    RangeElementSelectionManager.IsSelectedChanged));

        /// <summary>
        /// Identifies the IsSelected dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = IsSelectedPropertyKey.DependencyProperty;

        /// <summary>
        /// Invoked when an unhandled PreviewMouseLeftButtonDown routed event reaches an element in its route that is derived from this class. 
        /// </summary>
        /// <param name="e">The MouseButtonEventArgs that contains the event data.</param>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Focus();
            if ((Keyboard.Modifiers & ModifierKeys.Control) > 0)
            {
                base.SetValue(IsSelectedPropertyKey, !this.IsSelected);
                _selectedByCtrl = this.IsSelected;
            }
            else
            {
                base.SetValue(IsSelectedPropertyKey, true);
            }
            base.OnPreviewMouseLeftButtonDown(e);
        }

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

        #endregion

        #region Mouse wheel handling
        private const double _mouseWheelIncrement = 0.025;

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
            Knob.MouseWheelIncrementProperty.AddOwner(
            typeof(Fader),
            new PropertyMetadata(
                Knob.DefaultMouseWheelIncrement));

        /// <summary>
        /// Invoked when an unhandled MouseWheel attached event reaches the element in its route 
        /// </summary>
        /// <param name="e">The MouseWheelEventArgs that contains event data.</param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            double rate = e.Delta > 0.0 ?
                GetRate(Value) + _mouseWheelIncrement :
                GetRate(Value) - _mouseWheelIncrement;
            Value = Minimum + ((Maximum - Minimum) * rate);
        }
        #endregion

    }
}
