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
using System.Windows.Media.Animation;

namespace PerspectiveDemo.UI.Pages.pWpf3D
{
    /// <summary>
    /// Interaction logic for Interactivity.xaml
    /// </summary>
    public partial class Interactivity : Page
    {
        public Interactivity()
        {
            InitializeComponent();
        }

        private void Box3D_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Initialization of the animation object : on 360 degrees after the current angle
            DoubleAnimation da = new DoubleAnimation(boxRotation.Angle + 360.0, new Duration(TimeSpan.FromSeconds(5.0)));
            da.RepeatBehavior = RepeatBehavior.Forever;
            // Trigger the animation on the boxRotation's Angle property
            boxRotation.ApplyAnimationClock(AxisAngleRotation3D.AngleProperty, da.CreateClock());
        }
        private void Box3D_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // current angle memorization
            double currentAngle = boxRotation.Angle;
            // Stops the animation
            boxRotation.ApplyAnimationClock(AxisAngleRotation3D.AngleProperty, null);
            // Reassignment of the angle (which otherwise resumes its initial value)
            boxRotation.Angle = currentAngle;
        }
    }
}
