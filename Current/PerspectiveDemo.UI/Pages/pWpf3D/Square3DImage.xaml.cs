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

namespace PerspectiveDemo.UI.Pages.pWpf3D
{
    /// <summary>
    /// Interaction logic for Square3DImage.xaml
    /// </summary>
    public partial class Square3DImage : Page
    {
        public Square3DImage()
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
