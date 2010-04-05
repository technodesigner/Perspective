using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerspectiveDemo.Wpf3D.Entities;
using System.Xml;
using System.Windows.Resources;
using System.Windows;
using System.Globalization;

namespace PerspectiveDemo.Wpf3D.Model
{
    public class BubbleModel
    {
        private List<Bubble> _bubbles = new List<Bubble>();

        public List<Bubble> Bubbles
        {
            get { return _bubbles; }
        }

        public BubbleModel()
    	{
            XmlDocument xd = new XmlDocument();

            StreamResourceInfo sri = Application.GetResourceStream(
                new Uri("pack://application:,,,/PerspectiveDemo.Wpf3D;component/Model/BubbleChartData.xml", UriKind.Absolute));
            xd.Load(sri.Stream);

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            XmlNodeList xnl = xd.SelectNodes("/DataList/Data");
            for (int i = 0; i < xnl.Count; i++)
            {
                Bubble bubble = new Bubble();
                XmlElement xe = (XmlElement)xnl.Item(i);
                bubble.X = Convert.ToDouble(xe.GetAttribute("X"), provider);
                bubble.Y = Convert.ToDouble(xe.GetAttribute("Y"), provider);
                bubble.Z = Convert.ToDouble(xe.GetAttribute("Z"), provider);
                bubble.Value = Convert.ToDouble(xe.GetAttribute("Value"), provider);
                _bubbles.Add(bubble);
            }
    	}
    }
}
