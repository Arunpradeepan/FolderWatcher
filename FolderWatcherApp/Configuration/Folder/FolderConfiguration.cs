using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FolderWatcherApp.Configuration.Folder
{
    [Serializable]
    public class FolderConfiguration
    {
        private static object _lock = new object();

        [XmlIgnore]
        public string FullPath { get; set; }
        public bool Enabled { get; set; }
        public string Title { get; set; }
        public List<FolderItemConfiguration> Folders { get; set; }

        public FolderConfiguration()
        {
            Folders = new List<FolderItemConfiguration>();
        }

        public static FolderConfiguration Deserialize(string path)
        {
            lock (_lock)
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(FolderConfiguration));
                using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
                {
                    FolderConfiguration ret = ((FolderConfiguration)serializer.Deserialize(reader));
                    ret.FullPath = path;
                    return ret;
                }
            }
        }

        public void Save()
        {
            lock (_lock)
            {
                if (string.IsNullOrWhiteSpace(FullPath))
                    FullPath = ConfigurationData.ConfiurationFolder + System.IO.Path.DirectorySeparatorChar + Title + ".xml";

                if (System.IO.File.Exists(FullPath))
                    System.IO.File.Delete(FullPath);

                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(FolderConfiguration));
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(FullPath))
                    serializer.Serialize(writer, this);
            }
        }

        public void Delete()
        {
            lock (_lock)
            {
                if (string.IsNullOrWhiteSpace(FullPath))
                    FullPath = ConfigurationData.ConfiurationFolder + System.IO.Path.DirectorySeparatorChar + Title + ".xml";

                if (System.IO.File.Exists(FullPath))
                    System.IO.File.Delete(FullPath);
            }
        }
    }
}
