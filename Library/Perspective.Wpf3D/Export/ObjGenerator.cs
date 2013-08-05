using Perspective.Wpf3D.Primitives;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Perspective.Wpf3D.Export
{
    public class ObjGenerator : IModelCodeGenerator
    {
        public string ModelName { get; set; }

        private IndentedTextWriter _out;
        private StreamWriter _writer;
        public void Execute(Sculptor sculptor)
        {
            _writer = new StreamWriter(String.Format("{0}.obj", ModelName));
            _out = new IndentedTextWriter(_writer);
            try
            {
                _out.WriteLine("# Exported by Perspective for WPF, {0}", Perspective.Core.LibraryInfo.Copyright);
                _out.WriteLine("g {0}", this.ModelName);
                _out.WriteLine();


                //foreach (int vertexIndice in Element.Sculptor.Mesh.TriangleIndices)
                //{
                //    Generator.MakeVertex(Element.Sculptor.Mesh.Positions[vertexIndice]);
                //    /*
                //    Point p = Element.Sculptor.Mesh.TextureCoordinates[vertexIndice];
                //    Generator.MakeTextureCoordinates(p);
                //    if (Element.Sculptor.Mesh.Normals.Count >= vertexIndice + 1)
                //    {
                //        Vector3D v = Element.Sculptor.Mesh.Normals[vertexIndice];
                //        Generator.MakeNormal(v);
                //    }
                //     * */
                //}

                for (int vertexIndice = sculptor.Mesh.TriangleIndices.Count - 1; vertexIndice >= 0; vertexIndice--)
                // foreach (int vertexIndice in sculptor.Mesh.TriangleIndices)
                {
                    //if (vertexIndice % 3 == 0)
                    //{
                    //    Generator.MakeFaceIndicesHeader(); // OBJ
                    //}
                    //Generator.MakeFaceIndice(vertexIndice); // OBJ
                    //Generator.MakeFaceIndice(
                    //    vertexIndice,
                    //    sculptor.Mesh.TextureCoordinates.IndexOf(
                    //        sculptor.Mesh.TextureCoordinates[vertexIndice]),
                    //    sculptor.Mesh.Normals.IndexOf(
                    //        sculptor.Mesh.Normals[vertexIndice]));


                    Point3D p = sculptor.Mesh.Positions[vertexIndice];
                    _out.WriteLine("v {0} {1} {2}", p.X, p.Y, p.Z); 

                    if (vertexIndice % 3 == 0)
                    {
                        // _out.WriteLine("f {0} {1} {2}", vertexIndice - 2, vertexIndice - 1, vertexIndice);
                        // _out.WriteLine("f {0} {1} {2}", vertexIndice, vertexIndice - 1, vertexIndice - 2);
                        // _out.WriteLine("f {0} {1} {2}", vertexIndice, vertexIndice + 1, vertexIndice + 2);
                        _out.WriteLine("f {0} {1} {2}", vertexIndice + 2, vertexIndice + 1, vertexIndice);
                        _out.WriteLine();
                    }
                }
                _out.WriteLine();
            }
            finally
            {
                _writer.Close();
            }
        }
    }
}
