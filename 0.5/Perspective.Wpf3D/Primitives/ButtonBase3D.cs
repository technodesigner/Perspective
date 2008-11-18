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
using System.Windows.Media.Animation;

namespace Perspective.Wpf3D.Primitives
{
    /// <summary>
    /// A 3D push button base class.
    /// </summary>
    [Perspective.Core.SkinPart(Name="PART_Button", Type=typeof(ButtonBase3D))]
    public abstract class ButtonBase3D : ContentControl3D
    {

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ButtonBase3D()
        {
            ContentProperty.OverrideMetadata(
                typeof(ButtonBase3D),
                new UIPropertyMetadata(OnContentChanged));
        }

        private UIElement3D _button;

        /// <summary>
        /// Callback called when the Content property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ButtonBase3D bb = (ButtonBase3D)d;
            bb._button = (UIElement3D)bb.FindName("PART_Button");
            bb.InitializeButton();
        }

        private void InitializeButton()
        {
            if (_button != null)
            {
                _button.MouseLeftButtonDown += button_MouseLeftButtonDown;
                _button.MouseLeftButtonUp += button_MouseLeftButtonUp;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the control is currently activated. 
        /// This is a dependency property.
        /// </summary>
        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
        }

        /// <summary>
        /// Key of the IsPressed dependency property.
        /// </summary>
        protected static readonly DependencyPropertyKey IsPressedPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsPressed", 
                typeof(bool), 
                typeof(ButtonBase3D), 
                new UIPropertyMetadata(
                    false,
                    new PropertyChangedCallback(OnIsPressedChanged)));

        /// <summary>
        /// Identifies the IsPressed dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPressedProperty = IsPressedPropertyKey.DependencyProperty;

        /// <summary>
        /// Callback called when the IsPressed property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void OnIsPressedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ButtonBase3D bb = (ButtonBase3D)d;
            DoubleAnimation da = new DoubleAnimation();
            if (bb.IsPressed)
            {
                da.From = 0.0;
                da.To = -1.0;
            }
            else
            {
                da.From = -1.0;
                da.To = 0.0;
            }
            da.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            bb.ApplyAnimationClock(PressEffectProperty, null);
            AnimationClock ac = da.CreateClock();
            bb.ApplyAnimationClock(PressEffectProperty, ac);
        }

        /// <summary>
        /// Occurs when a Button is clicked.
        /// </summary>
        public event RoutedEventHandler Click
        {
            add
            {
                base.AddHandler(ClickEvent, value);
            }
            remove
            {
                base.RemoveHandler(ClickEvent, value);
            }
        }

        /// <summary>
        /// Identifies the Click routed event.
        /// </summary>
        public static readonly RoutedEvent ClickEvent = 
            EventManager.RegisterRoutedEvent(
                "Click", 
                RoutingStrategy.Bubble, 
                typeof(RoutedEventHandler), 
                typeof(ButtonBase3D));

        /// <summary>
        /// Raises the Click routed event.
        /// </summary>
        protected virtual void OnClick()
        {
            RoutedEventArgs e = new RoutedEventArgs(ClickEvent, this);
            base.RaiseEvent(e);
        }

        private void button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Keyboard.Focus(_button);
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                _button.CaptureMouse();
                if (_button.IsMouseCaptured)
                {
                    if (e.ButtonState == MouseButtonState.Pressed)
                    {
                        SetIsPressed(!IsPressed);
                    }
                    else
                    {
                        _button.ReleaseMouseCapture();
                    }
                }
            }
        }

        private void button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (_button.IsMouseCaptured)
            {
                _button.ReleaseMouseCapture();
            }
            //if (Mode == ButtonMode.Push)
            {
                SetIsPressed(false);
            }
            this.OnClick();
        }

        private void SetIsPressed(bool pressed)
        {
            if (pressed)
            {
                base.SetValue(IsPressedPropertyKey, pressed);
            }
            else
            {
                base.ClearValue(IsPressedPropertyKey);
            }
        }

        /// <summary>
        /// Gets or sets a value which is automatically animated when the button is pressed or released, and which can be used in a skin to make a visual effect. 
        /// This is a dependency property.
        /// </summary>
        public double PressEffect
        {
            get { return (double)GetValue(PressEffectProperty); }
            set { SetValue(PressEffectProperty, value); }
        }
        
        /// <summary>
        /// Identifies the PressEffect dependency property.
        /// </summary>        
        public static readonly DependencyProperty PressEffectProperty =
            DependencyProperty.Register(
                "PressEffect", 
                typeof(double), 
                typeof(ButtonBase3D), 
                new UIPropertyMetadata(0.0));

        ///// <summary>
        ///// Gets a value that indicates the mode of the button control. 
        ///// This is a dependency property.
        ///// </summary>
        //public ButtonMode Mode
        //{
        //    get { return (ButtonMode)GetValue(ModeProperty); }
        //    set { SetValue(ModeProperty, value); }
        //}

        ///// <summary>
        ///// Identifies the Mode dependency property.
        ///// </summary>        
        //public static readonly DependencyProperty ModeProperty =
        //    DependencyProperty.Register("Mode", typeof(ButtonMode), typeof(ButtonBase3D), new UIPropertyMetadata(ButtonMode.Push));
    }

    ///// <summary>
    ///// An enum type to handle button's mode.
    ///// </summary>
    //public enum ButtonMode
    //{
    //    /// <summary>
    //    /// The control acts as a push button.
    //    /// </summary>
    //    Push,
    //    /// <summary>
    //    /// The control acts as a switch button.
    //    /// </summary>
    //    Switch
    //}
}
