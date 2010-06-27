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
using System.Windows.Media.Media3D;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using Perspective.Core.Primitives;
using Perspective.Core.Wpf;
using System.Windows.Media;
using System.Windows.Input;
using Perspective.Wpf.ResourceStrings;
using Perspective.Core.Wpf.Converters;
using Perspective.Core.Wpf.Data;
using System.Windows.Media.Imaging;

namespace Perspective.Wpf.Controls
{
    /// <summary>
    /// A ready to use viewport for 3D models,
    /// with light, moveable camera and camera control panel.
    /// The camera can be controlled by mouse (with joysticks) or by keyboard (when the control is focused) :
    /// - Plus and Minus keys act on the zoom factor.
    /// - Numeric keypad arrow key act on the camera position (on a XZ plane)
    /// - When the Ctrl key is pressed, they move the orientation of the camera according to a vertical plane (Up and Down arrows) or horizontal plane (Left and Right arrows). 
    /// - When the Shift key is pressed, they turn the camera around the origin according to a vertical plane (Up and Down arrows or position joystick's buttons) or horizontal plane (Left and Right arrows or position joystick's buttons). 
    /// - The key 5 or Ctrl-Plus of the numeric keypad elevate the position of the camera. Keys Ctrl-5 or Ctrl-Minus reduce the height of the camera position.
    /// <remarks>Binding between elements using ElementName is not currently supported in a Workshop3D, because it is not possible to get an inheritance context pointer. Use the Source or RelativeSource property. See http://blogs.msdn.com/nickkramer/archive/2006/08/18/705116.aspx</remarks>
    /// </summary>
    [ContentProperty("Children")]
    public class Workshop3D : FrameworkElement
    {
        private readonly Viewport3D _viewport;
        private readonly StackPanel _commandPanel;
        private readonly Joystick _cameraLookDirectionJoystick;
        private readonly Joystick _cameraPositionJoystick;
        private readonly Joystick _cameraZoomJoystick;
        private Point3DAnimation _cameraPositionAnimation;
        private Vector3DAnimation _cameraLookDirectionAnimation;
        private DoubleAnimation _cameraZoomAnimation;
        private bool _keyPressed;
        //private bool _cameraTranslating;
        //private bool _cameraOrienting;
        //private bool _cameraZooming;
        private Point3D _initialCameraPosition = new Point3D(10.0, 3.0, 10.0);
        private Vector3D _initialCameraLookDirection = new Vector3D(-10.0, -1.0, -10.0);
        //private Vector3D _initialCameraLookDirection = new Vector3D(-10.0, -2.0, -10.0);
        private readonly double _initialCameraFieldOfView = 50.0;
        //private readonly double _initialCameraFieldOfView = 15.0;
        private readonly double _opacity = 0.5;
        private readonly ResourceStringDecorator _rsd;
        private readonly ModelVisual3D _defaultLightingModel = new ModelVisual3D();
        private AxisAngleRotation3D _cameraPositionRotation;
        private RotateTransform3D _cameraPositionRotateTransform;
        private AxisAngleRotation3D _cameraLookDirectionRotation;
        private RotateTransform3D _cameraLookDirectionRotateTransform;
        private Vector3D _yAxis = new Vector3D(0, 1, 0);
        private OrbitKind? _lastOk = null;

        private enum OrbitKind
        {
            Horizontal,
            Vertical
        }

