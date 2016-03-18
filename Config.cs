using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace WebsiteXMLImport
{
    public class Config
    {
        static string ConfigFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WebsiteImport.xml");

        private string _Username;
        private string _Password;
        private string _Company;

        public string Username 
        { 
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
            }
        }
        public string Password 
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }
        public string Company
        {
            get
            {
                return _Company;
            }
            set
            {
                _Company = value;
            }
        }

        public void Save()
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            XmlSerializer serializer = new XmlSerializer(typeof(Config));

            using (TextWriter writer = new StreamWriter(ConfigFile))
            {
                serializer.Serialize(writer, this, ns);
            }
        }

        public static Config Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            Config oConfig = new Config();

            using (FileStream fs = new FileStream(ConfigFile, FileMode.Open))
            {
                oConfig = (Config)serializer.Deserialize(fs);
                return oConfig;
            }
        }


    }


}
