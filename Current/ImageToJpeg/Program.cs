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
using System.Threading;

namespace ImageToJpeg
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "fr")
                {
                    Console.WriteLine("Convertit les fichiers image d'un dossier en fichiers JPEG.");
                    Console.WriteLine("Arguments : dossierOriginal dossierConverti [qualité] [ow:0/1] [recurs:0/1]");
                    Console.WriteLine("  - dossierOriginal : chemin du dossier contenant les fichiers originaux");
                    Console.WriteLine("  - dossierConverti : chemin du dossier contenant les fichiers convertis (dossier créé si inexistant)");
                    Console.WriteLine("  - qualité : niveau de qualité JPEG (1 à 100 - 75 par défaut)");
                    Console.WriteLine("  - ow:1 écrase les fichiers convertis existants (valeur par défaut ow:0) - Attention à cette option si vos fichiers originaux sont des fichiers JPEG !");
                    Console.WriteLine("  - recurs:1 : traite les sous-répertoires récursivement (valeur par défaut recurs:0)");
                    Console.WriteLine("");
                    Console.WriteLine(@"Exemple : ImageToJpeg.exe Photos\RAW Photos\JPEG 80 ow:1 recurs:1");
                    Console.WriteLine("");
                    Console.WriteLine("Dernière version et informations de licence : voir http://www.codeplex.com/perspective");
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("Converts the image files of a folder into JPEG files.");
                    Console.WriteLine("Arguments : originalPath convertedPath [quality] [ow:0/1] [recurs:0/1]");
                    Console.WriteLine("  - originalPath : path of the folder containing the original files");
                    Console.WriteLine("  - convertedPath : path of the folder containing the converted files (this folder is created if necessary)");
                    Console.WriteLine("  - quality : JPEG quality level (1 to 100 - 75 by default)");
                    Console.WriteLine("  - ow:1 : overwrite existing converted files (ow:0 by default) - Be carefull with this option if your original files are JPEG files !");
                    Console.WriteLine("  - recurs:1 : process recursively the subfolders (recurs:0 by default)");
                    Console.WriteLine("");
                    Console.WriteLine(@"Example : ImageToJpeg.exe Photos\RAW Photos\JPEG 80 ow:1 recurs:1");
                    Console.WriteLine("");
                    Console.WriteLine("Last release and license informations : see http://www.codeplex.com/perspective");
                    Console.WriteLine("");
                }
                return;
            }
            string originalPath = args[0];
            string convertedPath = args[1];

            int quality = 75;
            bool overwrite = false;
            bool recursive = false;
            for (int i = 2; i < args.Length; i++)
            {
                if (String.Equals(args[i], "ow:1",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    overwrite = true;
                }
                else if (String.Equals(args[i], "recurs:1",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    recursive = true;
                }
                else
                {
                    quality = Convert.ToInt32(args[i]);
                }
            }

            //int quality = 75;
            //if (args.Length >= 3)
            //{
            //    quality = Convert.ToInt32(args[2]);
            //}
            //bool overwrite = false;
            //if (args.Length >= 4)
            //{
            //    overwrite = String.Equals(args[3], "ow:1", 
            //        StringComparison.InvariantCultureIgnoreCase);
            //}
            //bool recursive = false;
            //if (args.Length >= 5)
            //{
            //    recursive = String.Equals(args[4], "recurs:1", 
            //        StringComparison.InvariantCultureIgnoreCase);
            //}

            BitmapToJpegConverter btjc = new BitmapToJpegConverter();
            btjc.QualityLevel = quality;
            btjc.Encoding += (sender, e) =>
            {
                Console.Write("{0}", e.OriginalFilename);
            };
            btjc.Encoded += (sender, e) =>
            {
                Console.WriteLine(" => {0} ", e.ConvertedFilename);
            };
            btjc.ConvertFiles(originalPath, convertedPath, recursive, overwrite);

            if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "fr")
            {
                Console.WriteLine("Conversion terminée.");
            }
            else
            {
                Console.WriteLine("Conversion done.");
            }
            return;
        }
    }
}
