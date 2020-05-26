using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace FileLoader
{
    [Serializable]
    [XmlRoot(ElementName = "SavedData")]
    public class DataManager
    {
        public DataManager()  {  }

        [XmlElement(ElementName = "SavedDestination")]
        public string CopyDestination { get; set; }

        [XmlArrayItem("SavedFiles")]
        public List<FileData> filesData = new List<FileData>();

        public void Save()
        {

        }

        public void Load()
        {

        }
    }

    [Serializable]
    public class FileData
    {
        public FileData(string filename, string filedirectory)
        {
            this.fileDirectory = filedirectory;
            this.fileName = filename;
        }

        public string fileDirectory { get; set; }
        public string fileName { get; set; }
    }

}
