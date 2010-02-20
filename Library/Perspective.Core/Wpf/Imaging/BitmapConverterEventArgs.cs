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
    /// Provides event data for the events of BitmapConverter.
    /// </summary>
    public class BitmapConverterEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="originalFilename">The original image file name, without path.</param>
        /// <param name="convertedFilename">The converted image file name, without path.</param>
        /// <param name="originalPath">The path of the original file.</param>
        /// <param name="convertedPath">The path of the converted file.</param>
        public BitmapConverterEventArgs(
            string originalFilename,
            string convertedFilename,
            string originalPath,
            string convertedPath)
        {
            _originalFilename = originalFilename;
            _originalPath = originalPath;
            _convertedPath = convertedPath;
            _convertedFilename = convertedFilename;
        }

        private BitmapEncoder _encoder;

        /// <summary>
        /// Gets or sets the encoder for the bitmap.
        /// </summary>
        public BitmapEncoder Encoder
        {
            get { return _encoder; }
            set { _encoder = value; }
        }

        private string _convertedFileExtension;

        /// <summary>
        /// Gets or sets the converted file's extension.
        /// </summary>
        public string ConvertedFileExtension
        {
            get { return _convertedFileExtension; }
            set { _convertedFileExtension = value; }
        }

        private string _originalFilename;

        /// <summary>
        /// Gets the original file's name (without path).
        /// </summary>
        public string OriginalFilename
        {
            get { return _originalFilename; }
        }

        private string _convertedFilename;

        /// <summary>
        /// Gets or sets the converted file's name (without path).
        /// </summary>
        public string ConvertedFilename
        {
            get { return _convertedFilename; }
            set { _convertedFilename = value; }
        }

        private string _originalPath;

        /// <summary>
        /// Gets the path of the original file.
        /// </summary>
        public string OriginalPath
        {
            get { return _originalPath; }
        }

        private string _convertedPath;

        /// <summary>
        /// Gets the path of the converted file.
        /// </summary>
        public string ConvertedPath
        {
            get { return _convertedPath; }
        }
    }
}
