using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace Harvest.Bridge.Util
{
    public static class XmlUtil
    {
        /// <summary>
        /// Add Attribute
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="name">Name</param>
        /// <param name="value">Value to be set</param>
        public static XmlAttribute AddAttribute(XmlNode node, string name, string value)
        {
            XmlAttribute atr = null;
            if (node.NodeType == XmlNodeType.Document)
            {
                atr = ((XmlDocument)node).CreateAttribute(name);
                node = node.FirstChild;
            }
            else
            {
                atr = node.OwnerDocument.CreateAttribute(name);
            }
            atr.Value = value;
            node.Attributes.Append(atr);
            return atr;
        }

        /// <summary>
        /// Confirms attribute exists, if it does returns the value, if it does not returns default value or empty string.
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="name">Name</param>
        /// <param name="defaultValue">Default Value</param>
        public static string GetAttributeValue(XmlNode node, string name, string defaultValue)
        {
            string val = GetAttributeValue(node, name);
            if (val.Length == 0)
            {
                val = defaultValue;
            }

            return val;
        }

        /// <summary>
        /// Get Child Node Values By TagName as List
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="tagName">Tag Name</param>
        public static List<string> GetChildNodeValuesByTagNameAsList(XmlNode node, string tagName)
        {
            if (node != null)
            {
                return node.SelectSingleNode(tagName)?
                           .ChildNodes.Cast<XmlNode>()?
                           .Select(n => n.InnerText).ToList();
            }

            return null;
        }

        /// <summary>
        /// Sets the attribute value, if the attribute does not exist will create it, add to parent node then set value.
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="name">Name</param>
        /// <param name="value">Value to be set</param>
        public static void SetAttributeValue(XmlNode node, string name, string value)
        {
            if (node != null)
            {
                bool found = false;
                if (node.NodeType == XmlNodeType.Document)
                {
                    node = node.FirstChild;
                }

                try
                {
                    found = (node.Attributes.GetNamedItem(name) != null);
                }
                catch
                {
                }
                if (!found)
                {
                    XmlAttribute atr = node.OwnerDocument.CreateAttribute(name);
                    atr.Value = value;
                    node.Attributes.Append(atr);
                }
                else
                {
                    if (value.Length > 0)
                    {
                        node.Attributes.GetNamedItem(name).InnerText = value;
                    }
                    else
                    {
                        node.Attributes.Remove((XmlAttribute)node.Attributes.GetNamedItem(name));
                    }
                }
            }
        }

        /// <summary>
        /// Get Attribute Value as bool
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="name">Name</param>
        /// <param name="defaultValue">Default Value</param>
        public static bool GetAttributeValueAsBool(XmlNode node, string name, bool defaultValue = false)
        {
            bool valueAsBool;
            string value = GetAttributeValue(node, name, defaultValue.ToString());
            if (bool.TryParse(value, out valueAsBool))
            {
                // ignore
            }
            return valueAsBool;
        }

        /// <summary>
        /// Get Attribute Value as int
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="name">Name</param>
        /// <param name="defaultValue">Default Value</param>
        public static int GetAttributeValueAsInt(XmlNode node, string name, int defaultValue = -1)
        {
            int retValue;
            string value = GetAttributeValue(node, name, defaultValue.ToString());
            if (string.IsNullOrEmpty(value) || int.TryParse(value, out retValue) == false)
            {
                retValue = defaultValue;
            }
            return retValue;
        }


        /// <summary>
        /// Get Attribute Value
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="name">Name</param>
        public static string GetAttributeValue(XmlNode node, string name)
        {
            try
            {
                if (node != null && node.NodeType == XmlNodeType.Document)
                {
                    node = node.FirstChild;
                }

                if (node != null)
                {
                    if (node.Attributes != null)
                    {
                        if (node.Attributes.GetNamedItem(name) != null)
                        {
                            return node.Attributes.GetNamedItem(name).InnerText;
                        }
                    }
                }
            }
            catch
            {
            }
            return string.Empty;
        }

        /// <summary>
        /// Get Child Value
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="name">Name</param>
        public static string GetChildValue(XmlNode node, string name)
        {
            return GetChildValue(node, name, string.Empty);
        }

        /// <summary>
        /// Get Child Value
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="name">Name</param>
        /// <param name="defaultValue">Default Value</param>
        public static string GetChildValue(XmlNode node, string name, string defaultValue)
        {
            if (node != null && node[name] != null)
            {
                return node[name].InnerText;
            }
            return defaultValue;
        }

        /// <summary>
        /// Get Node Value
        /// </summary>
        /// <param name="node">Node</param>
        public static string GetNodeValue(XmlNode node)
        {
            return GetNodeValue(node, string.Empty);
        }

        /// <summary>
        /// Get Node Value
        /// </summary>
        /// <param name="node">Node </param>
        /// <param name="defaultValue">Default Value</param>
        public static string GetNodeValue(XmlNode node, string defaultValue)
        {
            if (node != null)
            {
                return node.InnerText;
            }
            return defaultValue;
        }

        /// <summary>
        /// Add Child Node
        /// </summary>
        /// <param name="node">Xml Node</param>
        /// <param name="name">Name</param>
        public static XmlNode AddChildNode(XmlNode node, string name)
        {
            XmlElement el;
            if (node.NodeType == XmlNodeType.Document)
            {
                el = ((XmlDocument)node).CreateElement(name);
            }
            else
            {
                el = node.OwnerDocument.CreateElement(name);
            }
            node.AppendChild(el);
            return el;
        }
    }
}
