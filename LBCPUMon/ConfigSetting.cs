using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace LBCPUMon
{
    /// <summary>
    /// Represents a Configuration Node in the XML file
    /// </summary>
    public class ConfigSetting
    {
        private XmlNode node;

        private ConfigSetting()
        {
            throw new Exception("Cannot be created directly. Needs a node parameter");
        }

        public ConfigSetting(XmlNode node)
        {
            if (node == null)
                throw new Exception("Node parameter can NOT be null!");
            this.node = node;
        }

        public string Name
        {
            get
            {
                return node.Name;
            }
        }

        public int ChildCount(bool unique)
        {
            IList<string> names = ChildrenNames(unique);
            if (names != null)
                return names.Count;
            else
                return 0;
        }

        public IList<String> ChildrenNames(bool unique)
        {

            if (node.ChildNodes.Count == 0)
                return null;
            List<String> stringlist = new List<string>();

            foreach (XmlNode achild in node.ChildNodes)
            {
                string name = achild.Name;
                if ((!unique) || (!stringlist.Contains(name)))
                    stringlist.Add(name);
            }
            
            stringlist.Sort();
            return stringlist;
        }

        public IList<ConfigSetting> Children()
        {
            if (ChildCount(false) == 0)
                return null;
            List<ConfigSetting> list = new List<ConfigSetting>();

            foreach (XmlNode achild in node.ChildNodes)
            {
                list.Add(new ConfigSetting(achild));
            }
            return list;
        }

        public IList<ConfigSetting> GetNamedChildren(String name)
        {
            foreach (Char c in name)
                if (!Char.IsLetterOrDigit(c))
                    throw new Exception("Name MUST be alphanumerical!");
            XmlNodeList xmlnl = node.SelectNodes(name);
            int NodeCount = xmlnl.Count;
            List<ConfigSetting> css = new List<ConfigSetting>();
            foreach (XmlNode achild in xmlnl)
            {
                css.Add(new ConfigSetting(achild));
            }
            return css;
        }

        public int GetNamedChildrenCount(String name)
        {
            foreach (Char c in name)
                if (!Char.IsLetterOrDigit(c))
                    throw new Exception("Name MUST be alphanumerical!");
            return node.SelectNodes(name).Count;
        }

        public string Value
        {
            get
            {
                XmlNode xmlattrib = node.Attributes.GetNamedItem("value");
                if (xmlattrib != null) return xmlattrib.Value; else return "";
            }

            set
            {
                XmlNode xmlattrib = node.Attributes.GetNamedItem("value");
                if (value != "")
                {
                    if (xmlattrib == null) xmlattrib = node.Attributes.Append(node.OwnerDocument.CreateAttribute("value"));
                    xmlattrib.Value = value;
                }
                else if (xmlattrib != null) node.Attributes.RemoveNamedItem("value");
            }
        }

        public int intValue
        {
            get { int i; int.TryParse(Value, out i); return i; }
            set { Value = value.ToString(); }
            
        }

        public bool boolValue
        {
            get { bool b; bool.TryParse(Value, out b); return b; }
            set { Value = value.ToString(); }
        }

        public float floatValue
        {
            get { float f; float.TryParse(Value, out f); return f; }
            set { Value = value.ToString(); }

        }

        public ConfigSetting this[string path]
        {
            get
            {
                char[] separators = { '/', '\\' };
                path.Trim(separators);
                String[] pathsection = path.Split(separators);
                                
                XmlNode selectednode = node;
                XmlNode newnode;

                foreach (string asection in pathsection)
                {
                    String nodename, nodeposstr;
                    int nodeposition;
                    int indexofdiez = asection.IndexOf('#');

                    if (indexofdiez == -1) // No position defined, take the first one by default
                    {
                        nodename = asection;
                        nodeposition = 1;
                    }
                    else
                    {
                        nodename = asection.Substring(0, indexofdiez); // Node name is before the diez character
                        nodeposstr = asection.Substring(indexofdiez + 1);
                        if (nodeposstr == "#") // Double diez means he wants to create a new node
                            nodeposition = GetNamedChildrenCount(nodename) + 1;
                        else
                            nodeposition = int.Parse(nodeposstr);
                    }

                    // Verify name
                    foreach (char c in nodename)
                    { if ((!Char.IsLetterOrDigit(c))) return null; }

                    String transformedpath = String.Format("{0}[{1}]", nodename, nodeposition);
                    newnode = selectednode.SelectSingleNode(transformedpath);

                    while (newnode == null)
                    {
                        XmlElement newelement = selectednode.OwnerDocument.CreateElement(nodename);
                        selectednode.AppendChild(newelement);
                        newnode = selectednode.SelectSingleNode(transformedpath);
                    }
                    selectednode = newnode;
                }

                return new ConfigSetting(selectednode);
            }
        }

        public bool Validate()
        {
            foreach (Char c in this.Name)
                if (!Char.IsLetterOrDigit(c))
                    return false;

            if (ChildCount(false) == 0)
                return true;
            else
            {
                foreach (ConfigSetting cs in this.Children())
                {
                    if (!cs.Validate())
                        return false;
                }
            }
            return true;
        }

        public void Clean()
        {
            if (ChildCount(false) != 0)
                foreach (ConfigSetting cs in this.Children())
                {
                    cs.Clean();
                }
            if ((ChildCount(false) == 0) && (this.Value == ""))
                this.Remove();            
        }
        
        public void Remove()
        {
            if (node.ParentNode == null) return;
            node.ParentNode.RemoveChild(node);        
        }

        public void RemoveChildren()
        {
            node.RemoveAll();
        }


    }
}
