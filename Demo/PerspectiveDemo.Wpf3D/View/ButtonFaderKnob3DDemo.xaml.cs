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

namespace PerspectiveDemo.Wpf3D.View
{
    /// <summary>
    /// Interaction logic for ButtonFaderKnob3DDemo.xaml
    /// </summary>
    public partial class ButtonFaderKnob3DDemo : Page
    {
        public ButtonFaderKnob3DDemo()
        {
            InitializeComponent();
        }

        private bool _animated;

        private void Button3D_Click(object sender, RoutedEventArgs e)
        {
            if (_animated)
            {
                double currentAngle = arrowRotation.Angle;
                arrowRotation.ApplyAnimationClock(AxisAngleRotation3D.AngleProperty, null);
                arrowRotation.Angle = currentAngle;
            }
            else
            {
                DoubleAnimation da = new DoubleAnimation(arrowRotation.Angle - 360.0, new Duration(TimeSpan.FromSeconds(5.0)));
                da.RepeatBehavior = RepeatBehavior.Forever;
                arrowRotation.ApplyAnimationClock(AxisAngleRotation3D.AngleProperty, da.CreateClock());
            }
            _animated = !_animated;
        }
    }
}
