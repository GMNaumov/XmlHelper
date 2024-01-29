using System.IO.Compression;
using XmlHelper.XmlFileProcessor;
using XmlHelper.XmlFileProcessor.IpsXmlElements;

namespace XmlHelper
{
    public class Program
    {
        static void Main(string[] args)
        {
            XmlFileCreator creator = new XmlFileCreator();

            creator.CreateXmlFile("MetaDataBrief.xml", new MetaDataBrief());
            creator.CreateXmlFile("Objects.xml", new Objects());
            creator.CreateXmlFile("Relations.xml", new Relations());


            var zipFile = ZipFile.Open("import.zip", ZipArchiveMode.Create);
        }
    }
}
