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
using Perspective.Wpf3D.Shapes;
using System.Windows.Media.Media3D;
using Perspective.Core.Primitives;
using Perspective.Wpf.Controls;
using Perspective.Core.Wpf;

namespace PerspectiveDemo.Wpf3D.View
{
    /// <summary>
    /// Interaction logic for Matrix3DDemo.xaml
    /// </summary>
    public partial class Matrix3DDemo : Page
    {
        public Matrix3DDemo()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Material grayMaterial;
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = Colors.LightGray;
            grayMaterial = new DiffuseMaterial(solidColorBrush);

            double initialAngle = 30.0;
            double angle = 15.0;

            Point3D p1 = new Point3D(0.0, 1.0, 0.0);
            p1 *= Helper3D.GetXRotationMatrix(initialAngle);
            DisplayPoint(p1, xWorkshop, grayMaterial);
            p1 *= Helper3D.GetXRotationMatrix(angle);
            DisplayPoint(p1, xWorkshop);

            Point3D p2 = new Point3D(1.0, 0.0, 0.0);
            p2 *= Helper3D.GetYRotationMatrix(initialAngle);
            DisplayPoint(p2, yWorkshop, grayMaterial);
            p2 *= Helper3D.GetYRotationMatrix(angle);
            DisplayPoint(p2, yWorkshop);
            
            Point3D p3 = new Point3D(1.0, 0.0, 0.0);
            p3 *= Helper3D.GetZRotationMatrix(initialAngle);
            DisplayPoint(p3, zWorkshop, grayMaterial);
            p3 *= Helper3D.GetZRotationMatrix(angle);
            DisplayPoint(p3, zWorkshop);

            //MatrixTransform3D mt = new MatrixTransform3D(
            //    Helper3D.GetPerspectiveMatrix(
            //        90.0,
            //        1.6,
            //        1.0,
            //        10.0));
            //box.Transform = mt;

            //MatrixTransform3D mt = new MatrixTransform3D(
            //    Helper3D.GetZRotationMatrix(
            //        45.0));
            //box.Transform = mt;

            Matrix3D mXRotation = Helper3D.GetXRotationMatrix(45.0);
            Matrix3D mZRotation = Helper3D.GetZRotationMatrix(45.0);
            MatrixTransform3D mt = new MatrixTransform3D(mXRotation * mZRotation);
            box.Transform = mt;
            Matrix m;
        }

        private void DisplayPoint(Point3D p, Workshop3D workshop, Material material = null)
        {
            Spherical3D sphere = new Spherical3D();
            if (material != null)
            {
                sphere.Material = material;
            }
            Transform3DGroup tg = new Transform3DGroup();
            ScaleTransform3D scaling = new ScaleTransform3D(0.05, 0.05, 0.05);
            tg.Children.Add(scaling);
            TranslateTransform3D translation = new TranslateTransform3D(p.X, p.Y, p.Z);
            tg.Children.Add(translation);

            sphere.Transform = tg;
            workshop.Children.Add(sphere);
        }
    }
}
