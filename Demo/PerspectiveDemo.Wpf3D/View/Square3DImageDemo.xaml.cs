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

namespace PerspectiveDemo.Wpf3D.View
{
    /// <summary>
    /// Interaction logic for Square3DImageDemo.xaml
    /// </summary>
    public partial class Square3DImageDemo : Page
    {
        public Square3DImageDemo()
        {
            InitializeComponent();
        }

        private void Square3D_MeshBuilt(object sender, Perspective.Wpf3D.Primitives.MeshBuiltEventArgs e)
        {
            // custom TextureCoordinates to rotate the image 90° on the left
            e.Mesh.TextureCoordinates.Clear();

            e.Mesh.TextureCoordinates.Add(new Point(0, 0));
            e.Mesh.TextureCoordinates.Add(new Point(0, 1));
            e.Mesh.TextureCoordinates.Add(new Point(1, 1));

            e.Mesh.TextureCoordinates.Add(new Point(0, 0));
            e.Mesh.TextureCoordinates.Add(new Point(1, 1));
            e.Mesh.TextureCoordinates.Add(new Point(1, 0));
        }
    }
}