        /// <summary>
        /// Initializes a new Workshop3D instance.
        /// </summary>
        public Workshop3D()
        {
            this.Focusable = true;

            _viewport = new Viewport3D();
            _viewport.Focusable = false;
            AddVisualChild(_viewport);

            PerspectiveCamera camera = new PerspectiveCamera();
            _viewport.Camera = camera;
            InitializeCamera();

            // INameScope ns = NameScope.GetNameScope(_viewport);
            // NameScope.SetNameScope(this, ns);

            // NameScope.SetNameScope(this, NameScope.GetNameScope(_viewport));
            // this.DataContext = _viewport.DataContext;

            DirectionalLight light1 = new DirectionalLight(Colors.White, new Vector3D(0.0, -1.0, 1.0));
            DirectionalLight light2 = new DirectionalLight(Colors.White, new Vector3D(-1.0, -1.0, -1.0));
            DirectionalLight light3 = new DirectionalLight(Colors.White, new Vector3D(1.0, -1.0, -1.0));
            DirectionalLight light4 = new DirectionalLight(Colors.White, new Vector3D(0.0, 1.0, 0.0));

            Model3DGroup mg = new Model3DGroup();
            mg.Children.Add(light1);
            mg.Children.Add(light2);
            mg.Children.Add(light3);
            mg.Children.Add(light4);
            _defaultLightingModel.Content = mg;
            _viewport.Children.Add(_defaultLightingModel);

            _rsd = new ResourceStringDecorator();
            _rsd.AssemblyName = "Perspective.Wpf";
            _rsd.BaseName = "Controls.Strings.Workshop3D";
            AddVisualChild(_rsd);
            
            _commandPanel = new StackPanel();
            _commandPanel.Focusable = false;
            _commandPanel.Orientation = Orientation.Vertical;
            ComponentResourceKey panelStyleKey = new ComponentResourceKey(typeof(ResourceKeys), "PanelStyle");
            _commandPanel.Style = (Style)this.FindResource(panelStyleKey);
            _commandPanel.Opacity = _opacity;
            _rsd.Child = _commandPanel;

            ComponentResourceKey groupBoxStyleKey = new ComponentResourceKey(typeof(ResourceKeys), "GroupBoxStyle");
            Style groupBoxStyle = (Style)this.FindResource(groupBoxStyleKey);

            GroupBox gbZoom = new GroupBox();
            gbZoom.Style = groupBoxStyle;
            _commandPanel.Children.Add(gbZoom);
            
            // must be called after the insertion in the element tree
            // to get benefit of the ResourceManager inherited property
            gbZoom.Header = ResourceStringDecorator.InitializeValue(gbZoom, GroupBox.HeaderProperty, "FieldOfView");

            StackPanel spZoom = new StackPanel();
            gbZoom.Content = spZoom;

            _cameraZoomJoystick = new Joystick();
            _cameraZoomJoystick.Dimensions = Dimensions.One;
            _cameraZoomJoystick.Focusable = false;
            _cameraZoomJoystick.IsTabStop = false;
            _cameraZoomJoystick.Startup += new StartupEventHandler(_cameraZoomJoystick_Startup);
            _cameraZoomJoystick.Stop += new RoutedEventHandler(_cameraZoomJoystick_Stop);
            spZoom.Children.Add(_cameraZoomJoystick);

            TextBlock tbZoom = new TextBlock();
            SignalBinding bZoom = new SignalBinding();

#if DN35
            // C# 3.0 : lambda expressions
            bZoom.Converting += (sender, e) =>
#else
            // C# 2.0 : anonymous methods
            bZoom.Converting += delegate(object sender, ConverterEventArgs e)
#endif
            {
                e.ConvertedValue = String.Format(e.Culture, "{0:###.0}°", e.Value);
            };

            bZoom.Source = camera;
            bZoom.Path = new PropertyPath("FieldOfView");
            tbZoom.SetBinding(TextBlock.TextProperty, bZoom);
            spZoom.Children.Add(tbZoom);

            GroupBox gbPosition = new GroupBox();
            gbPosition.Style = groupBoxStyle;
            _commandPanel.Children.Add(gbPosition);

            // must be called after the insertion in the element tree
            // to get benefit of the ResourceManager inherited property
            gbPosition.Header = ResourceStringDecorator.InitializeValue(gbPosition, GroupBox.HeaderProperty, "Position");

            StackPanel spPosition = new StackPanel();
            gbPosition.Content = spPosition;

            _cameraPositionJoystick = new Joystick();
            _cameraPositionJoystick.Focusable = false;
            _cameraPositionJoystick.IsTabStop = false;
            _cameraPositionJoystick.Startup += new StartupEventHandler(_cameraPositionJoystick_Startup);
            _cameraPositionJoystick.Stop += new RoutedEventHandler(_cameraPositionJoystick_Stop);
            spPosition.Children.Add(_cameraPositionJoystick);

            TextBlock tbPosition = new TextBlock();
            SignalBinding bPosition = new SignalBinding();

#if DN35
            // C# 3.0 : lambda expressions
            bPosition.Converting += (sender, e) =>
#else
            // C# 2.0 : anonymous methods
            bPosition.Converting += delegate(object sender, ConverterEventArgs e)
#endif
            {
                Point3D p = (Point3D)e.Value;
                e.ConvertedValue = String.Format(
                    e.Culture, 
                    "{0:###.0}, {1:###.0}, {2:###.0}", 
                    p.X, p.Y, p.Z);
            };

            bPosition.Source = camera;
            bPosition.Path = new PropertyPath("Position");
            tbPosition.SetBinding(TextBlock.TextProperty, bPosition);
            spPosition.Children.Add(tbPosition);

            GroupBox gbLookDirection = new GroupBox();
            gbLookDirection.Style = groupBoxStyle;
            _commandPanel.Children.Add(gbLookDirection);

            // must be called after the insertion in the element tree
            // to get benefit of the ResourceManager inherited property
            gbLookDirection.Header = ResourceStringDecorator.InitializeValue(gbLookDirection, GroupBox.HeaderProperty, "LookDirection");

            StackPanel spLookDirection = new StackPanel();
            gbLookDirection.Content = spLookDirection;

            _cameraLookDirectionJoystick = new Joystick();
            _cameraLookDirectionJoystick.Dimensions = Dimensions.Two;
            _cameraLookDirectionJoystick.Focusable = false;
            _cameraLookDirectionJoystick.IsTabStop = false;
            _cameraLookDirectionJoystick.Startup += new StartupEventHandler(_cameraLookDirectionJoystick_Startup);
            _cameraLookDirectionJoystick.Stop += new RoutedEventHandler(_cameraLookDirectionJoystick_Stop);
            spLookDirection.Children.Add(_cameraLookDirectionJoystick);

            TextBlock tbLookDirection = new TextBlock();
            SignalBinding bLookDirection = new SignalBinding();
#if DN35
            // C# 3.0 : lambda expressions
            bLookDirection.Converting += (sender, e) =>
#else
            // C# 2.0 : anonymous methods
            bLookDirection.Converting += delegate(object sender, ConverterEventArgs e)
#endif
            {
                Vector3D v = (Vector3D)e.Value;
                e.ConvertedValue = String.Format(
                    e.Culture,
                    "{0:N1}, {1:N1}, {2:N1}",
                    v.X, v.Y, v.Z);
            };
            
            bLookDirection.Source = camera;
            bLookDirection.Path = new PropertyPath("LookDirection");
            tbLookDirection.SetBinding(TextBlock.TextProperty, bLookDirection);
            spLookDirection.Children.Add(tbLookDirection);

            // maps the Children property on the Viewport3D Children property
            SetValue(ChildrenPropertyKey, _viewport.Children);

            Duration duration = new Duration(TimeSpan.FromSeconds(2.0));
            
            _cameraPositionAnimation = new Point3DAnimation();
            _cameraPositionAnimation.Duration = duration;
            _cameraPositionAnimation.Completed += _cameraPositionAnimation_Completed;

            _cameraLookDirectionAnimation = new Vector3DAnimation();
            _cameraLookDirectionAnimation.Duration = duration;
            _cameraLookDirectionAnimation.Completed += _cameraLookDirectionAnimation_Completed;

            _cameraZoomAnimation = new DoubleAnimation();
            _cameraZoomAnimation.Duration = duration;
            _cameraZoomAnimation.AccelerationRatio = 0.2;
            _cameraZoomAnimation.DecelerationRatio = 0.2;

            _cameraPositionRotation = new AxisAngleRotation3D();
            _cameraPositionRotateTransform = new RotateTransform3D(_cameraPositionRotation);

            _cameraLookDirectionRotation = new AxisAngleRotation3D();
            _cameraLookDirectionRotateTransform = new RotateTransform3D(_cameraLookDirectionRotation);

            this.Focusable = true;
        }

