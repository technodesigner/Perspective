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
using System.Windows.Media.Imaging;
using System.IO;

namespace Perspective.Core.Wpf.Imaging
{
    /// <summary>
    /// A bitmap file conversion class.
    /// </summary>
    public class BitmapConverter : IBitmapConverter
    {
        /// <summary>
        /// Converts the bitmap files (whatever format, i.e. RAW or BMP) of a folder into files file which format and folder depend of the arguments.
        /// </summary>
        /// <param name="originalPath">The path of the original files.</param>
        /// <param name="convertedPath">The path of the converted files.</param>
        /// <param name="recursive">Indicates if the subfolders are processed.</param>
        /// <param name="overwriteExistingFiles">Indicates if the existing converted files should be overwritten.</param>
        public void ConvertFiles(
            string originalPath,
            string convertedPath,
            bool recursive,
            bool overwriteExistingFiles)
        {
            DirectoryInfo di = new DirectoryInfo(originalPath);
            FileInfo[] fileInfos = di.GetFiles();
            foreach (FileInfo fi in fileInfos)
                ConvertFile(fi.Name, originalPath, convertedPath, overwriteExistingFiles);
            if (recursive)
            {
                DirectoryInfo[] directoryInfos = di.GetDirectories();
                foreach (DirectoryInfo diItem in directoryInfos)
                {
                    string newOriginalPath = Path.Combine(originalPath, diItem.Name);
                    string newConvertedPath = Path.Combine(convertedPath, diItem.Name);
                    ConvertFiles(newOriginalPath, newConvertedPath, recursive, overwriteExistingFiles);
                }
            }
        }

        /// <summary>
        /// Converts a bitmap file (whatever format, i.e. RAW or BMP) into a file which format depends of the encoder passed as argument.
        /// A new BitmapEncoder has to be created in the Encoding event handler for each file (because the Save method can be called only once).
        /// </summary>
        /// <param name="originalFilename">The original image file name, without path.</param>
        /// <param name="originalPath">The path of the original file.</param>
        /// <param name="convertedPath">The path of the converted file.</param>
        /// <param name="overwriteExistingFile">Indicates if an existing converted file should be overwritten.</param>
        public void ConvertFile(
            string originalFilename,
            string originalPath,
            string convertedPath,
            bool overwriteExistingFile)
        {
            Directory.CreateDirectory(convertedPath);  
            BitmapDecoder decoder = GetDecoder(originalFilename, originalPath);
            if (_encoding != null)
            {
                BitmapConverterEventArgs e = new BitmapConverterEventArgs(
                    originalFilename, "", originalPath, convertedPath);
                _encoding(this, e);
                if (!String.IsNullOrEmpty(e.ConvertedFileExtension))
                {
                    e.ConvertedFilename = System.IO.Path.ChangeExtension(originalFilename, e.ConvertedFileExtension);
                }
                string convertedFullFilename = System.IO.Path.Combine(convertedPath, e.ConvertedFilename);
                if (overwriteExistingFile || !File.Exists(convertedFullFilename))
                {
                    using (FileStream fs = File.OpenWrite(convertedFullFilename))
                    {
                        if (e.Encoder != null)
                        {
                            e.Encoder.Frames.Add(decoder.Frames[0]);
                            e.Encoder.Save(fs);
                            if (_encoded != null)
                            {
                                _encoded(this, e);
                            }

                        }
                    }
                }
            }
        }

        private BitmapDecoder GetDecoder(
            string originalFilename,
            string originalPath)
        {
            string originalFullFileName = System.IO.Path.Combine(originalPath, originalFilename);
            return BitmapDecoder.Create(
              new Uri(originalFullFileName, UriKind.RelativeOrAbsolute),
              BitmapCreateOptions.None,
              BitmapCacheOption.None);
        }

        private event EventHandler<BitmapConverterEventArgs> _encoding;

        /// <summary>
        /// Event fired before encoding the bitmap.
        /// In the event handler, the encoder must be instanciated. e.ConvertedFileExtension or e.ConvertedFilename must be specified.
        /// </summary>
        public event EventHandler<BitmapConverterEventArgs> Encoding
        {
            add
            {
                _encoding += value;
            }
            remove
            {
                _encoding -= value;
            }
        }

        private event EventHandler<BitmapConverterEventArgs> _encoded;

        /// <summary>
        /// Event fired after encoding the bitmap.
        /// </summary>
        public event EventHandler<BitmapConverterEventArgs> Encoded
        {
            add
            {
                _encoded += value;
            }
            remove
            {
                _encoded -= value;
            }
        }
    }
}
