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
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Perspective.Core
{
    /// <summary>
    /// An XML serializable dictionary class, compatible with Isolated Storage in partial-trust mode.
    /// </summary>
    [Serializable()]
    public class StorableDictionary : Dictionary<string, object>, IXmlSerializable
    {
        private const string _rootElementName = "StorableDictionary";
        private const string _entryElementName = "Entry";
        private const string _keyAttributeName = "Key";

        #region IXmlSerializable Members

        /// <summary>
        /// Implements IXmlSerializable.GetSchema()
        /// </summary>
        /// <returns></returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The XmlReader stream from which the object is deserialized.</param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            this.Clear();
            XmlSerializer xs = new XmlSerializer(typeof(object));

            reader.ReadStartElement(_rootElementName);
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                string value = reader.GetAttribute(_keyAttributeName);
                reader.ReadStartElement(_entryElementName);
                string xml = reader.ReadOuterXml();
                if (!String.IsNullOrEmpty(value))
                {
                    StringReader sr = new StringReader(xml);
                    XmlTextReader xr = new XmlTextReader(sr);
                    object o = xs.Deserialize(xr);
                    this.Add(value, o);
                }
                reader.ReadEndElement();
            }

            reader.ReadEndElement();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The XmlWriter stream to which the object is serialized.</param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            
            XmlSerializer xs = new XmlSerializer(typeof(object));

            foreach (KeyValuePair<string, object> kvp in this)
            {
                writer.WriteStartElement(_entryElementName);
                writer.WriteAttributeString(_keyAttributeName, kvp.Key);
                xs.Serialize(writer, kvp.Value, ns);
                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
