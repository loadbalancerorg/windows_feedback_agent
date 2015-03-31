using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace LBCPUMon
{
    
    public class Xmlconfig : IDisposable
    {
        private XmlDocument xmldoc;
        private string originalFile;
        private bool commitonunload = true;
        private bool cleanuponsave = false;


        public Xmlconfig()
        {
            xmldoc = new XmlDocument();
            LoadXmlFromString("<xml></xml>");
        }

        public Xmlconfig(string loadfromfile, bool create)
        {
            xmldoc = new XmlDocument();
            LoadXmlFromFile(loadfromfile,create);
        }

        public bool ValidateXML(bool silent)
        {
            if (!Settings.Validate())
            {
                if (silent)
                    return false;
                else
                    throw new Exception("This is not a valid configuration xml file! Probably duplicate children with the same names, or non-alphanumerical tagnames!");
            }
            else
                return true;
        }

        public void Clean()
        {
            Settings.Clean();
        }

        public bool CleanUpOnSave
        {
            get { return cleanuponsave; }
            set { cleanuponsave = value; }            
        }
        
        public bool CommitOnUnload
        {
            get { return commitonunload; }
            set { commitonunload = value; }
        }

        public void Dispose()
        {
            if (CommitOnUnload) Commit();
        }

        public void LoadXmlFromFile(string filename, bool create)
        {
            if (CommitOnUnload) Commit();
            try
            {
                xmldoc.Load(filename);
            }
            catch
            {
                if (!create)
                    throw new Exception("xmldoc.Load() failed! Probably file does NOT exist!");
                else
                {
                    xmldoc.LoadXml("<xml></xml>");
                    Save(filename);
                }
            }
            ValidateXML(false);
            originalFile = filename;

        }

        public void LoadXmlFromFile(string filename)
        {
            LoadXmlFromFile(filename, false);
        }

        public void LoadXmlFromString(string xml)
        {
            if (CommitOnUnload) Commit();
            xmldoc.LoadXml(xml);
            originalFile = null;
            ValidateXML(false);
        }

        public void NewXml(string rootelement)
        {
            if (CommitOnUnload) Commit();
            LoadXmlFromString(String.Format("<{0}></{0}>",rootelement));
        }

        public void Save(string filename)
        {
            ValidateXML(false);
            if (CleanUpOnSave) { Clean(); }
            try
            {
                xmldoc.Save(filename);
                originalFile = filename;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();

            }
        }

        public void Save(System.IO.Stream stream)
        {
            ValidateXML(false);
            if (CleanUpOnSave)
                Clean();
            xmldoc.Save(stream);
        }

        public bool Commit()
        {
            if (originalFile != null) { Save(originalFile); return true; } else { return false; }
        }

        public bool Reload()
        {
            if (originalFile != null) { LoadXmlFromFile(originalFile); return true; } else { return false; }
        }

        public ConfigSetting Settings
        {
            get { return new ConfigSetting(xmldoc.DocumentElement); }           
        }

    }
}
