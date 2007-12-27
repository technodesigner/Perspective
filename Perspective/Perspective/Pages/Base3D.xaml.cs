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

namespace Perspective.Pages
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
        private void b1_Click(object sender, RoutedEventArgs e)
        {
            Perspective.Wpf3D.SkinManager.Current.LoadSkin("LaMoisson");
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            Perspective.Wpf3D.SkinManager.Current.LoadDefaultSkin();
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            Perspective.Wpf.SkinManager.Current.LoadSkin("BlackAndWhite");
        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            Perspective.Wpf.SkinManager.Current.LoadDefaultSkin();
        }

        private void b5_Click(object sender, RoutedEventArgs e)
        {
            //Window2 w2 = new Window2();
            //w2.Show();
        }

        private void b6_Click(object sender, RoutedEventArgs e)
        {
            GenerateCustomKnob();
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
