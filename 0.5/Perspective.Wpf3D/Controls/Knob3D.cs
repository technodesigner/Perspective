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
using System.Windows.Media.Media3D;
using System.Windows.Input;
using Perspective.Wpf3D.Primitives;
using Perspective.Core.Primitives;
using System.Windows.Media;
using System.Windows.Controls;

namespace Perspective.Wpf3D.Controls
{
    /// <summary>
    /// A 3D knob (rotative button) class, compatible with range element multiselection.
    /// <see cref="RangeElementSelectionManager"/>
    /// It supports Mouse wheel.
    /// </summary>
    public class Knob3D : RangeBase3D
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static Knob3D()
        {
            ValueProperty.OverrideMetadata(
                typeof(Knob3D),
                new UIPropertyMetadata(
                    OnValueChanged));

            MinimumProperty.OverrideMetadata(
                typeof(Knob3D),
                new UIPropertyMetadata(
                    OnMinimumChanged));

            MaximumProperty.OverrideMetadata(
                typeof(Knob3D),
                new UIPropertyMetadata(
                    OnMaximumChanged));
        }

        /// <summary>
        /// Callback called when the Value property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.InvalidateProperty(AngleProperty);
        }

        /// <summary>
        /// Callback called when the Minimum property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.InvalidateProperty(AngleProperty);            
        }

        /// <summary>
        /// Callback called when the Maximum property's value has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.InvalidateProperty(AngleProperty);
        }

        /// <summary>
        /// Gets the name (without path and extension) of the XAML file that describes the appearence of the control.
        /// </summary>
        protected override string ContentFileName
        {
            get
            {
                return "Knob3D";
            }
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
                typeof(Knob3D),
                new PropertyMetadata(
                    -30.0,
                    null,
                    CoerceAngle));

        /// <summary>
        /// Identifies the Angle dependency property.
        /// </summary>
        public static readonly DependencyProperty AngleProperty = AnglePropertyKey.DependencyProperty;

        private static object CoerceAngle(DependencyObject d, object value)
        {
            Knob3D k = ((Knob3D)d);
            double walkAngle = k.WalkAngle;
            return -(k.Rate * walkAngle + ((360.0 - walkAngle) / 2.0));
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
                typeof(Knob3D),
                new PropertyMetadata(
                    300.0,
                    WalkAngleChanged,
                    CoerceWalkAngle));

        private static void WalkAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Knob3D k = (Knob3D)d;
            k.InvalidateProperty(Knob3D.AngleProperty);
        }

        private static object CoerceWalkAngle(DependencyObject d, object value)
        {
            double walkAngle = (double)value;
            if (walkAngle < 0.0)
            {
                walkAngle = 0.0;
            }
            if (walkAngle > 360.0)
            {
                walkAngle = 360.0;
            }
            return walkAngle;
        }
    }
}
