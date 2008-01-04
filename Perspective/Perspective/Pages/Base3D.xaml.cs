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
using System.Collections.ObjectModel;
using Perspective.Wpf3D;
using System.Windows.Media.Media3D;
using System.Windows.Data;
using Perspective.Wpf3D.Controls;
using Perspective.Wpf3D.Shapes;

namespace PerspectiveDemo.Pages
{
    /// <summary>
    /// Interaction logic for Base3D.xaml
    /// </summary>
    public partial class Base3D : Page
    {
        public Base3D()
        {
            InitializeComponent();
        }

        private void bDynamicKnob_Click(object sender, RoutedEventArgs e)
        {
            GenerateCustomKnob();

            //Gyroscope3D g = new Gyroscope3D();
            //vMain.Children.Add(g);
        }

        private void GenerateCustomKnob()
        {
            AxisAngleRotation3D rotation = new AxisAngleRotation3D();
            rotation.Axis = new Vector3D(0.0, 0.0, 1.0);

            RotateTransform3D transform = new RotateTransform3D();

            transform.Rotation = rotation;

            Bar3D bar = new Bar3D();
            bar.SideCount = 3;
            bar.RoundingRate = 0.15;
            // bar.Material = (Material)Application.Current.TryFindResource("ControlMaterial");

            Knob3D knob = new Knob3D();

            // vMain.Children.Add(knob);

            knob.OverridesDefaultSkin = true;
            knob.Transform = transform;
            knob.Content = bar;

            Binding b = new Binding("Angle");
            b.Source = knob;
            BindingOperations.SetBinding(rotation, AxisAngleRotation3D.AngleProperty, b);

            vMain.Children.Add(knob);
        }

    }
}
