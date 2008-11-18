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
using Perspective.Wpf3D.Primitives;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Perspective.Wpf3D.Controls
{
    /// <summary>
    /// A skinable 3D gyroscope control, animated when clicked.
    /// </summary>
    [Perspective.Core.SkinPart(Name = "PART_FrameRingRotation", Type = typeof(AxisAngleRotation3D))]
    [Perspective.Core.SkinPart(Name = "PART_RingModelRotation", Type = typeof(AxisAngleRotation3D))]
    [Perspective.Core.SkinPart(Name = "PART_RotorRotation", Type = typeof(AxisAngleRotation3D))]
    public class Gyroscope3D : ContentControl3D
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static Gyroscope3D()
        {
            ContentProperty.OverrideMetadata(
                typeof(Gyroscope3D),
                new UIPropertyMetadata(OnContentChanged));
        }

        private bool _isAnimated;
        DoubleAnimation _frameRingAngleAnimation;
        DoubleAnimation _ringModelAngleAnimation;
        DoubleAnimation _rotorAngleAnimation;

        /// <summary>
        /// Initializes a new instance of Gyroscope3D
        /// </summary>
        public Gyroscope3D()
        {
            _frameRingAngleAnimation = new DoubleAnimation();
            _frameRingAngleAnimation.Duration = new Duration(new TimeSpan(0, 0, 30));
            _frameRingAngleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            _ringModelAngleAnimation = new DoubleAnimation();
            _ringModelAngleAnimation.Duration = new Duration(new TimeSpan(0, 0, 15));
            _ringModelAngleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            _rotorAngleAnimation = new DoubleAnimation();
            _rotorAngleAnimation.Duration = new Duration(new TimeSpan(0, 0, 3));
            _rotorAngleAnimation.RepeatBehavior = RepeatBehavior.Forever;
        }

        AxisAngleRotation3D _frameRingRotation;
        AxisAngleRotation3D _ringModelRotation;
        AxisAngleRotation3D _rotorRotation;

        /// <summary>
        /// Callback called when the Content property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Gyroscope3D g = (Gyroscope3D)d;
            g._frameRingRotation = (AxisAngleRotation3D)g.FindName("PART_FrameRingRotation");
            g._ringModelRotation = (AxisAngleRotation3D)g.FindName("PART_RingModelRotation");
            g._rotorRotation = (AxisAngleRotation3D)g.FindName("PART_RotorRotation");
        }

        /// <summary>
        /// Gets the name (without path and extension) of the XAML file that describes the appearence of the control.
        /// </summary>
        protected override string ContentFileName
        {
            get
            {
                return "Gyroscope3D";
            }
        }

        // instead of using content named parts, it is possible to anime a property
        // then bound in the content file (see Button3D.PressEffect).

        //public double FrameRingAngle
        //{
        //    get { return (double)GetValue(FrameRingAngleProperty); }
        //    set { SetValue(FrameRingAngleProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for FrameRingAngle.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty FrameRingAngleProperty =
        //    DependencyProperty.Register(
        //        "FrameRingAngle", 
        //        typeof(double), 
        //        typeof(Gyroscope3D), 
        //        new UIPropertyMetadata(0.0));

        /// <summary>
        /// Invoked when an unhandled MouseLeftButtonDown routed event is raised on this element. 
        /// </summary>
        /// <param name="e">The MouseButtonEventArgs that contains the event data.</param>
        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
           if (!_isAnimated)
            {
                // Initialization of the animation objects : on 360 degrees after the current angle
                // _frameRingAngleAnimation.To = FrameRingAngle + 360.0;
                _frameRingAngleAnimation.To = _frameRingRotation.Angle + 360.0;
                _ringModelAngleAnimation.To = _ringModelRotation.Angle + 360.0;
                _rotorAngleAnimation.To = _rotorRotation.Angle - 360.0;

                // Triggers the animation on the FrameRingAngle property
                //this.ApplyAnimationClock(
                //    FrameRingAngleProperty, 
                //    _frameRingAngleAnimation.CreateClock());
                // Triggers the animation on the _frameRingAngleAnimation.Angle property
                _frameRingRotation.ApplyAnimationClock(
                    AxisAngleRotation3D.AngleProperty,
                    _frameRingAngleAnimation.CreateClock());

                // Triggers the animation on the _ringModelRotation.Angle property
                _ringModelRotation.ApplyAnimationClock(
                    AxisAngleRotation3D.AngleProperty,
                    _ringModelAngleAnimation.CreateClock());

                // Triggers the animation on the _rotorRotation.Angle property
                _rotorRotation.ApplyAnimationClock(
                    AxisAngleRotation3D.AngleProperty,
                    _rotorAngleAnimation.CreateClock());
            }
            else
            {
                // current angle memorization
                // double frameRingAngle = FrameRingAngle;
                double frameRingAngle = _frameRingRotation.Angle;
                double ringModelRotationAngle = _ringModelRotation.Angle;
                double rotorRotationAngle = _rotorRotation.Angle;

                // Halts the animations
                // this.ApplyAnimationClock(FrameRingAngleProperty, null);
                _frameRingRotation.ApplyAnimationClock(
                     AxisAngleRotation3D.AngleProperty,
                     null);
                _ringModelRotation.ApplyAnimationClock(
                    AxisAngleRotation3D.AngleProperty, 
                    null);
                _rotorRotation.ApplyAnimationClock(
                    AxisAngleRotation3D.AngleProperty,
                    null);

                // Reassignment of the angle (which otherwise resumes its initial value)
                _rotorRotation.Angle = rotorRotationAngle; 
                _ringModelRotation.Angle = ringModelRotationAngle;
                _frameRingRotation.Angle = frameRingAngle;
                // FrameRingAngle = frameRingAngle;
            }
           _isAnimated = !_isAnimated;
            base.OnMouseLeftButtonDown(e);
        }
    }
}
