using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Perspective.Core;
using Perspective.Core.Wpf;

namespace Perspective.Wpf.Controls
{
    /// <summary>
    /// Represents the method that will handle the Starup event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A RoutedEventArgs that contains the event data.</param>
    public delegate void StartupEventHandler(object sender, RoutedEventArgs<Direction> e);

    /// <summary>
    /// A joystick control. The number of buttons is relevant to the Dimensions property, which indicates the number of dimensions (1D, 2D or 3D).
    /// </summary>
    [TemplatePartAttribute(Name = Joystick.GoForwardElementName, Type = typeof(UIElement))]
    [TemplatePartAttribute(Name = Joystick.GoForwardLeftElementName, Type = typeof(UIElement))]
    [TemplatePartAttribute(Name = Joystick.GoForwardRightElementName, Type = typeof(UIElement))]
    [TemplatePartAttribute(Name = Joystick.GoBackElementName, Type = typeof(UIElement))]
    [TemplatePartAttribute(Name = Joystick.GoBackLeftElementName, Type = typeof(UIElement))]
    [TemplatePartAttribute(Name = Joystick.GoBackRightElementName, Type = typeof(UIElement))]
    [TemplatePartAttribute(Name = Joystick.GoLeftElementName, Type = typeof(UIElement))]
    [TemplatePartAttribute(Name = Joystick.GoRightElementName, Type = typeof(UIElement))]
    [TemplatePartAttribute(Name = Joystick.GoUpElementName, Type = typeof(UIElement))]
    [TemplatePartAttribute(Name = Joystick.GoDownElementName, Type = typeof(UIElement))]
    public class Joystick : Control
    {
        /// <summary>
        /// Template name of the GoForward element.
        /// </summary>
        public const string GoForwardElementName = "PART_GoForward";
        
        /// <summary>
        /// Template name of the GoForwardLeft element.
        /// </summary>
        public const string GoForwardLeftElementName = "PART_GoForwardLeft";

        /// <summary>
        /// Template name of the GoForwardRight element.
        /// </summary>
        public const string GoForwardRightElementName = "PART_GoForwardRight";

        /// <summary>
        /// Template name of the GoBack element.
        /// </summary>
        public const string GoBackElementName = "PART_GoBack";

        /// <summary>
        /// Template name of the GoBackLeft element.
        /// </summary>
        public const string GoBackLeftElementName = "PART_GoBackLeft";

        /// <summary>
        /// Template name of the GoBackRight element.
        /// </summary>
        public const string GoBackRightElementName = "PART_GoBackRight";

        /// <summary>
        /// Template name of the GoLeft element.
        /// </summary>
        public const string GoLeftElementName = "PART_GoLeft";

        /// <summary>
        /// Template name of the GoRight element.
        /// </summary>
        public const string GoRightElementName = "PART_GoRight";

        /// <summary>
        /// Template name of the GoUp element.
        /// </summary>
        public const string GoUpElementName = "PART_GoUp";

        /// <summary>
        /// Template name of the GoDown element.
        /// </summary>
        public const string GoDownElementName = "PART_GoDown";

        private UIElement _goForwardElement;
        private UIElement _goForwardLeftElement;
        private UIElement _goForwardRightElement;
        private UIElement _goBackElement;
        private UIElement _goBackLeftElement;
        private UIElement _goBackRightElement;
        private UIElement _goLeftElement;
        private UIElement _goRightElement;
        private UIElement _goUpElement;
        private UIElement _goDownElement;
        private bool _hasStarted;