        private void InitializeCamera()
        {
            PerspectiveCamera camera = (_viewport.Camera as PerspectiveCamera);
            camera.Position = _initialCameraPosition;
            camera.LookDirection = _initialCameraLookDirection;
            camera.FieldOfView = _initialCameraFieldOfView;
        }

        /// <summary>
        /// Indicates if the command panel is visible.
        /// The default value is true.
        /// </summary>
        public bool ShowCommandPanel
        {
            get { return (bool)GetValue(ShowCommandPanelProperty); }
            set { SetValue(ShowCommandPanelProperty, value); }
        }

        /// <summary>
        /// Identifies the ShowCommandPanel dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowCommandPanelProperty =
            DependencyProperty.Register(
                "ShowCommandPanel", 
                typeof(bool), 
                typeof(Workshop3D), 
                new FrameworkPropertyMetadata(true, ShowCommandPanelPropertyChanged));
        
        /// <summary>
        /// Callback called when the ShowCommandPanel property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void ShowCommandPanelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool b = (bool)e.NewValue;
            Workshop3D w = (Workshop3D)d;
            if (b)
            {
                w._commandPanel.Visibility = Visibility.Visible;
            }
            else
            {
                w._commandPanel.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Indicates if the default lights should be used.
        /// The default value is true.
        /// </summary>
        public bool DefaultLighting
        {
            get { return (bool)GetValue(DefaultLightingProperty); }
            set { SetValue(DefaultLightingProperty, value); }
        }

        /// <summary>
        /// Identifies the DefaultLighting dependency property.
        /// </summary>
        public static readonly DependencyProperty DefaultLightingProperty =
            DependencyProperty.Register(
                "DefaultLighting", 
                typeof(bool), 
                typeof(Workshop3D), 
                new FrameworkPropertyMetadata(
                    true,
                    DefaultLightingPropertyChanged));

        /// <summary>
        /// Callback called when the DefaultLighting property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void DefaultLightingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool b = (bool)e.NewValue;
            Workshop3D w = (Workshop3D)d;
            if (b)
            {
                w._viewport.Children.Add(w._defaultLightingModel);
            }
            else
            {
                w._viewport.Children.Remove(w._defaultLightingModel);
            }
        }

