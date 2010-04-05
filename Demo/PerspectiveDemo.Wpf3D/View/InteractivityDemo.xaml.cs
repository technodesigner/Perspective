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
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace PerspectiveDemo.Wpf3D.View
{
    /// <summary>
    /// Interaction logic for InteractivityDemo.xaml
    /// </summary>
    public partial class InteractivityDemo : Page
    {
        public InteractivityDemo()
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
