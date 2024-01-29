using System.Xml.Linq;
using System.Xml.Serialization;
using XmlHelper.XmlFileProcessor.IpsXmlElements;

namespace XmlHelper.XmlFileProcessor
{
    public class XmlFileCreator
    {
        public void CreateXmlFile()
        {
            new XDocument(
                new XElement("root",
                new XElement("someNode", "someValue")
                )
            ).Save("foo.xml");
        }

        public void CreateXmlFile(string fileName, MetaDataBrief metaDataBrief) 
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MetaDataBrief));

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, metaDataBrief);
            }
        }

        public void CreateXmlFile(string fileName, Relations relations)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Relations));

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, relations);
            }
        }

        public void CreateXmlFile(string fileName, Objects objects)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Objects));

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, objects);
            }
        }
    }
}
