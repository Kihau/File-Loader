using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FileLoader
{
    public class DataManager
    {
        public DataManager()  {  }
        public string CopyDestination { get; set; }

        public List<FileData> filesData = new List<FileData>();

        public void Save(string destination)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataSet));
            DataSet dataSet = new DataSet(filesData, destination);

            using (XmlWriter writer = XmlWriter.Create("fdata.xml", new XmlWriterSettings() { Indent = true }))
            {
                serializer.Serialize(writer, dataSet);
            }
        }

        public void Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataSet));

            if (File.Exists("fdata.xml"))
            {
                using (Stream stream = new FileStream("fdata.xml", FileMode.Open))
                {
                    DataSet dataSet = (DataSet)serializer.Deserialize(stream);
                    this.CopyDestination = dataSet.CopyDestination;
                    this.filesData = dataSet.FilesData;
                }
            }
        }
    }

    [Serializable]
    public class DataSet
    {
        public DataSet() { }
        public DataSet(List<FileData> fd, string cd) 
        {
            this.FilesData = fd;
            this.CopyDestination = cd;
        }

        public List<FileData> FilesData { get; set; }
        public string CopyDestination { get; set; }
    }

    [Serializable]
    public class FileData
    {
        public FileData() { }
        public FileData(string filename, string filedirectory)
        {
            this.fileDirectory = filedirectory;
            this.fileName = filename;
        }

        public string fileDirectory { get; set; }
        public string fileName { get; set; }
    }

}