        /// <summary>
        /// Static constructor.
        /// </summary>
        static Joystick()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Joystick),
                new FrameworkPropertyMetadata(typeof(Joystick)));
        }

        /// <summary>
        /// Initializes a new instance of Joystick.
        /// </summary>
        public Joystick()
        {
        }

        /// <summary>
        /// Invoked when ApplyTemplate() is called.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ApplyJoystickTemplate();
        }

        private void ApplyJoystickTemplate()
        {
            _goForwardElement = base.GetTemplateChild(Joystick.GoForwardElementName) as UIElement;
            if (_goForwardElement != null)
            {
                _goForwardElement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_goForwardElement_PreviewMouseLeftButtonDown);
                _goForwardElement.PreviewMouseLeftButtonUp += _goElement_PreviewMouseLeftButtonUp;
            }

            _goBackElement = base.GetTemplateChild(Joystick.GoBackElementName) as UIElement;
            if (_goBackElement != null)
            {
                _goBackElement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_goBackElement_PreviewMouseLeftButtonDown);
                _goBackElement.PreviewMouseLeftButtonUp += _goElement_PreviewMouseLeftButtonUp;
            }

            if ((this.Dimensions == Dimensions.Two) || (this.Dimensions == Dimensions.Three))
            {
                _goForwardLeftElement = base.GetTemplateChild(Joystick.GoForwardLeftElementName) as UIElement;
                if (_goForwardLeftElement != null)
                {
                    _goForwardLeftElement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_goForwardLeftElement_PreviewMouseLeftButtonDown);
                    _goForwardLeftElement.PreviewMouseLeftButtonUp += _goElement_PreviewMouseLeftButtonUp;
                }

                _goForwardRightElement = base.GetTemplateChild(Joystick.GoForwardRightElementName) as UIElement;
                if (_goForwardRightElement != null)
                {
                    _goForwardRightElement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_goForwardRightElement_PreviewMouseLeftButtonDown);
                    _goForwardRightElement.PreviewMouseLeftButtonUp += _goElement_PreviewMouseLeftButtonUp;
                }

                _goBackLeftElement = base.GetTemplateChild(Joystick.GoBackLeftElementName) as UIElement;
                if (_goBackLeftElement != null)
                {
                    _goBackLeftElement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_goBackLeftElement_PreviewMouseLeftButtonDown);
                    _goBackLeftElement.PreviewMouseLeftButtonUp += _goElement_PreviewMouseLeftButtonUp;
                }

                _goBackRightElement = base.GetTemplateChild(Joystick.GoBackRightElementName) as UIElement;
                if (_goBackRightElement != null)
                {
                    _goBackRightElement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_goBackRightElement_PreviewMouseLeftButtonDown);
                    _goBackRightElement.PreviewMouseLeftButtonUp += _goElement_PreviewMouseLeftButtonUp;
                }

                _goLeftElement = base.GetTemplateChild(Joystick.GoLeftElementName) as UIElement;
                if (_goLeftElement != null)
                {
                    _goLeftElement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_goLeftElement_PreviewMouseLeftButtonDown);
                    _goLeftElement.PreviewMouseLeftButtonUp += _goElement_PreviewMouseLeftButtonUp;
                }

                _goRightElement = base.GetTemplateChild(Joystick.GoRightElementName) as UIElement;
                if (_goRightElement != null)
                {
                    _goRightElement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_goRightElement_PreviewMouseLeftButtonDown);
                    _goRightElement.PreviewMouseLeftButtonUp += _goElement_PreviewMouseLeftButtonUp;
                }
            }

            if (this.Dimensions == Dimensions.Three)
            {
                _goUpElement = base.GetTemplateChild(Joystick.GoUpElementName) as UIElement;
                if (_goUpElement != null)
                {
                    _goUpElement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_goUpElement_PreviewMouseLeftButtonDown);
                    _goUpElement.PreviewMouseLeftButtonUp += _goElement_PreviewMouseLeftButtonUp;
                }

                _goDownElement = base.GetTemplateChild(Joystick.GoDownElementName) as UIElement;
                if (_goDownElement != null)
                {
                    _goDownElement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_goDownElement_PreviewMouseLeftButtonDown);
                    _goDownElement.PreviewMouseLeftButtonUp += _goElement_PreviewMouseLeftButtonUp;
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of dimensions.
        /// </summary>
        public Dimensions Dimensions
        {
            get { return (Dimensions)GetValue(DimensionsProperty); }
            set { SetValue(DimensionsProperty, value); }
        }

        /// <summary>
        /// Identifies the Dimensions dependency property.
        /// </summary>
        public static readonly DependencyProperty DimensionsProperty =
            DependencyProperty.Register(
                "Dimensions", 
                typeof(Dimensions), 
                typeof(Joystick), 
                new UIPropertyMetadata(
                    Dimensions.Three,
                    DimensionsPropertyChanged));

        private static void DimensionsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Joystick).ApplyJoystickTemplate();
        }

        /// <summary>
        /// Identifies the Startup routed event.
        /// </summary>
        public static readonly RoutedEvent StartupEvent =
            EventManager.RegisterRoutedEvent(
                "Startup",
                RoutingStrategy.Bubble,
                typeof(StartupEventHandler),
                typeof(Joystick));

        /// <summary>
        /// Occurs when a button is pushed.
        /// </summary>
        public event StartupEventHandler Startup
        {
            add { AddHandler(StartupEvent, value); }
            remove { RemoveHandler(StartupEvent, value); }
        }

        /// <summary>
        /// Raises the Startup event.
        /// </summary>
        /// <param name="direction">Direction enum value.</param>
        protected void RaiseStartupEvent(Direction direction)
        {
            _hasStarted = true;
            RoutedEventArgs<Direction> rea = new RoutedEventArgs<Direction>(StartupEvent, direction);
            RaiseEvent(rea);
        }

        /// <summary>
        /// Identifies the Stop routed event.
        /// </summary>
        public static readonly RoutedEvent StopEvent =
            EventManager.RegisterRoutedEvent(
                "Stop",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(Joystick));

        /// <summary>
        /// Occurs when a button is released.
        /// </summary>
        public event RoutedEventHandler Stop
        {
            add { AddHandler(StopEvent, value); }
            remove { RemoveHandler(StopEvent, value); }
        }

        /// <summary>
        /// Raises the Stop event.
        /// </summary>
        protected void RaiseStopEvent()
        {
            RoutedEventArgs rea = new RoutedEventArgs(StopEvent);
            RaiseEvent(rea);
            _hasStarted = false;
        }
       
        /// <summary>
        /// Class handling for the PreviewKeyDown event.
        /// </summary>
        /// <param name="e">The KeyEventArgs that contains the event data.</param>
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            // The event is fired several times when a key is down (while not released)
            // but we want to raise the Startup event only once
            if (!_hasStarted)
            {
                if ((e.Key == Key.Up) || (e.Key == Key.NumPad8))
                {
                    RaiseStartupEvent(Direction.Forward);
                    e.Handled = true;
                }
                if (e.Key == Key.NumPad7)
                {
                    RaiseStartupEvent(Direction.Left | Direction.Forward);
                    e.Handled = true;
                }
                if ((e.Key == Key.Left) || (e.Key == Key.NumPad4))
                {
                    RaiseStartupEvent(Direction.Left);
                    e.Handled = true;
                }
                if (e.Key == Key.NumPad1)
                {
                    RaiseStartupEvent(Direction.Left | Direction.Backward);
                    e.Handled = true;
                }
                if ((e.Key == Key.Down) || (e.Key == Key.NumPad2))
                {
                    RaiseStartupEvent(Direction.Backward);
                    e.Handled = true;
                }
                if (e.Key == Key.NumPad3)
                {
                    RaiseStartupEvent(Direction.Right | Direction.Backward);
                    e.Handled = true;
                }
                if ((e.Key == Key.Right) || (e.Key == Key.NumPad6))
                {
                    RaiseStartupEvent(Direction.Right);
                    e.Handled = true;
                }
                if (e.Key == Key.NumPad9)
                {
                    RaiseStartupEvent(Direction.Right | Direction.Forward);
                    e.Handled = true;
                }
                if (e.Key == Key.PageUp)
                {
                    RaiseStartupEvent(Direction.Up);
                    e.Handled = true;
                }
                if (e.Key == Key.PageDown)
                {
                    RaiseStartupEvent(Direction.Down);
                    e.Handled = true;
                }
            }
            else
            {
                // This prevents changing the focus
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }

        /// <summary>
        /// Class handling for the PreviewKeyUp event.
        /// </summary>
        /// <param name="e">The KeyEventArgs that contains the event data.</param>
        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            if ((e.Key == Key.Up)
               || (e.Key == Key.Down)
                || (e.Key == Key.Left)
                || (e.Key == Key.Right)
                || (e.Key == Key.NumPad1)
                || (e.Key == Key.NumPad2)
                || (e.Key == Key.NumPad3)
                || (e.Key == Key.NumPad4)
                || (e.Key == Key.NumPad6)
                || (e.Key == Key.NumPad7)
                || (e.Key == Key.NumPad8)
                || (e.Key == Key.NumPad9)
                || (e.Key == Key.PageUp)
                || (e.Key == Key.PageDown)
                )
            {
                RaiseStopEvent();
                e.Handled = true;
            }
            base.OnPreviewKeyUp(e);
        }

        private void _goDownElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseStartupEvent(Direction.Down);
        }

        private void _goUpElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseStartupEvent(Direction.Up);
        }

        private void _goRightElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseStartupEvent(Direction.Right);
        }

        private void _goLeftElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseStartupEvent(Direction.Left);
        }

        private void _goBackRightElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseStartupEvent(Direction.Backward | Direction.Right);
        }

        private void _goBackLeftElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseStartupEvent(Direction.Backward | Direction.Left);
        }

        private void _goForwardRightElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseStartupEvent(Direction.Forward | Direction.Right);
        }

        private void _goForwardLeftElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseStartupEvent(Direction.Forward | Direction.Left);
        }

        private void _goBackElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseStartupEvent(Direction.Backward);
        }

        private void _goForwardElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseStartupEvent(Direction.Forward);
        }

        private void _goElement_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RaiseStopEvent();
        }
    }
}
