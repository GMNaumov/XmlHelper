using System.Xml;

namespace XmlHelper
{
    public class ExampleSystemXml
    {
        public void Run()
        {
            XmlDocument xDoc = new XmlDocument(); // Создаём новый объект для работы с файлом XML
            xDoc.Load("people.xml"); // Загружаем в созданный объект файл для работы с ним (файлом)
            XmlElement? xRoot = xDoc.DocumentElement; // Получаем корневой элемент XML-файла
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot) // Обрабатываем все дочерние элементы корневого узла
                {
                    XmlNode? attr = xnode.Attributes.GetNamedItem("name"); // Получает атрибут следующего элемента по имени "name"
                    Console.WriteLine(attr?.Value);

                    foreach (XmlNode childnode in xnode.ChildNodes) // Обрабатываем дочерние элементы
                    {
                        if (childnode.Name == "company") // Сравниваем имя элемента с company
                        {
                            Console.WriteLine($"Company: {childnode.InnerText}");
                        }

                        if (childnode.Name == "age") // Сравниваем имя элемента с company
                        {
                            Console.WriteLine($"Age: {childnode.InnerText}");
                        }
                    }
                }
            }
        }
    }
}
