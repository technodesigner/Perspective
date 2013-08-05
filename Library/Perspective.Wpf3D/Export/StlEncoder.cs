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
using Perspective.Wpf3D.Primitives;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Perspective.Wpf3D.Export
{
    public class StlEncoder : I3dModelEncoder
    {
        public string ModelName { get; set; }
        public Sculptor Sculptor { get; set; }

        public Transform3D PointsTransform { get; set; }

        private IndentedTextWriter _out;
        private StreamWriter _writer;
        public void Save()
        {
            var sculptor = new Sculptor(Sculptor);
            if (PointsTransform != null)
            {
                sculptor.Transform(PointsTransform);
            }
            _writer = new StreamWriter(String.Format("{0}.stl", ModelName));
            _out = new IndentedTextWriter(_writer);
            _out.WriteLine("solid {0}", ModelName);
            _out.Indent++;
            try
            {
                foreach (int vertexIndice in sculptor.Mesh.TriangleIndices)
                {
                    if (vertexIndice % 3 == 0)
                    {
                        _out.WriteLine("facet");
                        _out.Indent++;
                        _out.WriteLine("outer loop");
                        _out.Indent++;
                    }

                    Point3D p = sculptor.Mesh.Positions[vertexIndice];
                    _out.WriteLine("vertex {0} {1} {2}", p.X, p.Y, p.Z);

                    if (vertexIndice % 3 == 2)
                    {
                        _out.Indent--;
                        _out.WriteLine("endloop");            
                        _out.Indent--;
                        _out.WriteLine("endfacet");
                    }
                }
                _out.Indent--;
                _out.WriteLine("endsolid ", ModelName);
            }
            finally
            {
                _writer.Close();
            }
        }
    }
}
