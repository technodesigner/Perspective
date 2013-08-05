//using System.CodeDom.Compiler;
using Perspective.Wpf3D.Primitives;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Perspective.Wpf3D.Export
{
    public class ModelExporter
    {
        public GeometryElement3D Element { get; private set; }
        public I3dModelEncoder Encoder { get; private set; }

        //public string ModelName { get; set; }

        // public double ScaleFactor { get; set; }
       //  public Transform3D PointsTransform { get; set; }

        public ModelExporter(GeometryElement3D element, I3dModelEncoder encoder)
        {
            Element = element;
            Encoder = encoder;
            // ScaleFactor = 1.0;
        }
        public void Execute()
        {
            //var sculptor = new Sculptor(Element.Sculptor);

            Encoder.Save(sculptor);


        }
    }
}
