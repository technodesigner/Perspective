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
using System.ComponentModel.Composition;
using Perspective.Hosting;
using System.ComponentModel.Composition.Hosting;
using System.Windows.Media.Imaging;
using System.IO;

namespace Perspective.Model
{
    /// <summary>
    /// Manages the data that represent the extensions.
    /// </summary>
    public class MainModel
    {
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Extension> Extensions { get; set; }

        /// <summary>
        /// Initializes a new instance of MainModel.
        /// </summary>
        public MainModel()
        {
            Compose();
        }

        private void Compose()
        {
            var catalog = new DirectoryCatalog("Extensions");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        private BitmapImage _randomImage = null;

        public BitmapImage RandomImage
        {
            get 
            {
                if (_randomImage == null)
                {
                    var di = new DirectoryInfo(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                                "Perspective"));
                    FileInfo[] files = di.GetFiles("*.jpg");
                    if (files.GetLength(0) > 0)
                    {
                        Random r = new Random();
                        string fileName = files[r.Next(0, files.GetUpperBound(0))].FullName;
                        _randomImage = new BitmapImage(new Uri(fileName));
                    }
                }
                return _randomImage;
            }
        }
    }
}
