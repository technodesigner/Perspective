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
using Perspective.Core.Wpf.Imaging;
using System.Windows.Media.Imaging;

namespace Perspective.Core.Wpf.Imaging
{
    /// <summary>
    /// A bitmap to JPEG file conversion class.
    /// </summary>
    public class BitmapToJpegConverter : IBitmapConverter
    {
        BitmapConverter _converter = new BitmapConverter();

        /// <summary>
        /// Constructor.
        /// </summary>
        public BitmapToJpegConverter()
        {
            _converter.Encoding += new EventHandler<BitmapConverterEventArgs>(_converter_Encoding);
        }

        private int _qualityLevel;

        /// <summary>
        /// Jpeg quality level.
        /// </summary>
        public int QualityLevel
        {
            get { return _qualityLevel; }
            set { _qualityLevel = value; }
        }

        /// <summary>
        /// Converts the bitmap files (whatever format, i.e. RAW or BMP) of a folder into Jpeg files.
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
            _converter.ConvertFiles(originalPath, convertedPath, recursive, overwriteExistingFiles);
        }

        /// <summary>
        /// Converts a bitmap file (whatever format, i.e. RAW or BMP) into a Jpeg file.
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
            _converter.ConvertFile(originalFilename, originalPath, convertedPath, overwriteExistingFile);
        }

        void _converter_Encoding(object sender, BitmapConverterEventArgs e)
        {
            e.Encoder = new JpegBitmapEncoder();
            (e.Encoder as JpegBitmapEncoder).QualityLevel = _qualityLevel;
            e.ConvertedFileExtension = ".jpg";
        }

        /// <summary>
        /// Event fired before encoding the bitmap.
        /// </summary>
        public event EventHandler<BitmapConverterEventArgs> Encoding
        {
            add
            {
                _converter.Encoding += value;
            }
            remove
            {
                _converter.Encoding -= value;
            }
        }

        /// <summary>
        /// Event fired after encoding the bitmap.
        /// </summary>
        public event EventHandler<BitmapConverterEventArgs> Encoded
        {
            add
            {
                _converter.Encoded += value;
            }
            remove
            {
                _converter.Encoded -= value;
            }
        }
    }
}
