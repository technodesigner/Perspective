using Perspective.Wpf3D.Export;
using Perspective.Wpf3D.Primitives;
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
using Perspective.Core.Wpf;
using System.Windows.Media.Media3D;

namespace PerspectiveDemo.Wpf3D.View
{
    /// <summary>
    /// Logique d'interaction pour HouseDemo.xaml
    /// </summary>
    public partial class HouseDemo : Page
    {
        public HouseDemo()
        {
            InitializeComponent();
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            var element = FindName("house") as GeometryElement3D;
            var encoder = new StlEncoder();
            encoder.ModelName = "House";
            encoder.Sculptor = element.Sculptor;

            // Useful for MakerBot Replicator 2X / MakerWare
            Matrix3D mXRotation = Helper3D.GetXRotationMatrix(90.0);
            Matrix3D mScaling = Helper3D.GetScaleMatrix(10.0);
            MatrixTransform3D transform = new MatrixTransform3D(mXRotation * mScaling);
            encoder.PointsTransform = transform;

            encoder.Save();
        }
    }
}
