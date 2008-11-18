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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using Perspective.Wpf3D.Controls;
using Perspective.Wpf3D.Shapes;

namespace PerspectiveDemo.UI.Pages.pWpf3D
{
    /// <summary>
    /// Interaction logic for CustomKnob3DDemo.xaml
    /// </summary>
    public partial class CustomKnob3DDemo : Page
    {
        public CustomKnob3DDemo()
        {
            InitializeComponent();
        }

        private void myKnob_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            GenerateCustomKnob();
        }

        private void GenerateCustomKnob()
        {
            AxisAngleRotation3D rotation = new AxisAngleRotation3D();
            rotation.Axis = new Vector3D(0.0, 0.0, 1.0);

            RotateTransform3D rotateTransform = new RotateTransform3D();

            rotateTransform.Rotation = rotation;
            
            TranslateTransform3D translateTransform = new TranslateTransform3D(2.0, 3.0, 0.0);

            Transform3DGroup tg = new Transform3DGroup();
            tg.Children.Add(rotateTransform);
            tg.Children.Add(translateTransform);

            Perspective.Wpf3D.Shapes.Bar3D bar = new Perspective.Wpf3D.Shapes.Bar3D();
            bar.SideCount = 6;
            bar.RoundingRate = 0.15;

            Perspective.Wpf3D.Controls.Knob3D knob = new Perspective.Wpf3D.Controls.Knob3D();
            knob.OverridesDefaultSkin = true;
            knob.Transform = tg;
            knob.Content = bar;

            Binding b = new Binding("Angle");
            b.Source = knob;
            BindingOperations.SetBinding(rotation, AxisAngleRotation3D.AngleProperty, b);

            workshop.Children.Add(knob);
        }
    }
}
