using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerspectiveDemo.Wpf3D.Model;
using PerspectiveDemo.Wpf3D.Entities;

namespace PerspectiveDemo.Wpf3D.ViewModel
{
    public class BubbleViewModel
    {
        private BubbleModel _bubbleModel = new BubbleModel();

        public List<Bubble> Bubbles
        {
            get { return _bubbleModel.Bubbles; }
        }
    }
}
