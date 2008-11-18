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
using System.Collections;
using System.IO.IsolatedStorage;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Deployment.Application;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;

namespace Perspective.Core
{
    /// <summary>
    /// A helper class for isolated storage operations, like dictionary storage.
    /// </summary>
    public static class IsolatedStorageHelper
    {
        /// <summary>
        /// Fills a dictionary with the values stored in an isolated storage corresponding to the kind of application (installed or ClickOnce deployed).
        /// </summary>
        /// <param name="dictionary">The dictionary to fill.</param>
        /// <param name="fileName">The file name.</param>
        public static void Load(IDictionary dictionary, string fileName)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                IsolatedStorageHelper.LoadFromUserStoreForApplication(
                    dictionary,
                    fileName);
            }
            else
            {
                IsolatedStorageHelper.LoadFromUserStoreForDomain(
                    dictionary,
                    fileName);
            }
        }

        /// <summary>
        /// Fills a dictionary with the values stored in an isolated storage corresponding to the kind of application (installed or ClickOnce deployed).
        /// </summary>
        /// <param name="sd">The dictionary to fill.</param>
        /// <param name="fileName">The file name.</param>
        public static void Load(StorableDictionary sd, string fileName)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                IsolatedStorageHelper.LoadFromUserStoreForApplication(
                    sd,
                    fileName);
            }
            else
            {
                IsolatedStorageHelper.LoadFromUserStoreForDomain(
                    sd,
                    fileName);
            }
        }

        /// <summary>
        /// Save a dictionary into an isolated storage corresponding to the kind of application (installed or ClickOnce deployed).
        /// This method is not compatible with partial-trust in a ClickOnce application.
        /// </summary>
        /// <param name="dictionary">The dictionary to save.</param>
        /// <param name="fileName">The file name.</param>
        public static void Save(IDictionary dictionary, string fileName)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                IsolatedStorageHelper.SaveToUserStoreForApplication(
                    dictionary,
                    fileName);
            }
            else
            {
                IsolatedStorageHelper.SaveToUserStoreForDomain(
                    dictionary,
                    fileName);
            }
        }

        /// <summary>
        /// Save a dictionary into an isolated storage corresponding to the kind of application (installed or ClickOnce deployed).
        /// </summary>
        /// <param name="sd">The dictionary to save.</param>
        /// <param name="fileName">The file name.</param>
        public static void Save(StorableDictionary sd, string fileName)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                IsolatedStorageHelper.SaveToUserStoreForApplication(
                    sd,
                    fileName);
            }
            else
            {
                IsolatedStorageHelper.SaveToUserStoreForDomain(
                    sd,
                    fileName);
            }
        }

        /// <summary>
        /// Fills a dictionary with the values stored in an user-scoped isolated storage corresponding to the calling code's application identity.
        /// This requires a ClickOnce aplication (see http://blogs.msdn.com/shawnfa/archive/2006/01/18/514407.aspx).
        /// First, the dictionary is emptied.
        /// </summary>
        /// <param name="dictionary">The dictionary to fill.</param>
        /// <param name="fileName">The file name.</param>
        public static void LoadFromUserStoreForApplication(IDictionary dictionary, string fileName)
        {
            LoadForApplication(dictionary, 
                IsolatedStorageScope.Application | 
                IsolatedStorageScope.User, 
                fileName);
        }

        /// <summary>
        /// Fills a dictionary with the values stored in an user-scoped isolated storage corresponding to the calling code's application identity.
        /// This requires a ClickOnce aplication (see http://blogs.msdn.com/shawnfa/archive/2006/01/18/514407.aspx).
        /// First, the dictionary is emptied.
        /// </summary>
        /// <param name="sd">The dictionary to fill.</param>
        /// <param name="fileName">The file name.</param>
        public static void LoadFromUserStoreForApplication(StorableDictionary sd, string fileName)
        {
            LoadForApplication(sd,
                IsolatedStorageScope.Application |
                IsolatedStorageScope.User,
                fileName);
        }

        /// <summary>
        /// Saves a dictionary in an user-scoped isolated storage corresponding to the calling code's application identity.
        /// This requires a ClickOnce aplication (see http://blogs.msdn.com/shawnfa/archive/2006/01/18/514407.aspx).
        /// This method is not compatible with partial-trust in a ClickOnce application.
        /// </summary>
        /// <param name="dictionary">The dictionary to save.</param>
        /// <param name="fileName">The file name.</param>
        public static void SaveToUserStoreForApplication(IDictionary dictionary, string fileName)
        {
            SaveForApplication(dictionary, 
                IsolatedStorageScope.Application | 
                IsolatedStorageScope.User, 
                fileName);
        }

        /// <summary>
        /// Saves a dictionary in an user-scoped isolated storage corresponding to the calling code's application identity.
        /// This requires a ClickOnce aplication (see http://blogs.msdn.com/shawnfa/archive/2006/01/18/514407.aspx).
        /// </summary>
        /// <param name="sd">The dictionary to save.</param>
        /// <param name="fileName">The file name.</param>
        public static void SaveToUserStoreForApplication(StorableDictionary sd, string fileName)
        {
            SaveForApplication(sd,
                IsolatedStorageScope.Application |
                IsolatedStorageScope.User,
                fileName);
        }


        /// <summary>
        /// Deletes a file in an user-scoped isolated storage corresponding to the calling code's application identity.
        /// This requires a ClickOnce aplication (see http://blogs.msdn.com/shawnfa/archive/2006/01/18/514407.aspx).
        /// </summary>
        /// <param name="fileName">The relative path of the file to delete within the isolated storage scope.</param>
        public static void DeleteFileFromUserStoreForApplication(string fileName)
        {
            DeleteFileForApplication(
                IsolatedStorageScope.Application |
                IsolatedStorageScope.User,
                fileName);
        }

        /// <summary>
        /// Deletes a directory in an user-scoped isolated storage corresponding to the calling code's application identity.
        /// This requires a ClickOnce aplication (see http://blogs.msdn.com/shawnfa/archive/2006/01/18/514407.aspx).
        /// </summary>
        /// <param name="dir">The relative path of the directory to delete within the isolated storage scope.</param>
        public static void DeleteDirectoryFromUserStoreForApplication(string dir)
        {
            DeleteFileForApplication(
                IsolatedStorageScope.Application |
                IsolatedStorageScope.User,
                dir);
        }

        /// <summary>
        /// Fills a dictionary with the values stored in an user-scoped isolated storage corresponding to the application domain identity and Perspective.Core assembly identity.
        /// First, the dictionary is emptied.
        /// </summary>
        /// <param name="dictionary">The dictionary to fill.</param>
        /// <param name="fileName">The file name.</param>
        public static void LoadFromUserStoreForDomain(IDictionary dictionary, string fileName)
        {
            LoadForDomain(dictionary, 
                IsolatedStorageScope.Assembly |
                IsolatedStorageScope.Domain | 
                IsolatedStorageScope.User, 
                fileName);
        }

        /// <summary>
        /// Fills a dictionary with the values stored in an user-scoped isolated storage corresponding to the application domain identity and Perspective.Core assembly identity.
        /// First, the dictionary is emptied.
        /// </summary>
        /// <param name="sd">The dictionary to fill.</param>
        /// <param name="fileName">The file name.</param>
        public static void LoadFromUserStoreForDomain(StorableDictionary sd, string fileName)
        {
            LoadForDomain(sd,
                IsolatedStorageScope.Assembly |
                IsolatedStorageScope.Domain |
                IsolatedStorageScope.User,
                fileName);
        }

        /// <summary>
        /// Saves a dictionary in an user-scoped isolated storage corresponding to the application domain identity and Perspective.Core assembly identity.
        /// This method is not compatible with partial-trust.
        /// </summary>
        /// <param name="dictionary">The dictionary to save.</param>
        /// <param name="fileName">The file name.</param>
        public static void SaveToUserStoreForDomain(IDictionary dictionary, string fileName)
        {
            SaveForDomain(dictionary, 
                IsolatedStorageScope.Assembly |
                IsolatedStorageScope.Domain | 
                IsolatedStorageScope.User, 
                fileName);
        }

        /// <summary>
        /// Saves a dictionary in an user-scoped isolated storage corresponding to the application domain identity and Perspective.Core assembly identity.
        /// </summary>
        /// <param name="sd">The dictionary to save.</param>
        /// <param name="fileName">The file name.</param>
        public static void SaveToUserStoreForDomain(StorableDictionary sd, string fileName)
        {
            SaveForDomain(sd,
                IsolatedStorageScope.Assembly |
                IsolatedStorageScope.Domain |
                IsolatedStorageScope.User,
                fileName);
        }

        /// <summary>
        /// Deletes a file in an user-scoped isolated storage corresponding to the application domain identity and Perspective.Core assembly identity.
        /// </summary>
        /// <param name="fileName">The relative path of the file to delete within the isolated storage scope.</param>
        public static void DeleteFileFromUserStoreForDomain(string fileName)
        {
            DeleteDirectoryForDomain(
                IsolatedStorageScope.Assembly |
                IsolatedStorageScope.Domain |
                IsolatedStorageScope.User,
                fileName);
        }

        /// <summary>
        /// Deletes a directory in an user-scoped isolated storage corresponding to the application domain identity and Perspective.Core assembly identity.
        /// </summary>
        /// <param name="dir">The relative path of the directory to delete within the isolated storage scope.</param>
        public static void DeleteDirectoryFromUserStoreForDomain(string dir)
        {
            DeleteDirectoryForDomain(
                IsolatedStorageScope.Assembly |
                IsolatedStorageScope.Domain |
                IsolatedStorageScope.User,
                dir);
        }

        /// <summary>
        /// Fills a dictionary with the values stored in an isolated store,
        /// for application domain and assembly.
        /// </summary>
        /// <param name="dictionary">The dictionary to fill.</param>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="fileName">The file name.</param>
        public static void LoadForDomain(IDictionary dictionary, IsolatedStorageScope scope, string fileName)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null, null))
            {
                LoadFromStorageFile(storage, dictionary, fileName);
            }
        }

        /// <summary>
        /// Fills a dictionary with the values stored in an isolated store,
        /// for application domain and assembly.
        /// </summary>
        /// <param name="sd">The dictionary to fill.</param>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="fileName">The file name.</param>
        public static void LoadForDomain(StorableDictionary sd, IsolatedStorageScope scope, string fileName)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null, null))
            {
                LoadFromStorageFile(storage, sd, fileName);
            }
        }

        /// <summary>
        /// Fills a dictionary with the values stored in an isolated store,
        /// for application.
        /// </summary>
        /// <param name="dictionary">The dictionary to fill.</param>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="fileName">The file name.</param>
        public static void LoadForApplication(IDictionary dictionary, IsolatedStorageScope scope, string fileName)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null))
            {
                LoadFromStorageFile(storage, dictionary, fileName);
            }
        }

        /// <summary>
        /// Fills a dictionary with the values stored in an isolated store,
        /// for application.
        /// </summary>
        /// <param name="sd">The dictionary to fill.</param>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="fileName">The file name.</param>
        public static void LoadForApplication(StorableDictionary sd, IsolatedStorageScope scope, string fileName)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null))
            {
                LoadFromStorageFile(storage, sd, fileName);
            }
        }

        private static void LoadFromStorageFile(
            IsolatedStorageFile storage, 
            IDictionary dictionary, 
            string fileName)
        {
            string[] files = storage.GetFileNames(fileName);
            if ((files.Length > 0) && (files[0] == fileName))
            {
                using (Stream stream =
                    new IsolatedStorageFileStream(fileName, FileMode.Open, storage))
                {
                    if (stream.Length > 0)
                    {
                        IFormatter formatter = new BinaryFormatter();
                        IDictionary data = (IDictionary)formatter.Deserialize(stream);
                        IDictionaryEnumerator enumerator = data.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            dictionary.Add(enumerator.Key, enumerator.Value);
                        }
                    }
                }
            }
        }

        private static void LoadFromStorageFile(
            IsolatedStorageFile storage, 
            StorableDictionary sd, 
            string fileName)
        {
            string[] files = storage.GetFileNames(fileName);
            if ((files.Length > 0) && (files[0] == fileName))
            {
                using (Stream stream =
                    new IsolatedStorageFileStream(fileName, FileMode.Open, storage))
                {
                    if (stream.Length > 0)
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(StorableDictionary));
                        StorableDictionary sd2 = (StorableDictionary)xs.Deserialize(stream); //, sd);
                        foreach (KeyValuePair<string, object> kvp in sd2)
                        {
                            sd.Add(kvp.Key, kvp.Value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves a dictionary in an isolated store.
        /// for application domain and assembly.
        /// This method is not compatible with partial-trust.
        /// </summary>
        /// <param name="dictionary">The dictionary to save.</param>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="fileName">The file name.</param>
        public static void SaveForDomain(IDictionary dictionary, IsolatedStorageScope scope, string fileName)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null, null);
            SaveToStorageFile(storage, dictionary, fileName);
        }

        /// <summary>
        /// Saves a dictionary in an isolated store.
        /// for application domain and assembly.
        /// </summary>
        /// <param name="sd">The dictionary to save.</param>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="fileName">The file name.</param>
        public static void SaveForDomain(StorableDictionary sd, IsolatedStorageScope scope, string fileName)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null, null);
            SaveToStorageFile(storage, sd, fileName);
        }

        /// <summary>
        /// Saves a dictionary in an isolated store
        /// for application.
        /// This method is not compatible with partial-trust.
        /// </summary>
        /// <param name="dictionary">The dictionary to save.</param>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="fileName">The file name.</param>
        public static void SaveForApplication(IDictionary dictionary, IsolatedStorageScope scope, string fileName)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null);
            SaveToStorageFile(storage, dictionary, fileName);
        }

        /// <summary>
        /// Saves a dictionary in an isolated store
        /// for application.
        /// </summary>
        /// <param name="sd">The dictionary to save.</param>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="fileName">The file name.</param>
        public static void SaveForApplication(StorableDictionary sd, IsolatedStorageScope scope, string fileName)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null);
            SaveToStorageFile(storage, sd, fileName);
        }

        // Binary serialization is not compatible with partial-trust
        private static void SaveToStorageFile(
            IsolatedStorageFile storage, 
            IDictionary dictionary, 
            string fileName)
        {
            using (IsolatedStorageFileStream stream =
                new IsolatedStorageFileStream(fileName, FileMode.Create, storage))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, dictionary);
            }
        }

        private static void SaveToStorageFile(
            IsolatedStorageFile storage, 
            StorableDictionary sd, 
            string fileName)
        {
            using (IsolatedStorageFileStream stream =
                new IsolatedStorageFileStream(fileName, FileMode.Create, storage))
            {
                XmlSerializer xs = new XmlSerializer(typeof(StorableDictionary));
                xs.Serialize(stream, sd);
            }
        }

        /// <summary>
        /// Deletes a file in the isolated storage scope
        /// for application domain and assembly.
        /// </summary>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="fileName">The relative path of the file to delete within the isolated storage scope.</param>
        public static void DeleteFileForDomain(IsolatedStorageScope scope, string fileName)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null, null);
            storage.DeleteFile(fileName);
        }

        /// <summary>
        /// Deletes a directory in the isolated storage scope
        /// for application domain and assembly.
        /// </summary>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="dir">The relative path of the directory to delete within the isolated storage scope.</param>
        public static void DeleteDirectoryForDomain(IsolatedStorageScope scope, string dir)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null, null);
            storage.DeleteDirectory(dir);
        }

        /// <summary>
        /// Deletes a file in the isolated storage scope
        /// for application.
        /// </summary>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="fileName">The relative path of the file to delete within the isolated storage scope.</param>
        public static void DeleteFileForApplication(IsolatedStorageScope scope, string fileName)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null);
            storage.DeleteFile(fileName);
        }

        /// <summary>
        /// Deletes a directory in the isolated storage scope
        /// for application.
        /// </summary>
        /// <param name="scope">The degree of scope for the isolated store : a bitwise combination of the IsolatedStorageScope values.</param>
        /// <param name="dir">The relative path of the directory to delete within the isolated storage scope.</param>
        public static void DeleteDirectoryForApplication(IsolatedStorageScope scope, string dir)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetStore(scope, null);
            storage.DeleteDirectory(dir);
        }
    }
}