        private void _cameraLookDirectionAnimation_Completed(object sender, EventArgs e)
        {
            // if the key remains pressed, the animation will restart
            _keyPressed = false;
            // _cameraOrienting = false;
        }

        private void _cameraPositionAnimation_Completed(object sender, EventArgs e)
        {
            // if the key remains pressed, the animation will restart
            _keyPressed = false;
            // _cameraTranslating = false;
        }

        /// <summary>
        /// Class handling for the PreviewKeyDown event.
        /// </summary>
        /// <param name="e">The KeyEventArgs that contains the event data.</param>
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            // this prevents focus changing
            if ((e.Key == Key.Up)
                || (e.Key == Key.Down)
                || (e.Key == Key.Left)
                || (e.Key == Key.Right)
                )
            {
                e.Handled = true;
            }

            // The event is fired several times when a key is down (while not released)
            // but we want to handle it only once
            if (!_keyPressed)
            {
                switch (e.Key)
                {
                    case Key.R:
                        if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt))
                        {
                            // Resets the camera
                            InitializeCamera();
                        }
                        break;

                    case Key.NumPad7:
                    case Key.Home:
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            CameraOrient(Direction.Forward | Direction.Left);
                        }
                        else if (Keyboard.Modifiers == ModifierKeys.None)
                        {
                            CameraTranslate(Direction.Forward | Direction.Left);
                        }
                        _keyPressed = true;
                        break;

