namespace XmlHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExampleSystemXml systemXml = new ExampleSystemXml();
            systemXml.EditXmlFile();
            systemXml.ReadXmlFile();
            systemXml.UseXpath();
            systemXml.UseLinqToXmlToCreateNewDocument();
            systemXml.UseLinqToXmlToEditExistDocument();
        }
    }
}
