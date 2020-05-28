using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FileLoader
{
    public class DataManager
    {
        public DataManager() { }

        public DataSet dataSet = new DataSet();

        public void Save(string dsfile, string destination)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataSet));
            dataSet.Destination = destination;

            using (XmlWriter writer = XmlWriter.Create(dsfile, new XmlWriterSettings() { Indent = true }))
            {
                serializer.Serialize(writer, dataSet);
            }
        }

        public void Load(string dsfile)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataSet));

            if (File.Exists(dsfile))
            {
                using (Stream stream = new FileStream(dsfile, FileMode.Open))
                {
                    dataSet = (DataSet)serializer.Deserialize(stream);
                }
            }
        }
    }

    [Serializable]
    public class DataSet
    {
        public DataSet() { }
        public DataSet(List<FileData> fdlist, string dest)
        {
            this.FileDataList = fdlist;
            this.Destination = dest;
        }

        public List<FileData> FileDataList = new List<FileData>();
        public string Destination { get; set; }
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