                    case Key.NumPad4:
                    case Key.Left:
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            CameraOrient(Direction.Left);
                            _keyPressed = true;
                        }
                        else if (Keyboard.Modifiers == ModifierKeys.None)
                        {
                            CameraTranslate(Direction.Left);
                            _keyPressed = true;
                        }
                        else if (Keyboard.Modifiers == (ModifierKeys.Shift))
                        {
                            CameraOrbitOrigin(-2.0, OrbitKind.Horizontal);
                            // _keyPressed is not assigned so it may be repeated
                        }
                        break;

                    case Key.NumPad1:
                    case Key.End:
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            CameraOrient(Direction.Backward | Direction.Left);
                        }
                        else if (Keyboard.Modifiers == ModifierKeys.None)
                        {
                            CameraTranslate(Direction.Backward | Direction.Left);
                        }
                        _keyPressed = true;
                        break;

                    case Key.NumPad2:
                    case Key.Down:
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            CameraOrient(Direction.Backward);
                            _keyPressed = true;
                        }
                        else if (Keyboard.Modifiers == ModifierKeys.None)
                        {
                            CameraTranslate(Direction.Backward);
                            _keyPressed = true;
                        }
                        else if (Keyboard.Modifiers == (ModifierKeys.Shift))
                        {
                            CameraOrbitOrigin(2.0, OrbitKind.Vertical);
                            // _keyPressed is not assigned so it may be repeated
                        }
                        break;

                    case Key.NumPad3:
                    case Key.PageDown:
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            CameraOrient(Direction.Backward | Direction.Right);
                        }
                        else if (Keyboard.Modifiers == ModifierKeys.None)
                        {
                            CameraTranslate(Direction.Backward | Direction.Right);
                        }
                        _keyPressed = true;
                        break;

                    case Key.NumPad6:
                    case Key.Right:
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            CameraOrient(Direction.Right);
                            _keyPressed = true;
                        }
                        else if (Keyboard.Modifiers == ModifierKeys.None)
                        {
                            CameraTranslate(Direction.Right);
                            _keyPressed = true;
                        }
                        else if (Keyboard.Modifiers == (ModifierKeys.Shift))
                        {
                            CameraOrbitOrigin(2.0, OrbitKind.Horizontal);
                            // _keyPressed is not assigned so it may be repeated
                        }
                        break;

                    case Key.NumPad9:
                    case Key.PageUp:
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            CameraOrient(Direction.Forward | Direction.Right);
                        }
                        else if (Keyboard.Modifiers == ModifierKeys.None)
                        {
                            CameraTranslate(Direction.Forward | Direction.Right);
                        }
                        _keyPressed = true;
                        break;

                    case Key.Up:
                    case Key.NumPad8:
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            CameraOrient(Direction.Forward);
                            _keyPressed = true;
                        }
                        else if (Keyboard.Modifiers == ModifierKeys.None)
                        {
                            CameraTranslate(Direction.Forward);
                            _keyPressed = true;
                        }
                        else if (Keyboard.Modifiers == (ModifierKeys.Shift))
                        {
                            CameraOrbitOrigin(-2.0, OrbitKind.Vertical);
                            // _keyPressed is not assigned so it may be repeated
                        }
                        break;

                    case Key.Add:
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            CameraTranslate(Direction.Up);
                        }
                        else if (Keyboard.Modifiers == ModifierKeys.None)
                        {
                            CameraZoom(Direction.Forward);
                        }
                        _keyPressed = true;
                        break;

                    case Key.Subtract:
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            CameraTranslate(Direction.Down);
                        }
                        else if (Keyboard.Modifiers == ModifierKeys.None)
                        {
                            CameraZoom(Direction.Backward);
                        }
                        _keyPressed = true;
                        break;

                    case Key.NumPad5:
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                        {
                            CameraTranslate(Direction.Down);
                        }
                        else
                        {
                            CameraTranslate(Direction.Up);
                        }
                        _keyPressed = true;
                        break;
                }
            }
            base.OnPreviewKeyDown(e);
        }

        /// <summary>
        /// Class handling for the PreviewKeyUp event.
        /// </summary>
        /// <param name="e">The KeyEventArgs that contains the event data.</param>
        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            if (_keyPressed)
            {
                // testing these flags is not reliable
                //if (_cameraTranslating)
                //{
                //    CameraTranslate(null);
                //}
                //if (_cameraOrienting)
                //{
                //    CameraOrient(null);
                //}
                //if (_cameraZooming)
                //{
                //    CameraZoom(null);
                //}
                CameraTranslate(null);
                CameraOrient(null);
                CameraZoom(null);
                _keyPressed = false;
            }
            base.OnPreviewKeyUp(e);
        }

        private void _cameraPositionJoystick_Startup(object sender, Perspective.Core.RoutedEventArgs<Direction> e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                const double angle = 30.0;
                switch (e.Value)
                {
                    case Direction.Left :
                        CameraOrbitOrigin(-angle, OrbitKind.Horizontal);
                        break;
                    case Direction.Right:
                        CameraOrbitOrigin(angle, OrbitKind.Horizontal);
                        break;
                    case Direction.Forward:
                        CameraOrbitOrigin(-angle, OrbitKind.Vertical);
                        break;
                    case Direction.Backward:
                        CameraOrbitOrigin(angle, OrbitKind.Vertical);
                        break;
                }
            }
            else
            {
                CameraTranslate(e.Value);
            }
        }

        private void _cameraPositionJoystick_Stop(object sender, RoutedEventArgs e)
        {
            CameraTranslate(null);
        }

        private void _cameraLookDirectionJoystick_Startup(object sender, Perspective.Core.RoutedEventArgs<Direction> e)
        {
            CameraOrient(e.Value);
        }

        private void _cameraLookDirectionJoystick_Stop(object sender, RoutedEventArgs e)
        {
            CameraOrient(null);
        }

        private void _cameraZoomJoystick_Startup(object sender, Perspective.Core.RoutedEventArgs<Direction> e)
        {
            CameraZoom(e.Value);
        }

        private void _cameraZoomJoystick_Stop(object sender, RoutedEventArgs e)
        {
            CameraZoom(null);
        }

        /// <summary>
        /// Animates the position of the camera around the origin.
        /// (When orbit kind is Vertical, the view may change in the viewport
        /// because of the Camera.UpDirection value)
        /// </summary>
        /// <param name="angleOffset">Rotation angle</param>
        /// <param name="ok">Orbit kind (horizontal or vertical)</param>
        private void CameraOrbitOrigin(double angleOffset, OrbitKind ok)
        {
            PerspectiveCamera camera = (PerspectiveCamera)_viewport.Camera;
            _cameraPositionRotation.Angle = angleOffset;
            if (ok != _lastOk)
            {
                if (ok == OrbitKind.Horizontal)
                {
                    _cameraPositionRotation.Axis = _yAxis;
                }
                else
                {
                    _cameraPositionRotation.Axis = Vector3D.CrossProduct(camera.LookDirection, _yAxis);
                }
                _cameraLookDirectionRotation.Axis = _cameraPositionRotation.Axis;
                _lastOk = ok;
            }
            Point3D newPosition = _cameraPositionRotateTransform.Transform(camera.Position);
            camera.Position = newPosition;

            _cameraLookDirectionRotateTransform.CenterX = newPosition.X;
            _cameraLookDirectionRotateTransform.CenterY = newPosition.Y;
            _cameraLookDirectionRotateTransform.CenterZ = newPosition.Z;
            _cameraLookDirectionRotation.Angle = angleOffset;
            camera.LookDirection = _cameraLookDirectionRotateTransform.Transform(camera.LookDirection);
        }
        
        
        private void CameraTranslate(Direction? d)
        {
            PerspectiveCamera camera = (PerspectiveCamera)_viewport.Camera;

            if (d == null)
            {
                // Stops the animation
                
                // testing _cameraTranslating is not reliable
                // if (_cameraTranslating && camera.HasAnimatedProperties)
                
                if (camera.HasAnimatedProperties)
                {
                    Point3D currentPosition = camera.Position;
                    camera.ApplyAnimationClock(PerspectiveCamera.PositionProperty, null);
                    camera.Position = currentPosition;
                    // _cameraTranslating = false;
                }
            }
            else
            {
                Vector3D v = camera.LookDirection;
                Vector3D vY = new Vector3D(0.0, 1.0, 0.0);
                Vector3D vRelativeHorizontalAxis = Vector3D.CrossProduct(v, vY);
                Vector3D vRelativeVerticalAxis = Vector3D.CrossProduct(v, vRelativeHorizontalAxis);

                if (d != Direction.Forward)
                {
                    AxisAngleRotation3D rotation = new AxisAngleRotation3D();
                    switch (d)
                    {
                        case Direction.Forward | Direction.Left:
                            rotation.Axis = vRelativeVerticalAxis;
                            rotation.Angle = -45.0;
                            break;
                        case Direction.Left:
                            rotation.Axis = vRelativeVerticalAxis;
                            rotation.Angle = -90.0;
                            break;
                        case Direction.Backward | Direction.Left:
                            rotation.Axis = vRelativeVerticalAxis;
                            rotation.Angle = -135.0;
                            break;
                        case Direction.Backward:
                            rotation.Axis = vRelativeVerticalAxis;
                            rotation.Angle = 180.0;
                            break;
                        case Direction.Backward | Direction.Right:
                            rotation.Axis = vRelativeVerticalAxis;
                            rotation.Angle = 135.0;
                            break;
                        case Direction.Right:
                            rotation.Axis = vRelativeVerticalAxis;
                            rotation.Angle = 90.0;
                            break;
                        case Direction.Forward | Direction.Right:
                            rotation.Axis = vRelativeVerticalAxis;
                            rotation.Angle = 45.0;
                            break;
                        case Direction.Up:
                            rotation.Axis = vRelativeHorizontalAxis;
                            rotation.Angle = 90.0;
                            break;
                        case Direction.Down:
                            rotation.Axis = vRelativeHorizontalAxis;
                            rotation.Angle = -90.0;
                            break;
                    }
                    RotateTransform3D transform = new RotateTransform3D(rotation);
                    v = transform.Transform(camera.LookDirection);
                }
                Point3D p = new Point3D(v.X, v.Y, v.Z);
                _cameraPositionAnimation.By = p;
                //_cameraTranslating = true;
                camera.ApplyAnimationClock(PerspectiveCamera.PositionProperty, _cameraPositionAnimation.CreateClock());
            }
        }

        private void CameraOrient(Direction? d)
        {
            PerspectiveCamera camera = (PerspectiveCamera)_viewport.Camera;
            if (d == null)
            {
                // Stops the animation

                // testing _cameraOrienting is not reliable
                // if (_cameraOrienting && _viewport.Camera.HasAnimatedProperties)
                if (_viewport.Camera.HasAnimatedProperties)
                {
                    Vector3D currentDirection = camera.LookDirection;
                    camera.ApplyAnimationClock(PerspectiveCamera.LookDirectionProperty, null);
                    camera.LookDirection = currentDirection;
                    //_cameraOrienting = false;
                }
            }
            else
            {
                Vector3D v = camera.LookDirection;
                Vector3D vY = new Vector3D(0.0, 1.0, 0.0);
                Vector3D vRelativeHorizontalAxis = Vector3D.CrossProduct(v, vY);
                Vector3D vRelativeVerticalAxis = Vector3D.CrossProduct(v, vRelativeHorizontalAxis);

                double targetAngle = 45.0; // MUST be under 90.0 (if 90.0, Forward and Forward|Left have the same target vector)

                // this code supports direction combinations
                if ((d & Direction.Forward) == Direction.Forward)
                {
                    v = Helper3D.RotateVector(v, targetAngle, vRelativeHorizontalAxis);
                }
                if ((d & Direction.Backward) == Direction.Backward)
                {
                    v = Helper3D.RotateVector(v, -targetAngle, vRelativeHorizontalAxis);
                }
                if ((d & Direction.Left) == Direction.Left)
                {
                    v = Helper3D.RotateVector(v, -targetAngle, vRelativeVerticalAxis);
                }
                if ((d & Direction.Right) == Direction.Right)
                {
                    v = Helper3D.RotateVector(v, targetAngle, vRelativeVerticalAxis);
                }

                if (v != camera.LookDirection)
                {
                    _cameraLookDirectionAnimation.To = v;
                    camera.ApplyAnimationClock(PerspectiveCamera.LookDirectionProperty, _cameraLookDirectionAnimation.CreateClock());
                    //_cameraOrienting = true;
                }
            }
        }

        private void CameraZoom(Direction? d)
        {
            PerspectiveCamera camera = (PerspectiveCamera)_viewport.Camera;
            if (d == null)
            {
                // Stops the animation

                // testing _cameraZooming is not reliable
                // if (_cameraZooming && _viewport.Camera.HasAnimatedProperties)
                if (_viewport.Camera.HasAnimatedProperties)
                {
                    double fieldOfView = camera.FieldOfView;
                    camera.ApplyAnimationClock(PerspectiveCamera.FieldOfViewProperty, null);
                    camera.FieldOfView = fieldOfView;
                    //_cameraZooming = false;
                }
            }
            else
            {
                // PerspectiveCamera.FieldOfView must be between 0 and 180 degrees
                if (d == Direction.Forward)
                {
                    _cameraZoomAnimation.To = 0.1;
                }
                else if (d == Direction.Backward)
                {
                    _cameraZoomAnimation.To = 179.9;
                }
                if ((d == Direction.Forward)
                    || (d == Direction.Backward))
                {
                    camera.ApplyAnimationClock(PerspectiveCamera.FieldOfViewProperty, _cameraZoomAnimation.CreateClock());
                    //_cameraZooming = true;
                }
            }
        }

        /// <summary>
        /// Gets a collection of the Visual3D children of the Workshop3D.
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(
           System.ComponentModel.DesignerSerializationVisibility.Content)]
        public Visual3DCollection Children
        {
            get
            {
                return (Visual3DCollection)GetValue(ChildrenProperty);
            }
        }

        /// <summary>
        /// Key of the Children dependency property.
        /// </summary>
        private static readonly DependencyPropertyKey ChildrenPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Children",
                typeof(Visual3DCollection),
                typeof(Workshop3D),
                new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Identifies the Children dependency property.
        /// </summary>
        public static readonly DependencyProperty ChildrenProperty =
            ChildrenPropertyKey.DependencyProperty;

        /// <summary>
        /// Overrides Visual3D.GetVisualChild(int index)
        /// </summary>
        protected override System.Windows.Media.Visual GetVisualChild(int index)
        {
            if (index == 1)
            {
                return _rsd;
            }
            return _viewport;
        }

        /// <summary>
        /// Overrides Visual.VisualChildrenCount
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Measures the size in layout required for child elements and determines a size for the element.
        /// </summary>
        /// <param name="availableSize">The available size that this element can give to child elements.</param>
        /// <returns>The size that this element determines it needs during layout, based on its calculations of child element sizes.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            Size desiredSize = new Size();

            _rsd.Measure(availableSize);
            desiredSize.Width += _rsd.DesiredSize.Width;
            desiredSize.Height += _rsd.DesiredSize.Height;

            return desiredSize;
        }

        /// <summary>
        /// Positions child elements and determines a size for the element.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect rViewport = new Rect(finalSize);
            _viewport.Arrange(rViewport);

            double space = 15.0;

            // put the command panel in the bottom-left corner
            Rect r = new Rect(
                rViewport.Width - _rsd.DesiredSize.Width - space,
                rViewport.Bottom - _rsd.DesiredSize.Height - space,
                _rsd.DesiredSize.Width,
                _rsd.DesiredSize.Height);
            _rsd.Arrange(r);

            return finalSize;
        }

        /// <summary>
        /// Takes a screenshot of the 3D scene.
        /// </summary>
        /// <param name="dpi">Screenshot resolution, in dpi (i.e. 300).</param>
        /// <returns>A RenderTargetBitmap object.</returns>
        public RenderTargetBitmap TakeScreenshot(int dpi)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap(
              Convert.ToInt32(_viewport.ActualWidth * dpi / 96),
              Convert.ToInt32(_viewport.ActualHeight * dpi / 96),
              dpi, dpi, PixelFormats.Pbgra32);
            rtb.Render(_viewport);
            return rtb;
        }


        //public PerspectiveCamera Camera
        //{
        //    get { return _viewport.Camera as PerspectiveCamera; }
        //}


        //public double FieldOfView
        //{
        //    get { return (double)GetValue(FieldOfViewProperty); }
        //    set { SetValue(FieldOfViewProperty, value); }
        //}

        //public static readonly DependencyProperty FieldOfViewProperty =
        //    DependencyProperty.Register(
        //        "FieldOfView", 
        //        typeof(double), 
        //        typeof(Workshop3D), 
        //        new PropertyMetadata(
        //            50.0,
        //            FieldOfViewPropertyChanged));

        //private static void FieldOfViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    Workshop3D workshop = (Workshop3D)d;
        //    (workshop._viewport.Camera as PerspectiveCamera).FieldOfView = e.NewValue as double;
        //}

        
    }
}
