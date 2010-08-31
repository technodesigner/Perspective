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
using System.Windows;
using Perspective.Wpf.ResourceStrings;

namespace Perspective.Model
{
    /// <summary>
    /// Manages the data that represent the extensions.
    /// </summary>
    public class ExtensionModel
    {
        //[ImportMany(AllowRecomposition = true)]
        //public IEnumerable<Extension> Extensions { get; set; }

        [ImportMany(AllowRecomposition = true)]
        private IEnumerable<Extension> _mefExtensions;

        public List<Extension> Extensions { get; private set; }

        /// <summary>
        /// Initializes a new instance of MainModel.
        /// </summary>
        public ExtensionModel()
        {
            Compose();

            Extensions = new List<Extension>();
            foreach (var extension in _mefExtensions)
            {
                Extensions.Add(extension);
            }
            if (Extensions.Count == 0)
            {
                ResourceStringManager resourceStringManager = new ResourceStringManager(
                    ResourceAssembly.AssemblyNameConst,
                    "Model.Strings.ExtensionModel");
                MessageBox.Show(resourceStringManager.GetString("NoMefModuleWarning"));
            }
            else
            {
                Extensions.Sort(CompareExtensions);
            }
        }

        private void Compose()
        {
            var catalog = new DirectoryCatalog("Extensions");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        private static BitmapImage GetRandomImage(string dirName)
        {
            BitmapImage randomImage = null;
            DirectoryInfo di = new DirectoryInfo(dirName);
            FileInfo[] files = di.GetFiles("*.jpg");
            if (files.GetLength(0) > 0)
            {
                Random r = new Random();
                string fileName = files[r.Next(0, files.GetUpperBound(0))].FullName;
                randomImage = new BitmapImage(new Uri(fileName));
            }
            return randomImage;
        }

        private BitmapImage _randomImage = null;

        public BitmapImage RandomImage
        {
            get 
            {
                if (_randomImage == null)
                {
                    const string folderName = "PerspectiveAlbum";
                    if (Directory.Exists(folderName))
                    {
                        _randomImage = GetRandomImage(folderName);
                    }
                    else
                    {
                        string dirName = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                                folderName);
                        if (Directory.Exists(dirName))
                        {
                            _randomImage = GetRandomImage(dirName);
                        }
                    }
                }
                return _randomImage;
            }
        }

        private static int CompareExtensions(Extension e1, Extension e2)
        {
            return e1.SortOrder.CompareTo(e2.SortOrder);
        }
   
    }
}
