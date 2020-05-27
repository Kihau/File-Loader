using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FileLoader
{
    public class DataManager
    {
        public DataManager()  {  }

        //public string CopyDestination { get; set; }
        //public List<FileData> filesData = new List<FileData>();

        public DataSet dataSet = new DataSet();

        public void Save(string dsfile, string destination)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataSet));
            dataSet.CopyDestination = destination;

            using (XmlWriter writer = XmlWriter.Create(dsfile, new XmlWriterSettings() { Indent = true }))
                serializer.Serialize(writer, dataSet);
        }

        public void Load(string dsfile)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataSet));

            if (File.Exists(dsfile))
            {
                using (Stream stream = new FileStream(dsfile, FileMode.Open))
                    dataSet = (DataSet)serializer.Deserialize(stream);
            }
        }
    }

    [Serializable]
    public class DataSet
    {
        public DataSet() { }
        public DataSet(List<FileData> fd, string cd) 
        {
            this.FileDataList = fd;
            this.CopyDestination = cd;
        }

        public List<FileData> FileDataList = new List<FileData>();
        public string CopyDestination { get; set; }
    }

    [Serializable]
    public class FileData
    {
        public FileData() { }
        public FileData(string filename, string filedirectory)
        {
            this.FileDirectory = filedirectory;
            this.FileName = filename;
        }

        public string FileDirectory { get; set; }
        public string FileName { get; set; }
    }

}
