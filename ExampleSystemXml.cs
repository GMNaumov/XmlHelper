using System.Xml;

namespace XmlHelper
{
    public class ExampleSystemXml
    {
        public void Run()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("people.xml");
            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    XmlNode? attr = xnode.Attributes.GetNamedItem("name");
                    Console.WriteLine(attr?.Value);

                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "company")
                        {
                            Console.WriteLine($"Company: {childnode.InnerText}");
                        }

                        if (childnode.Name == "age")
                        {
                            Console.WriteLine($"Age: {childnode.InnerText}");
                        }
                    }
                }
            }
        }
    }
}
