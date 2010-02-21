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

namespace Perspective.Core.Wpf.Imaging
{
    /// <summary>
    /// A bitmap file conversion contract.
    /// </summary>
    public interface IBitmapConverter
    {
        /// <summary>
        /// Converts a bitmap file (whatever format, i.e. RAW or BMP) into a file which format depends of the instanciated class.
        /// </summary>
        /// <param name="originalPath">The path of the original file.</param>
        /// <param name="convertedPath">The path of the converted file.</param>
        /// <param name="recursive">Indicates if the subdirectories should be processed.</param>
        /// <param name="overwriteExistingFiles">Indicates if the existing converted files should be overwritten.</param>
        void ConvertFiles(
            string originalPath,
            string convertedPath,
            bool recursive,
            bool overwriteExistingFiles);

        /// <summary>
        /// Converts a bitmap file (whatever format, i.e. RAW or BMP) into a file which format depends of the instanciated class.
        /// </summary>
        /// <param name="originalFilename">The original image file name, without path.</param>
        /// <param name="originalPath">The path of the original file.</param>
        /// <param name="convertedPath">The path of the converted file.</param>
        /// <param name="overwriteExistingFile">Indicates if an existing converted file should be overwritten.</param>
        void ConvertFile(
            string originalFilename,
            string originalPath,
            string convertedPath,
            bool overwriteExistingFile);

        /// <summary>
        /// Event fired before encoding the bitmap.
        /// </summary>
        event EventHandler<BitmapConverterEventArgs> Encoding;

        /// <summary>
        /// Event fired after encoding the bitmap.
        /// </summary>
        event EventHandler<BitmapConverterEventArgs> Encoded;
    }
}
