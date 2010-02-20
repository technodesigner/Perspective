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

namespace Perspective.Core.Wpf.Imaging
{
    /// <summary>
    /// A bitmap to PNG file conversion class.
    /// </summary>
    public class BitmapToPngConverter : IBitmapConverter
    {
        BitmapConverter _converter = new BitmapConverter();

        /// <summary>
        /// Constructor.
        /// </summary>
        public BitmapToPngConverter()
        {
            _converter.Encoding += new EventHandler<BitmapConverterEventArgs>(_converter_Encoding);
        }

        PngInterlaceOption _interlace;

        /// <summary>
        /// Gets or sets a value that indicates whether bitmap should interlace.
        /// </summary>
        public PngInterlaceOption Interlace
        {
            get { return _interlace; }
            set { _interlace = value; }
        }

        void _converter_Encoding(object sender, BitmapConverterEventArgs e)
        {
            e.Encoder = new PngBitmapEncoder();
            (e.Encoder as PngBitmapEncoder).Interlace = _interlace;
            e.ConvertedFileExtension = ".png";
        }

        #region IBitmapConverter Members

        /// <summary>
        /// Converts the bitmap files (whatever format, i.e. RAW or BMP) of a folder into Png files.
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

        #endregion
    }
}
