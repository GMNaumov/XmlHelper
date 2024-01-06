using System.Xml;

namespace XmlHelper
{
    public class ExampleSystemXml
    {
        public void ReadXmlFile()
        {
            List<Person> persons = new List<Person>();

            XmlDocument xDoc = new XmlDocument(); // Создаём новый объект для работы с файлом XML
            xDoc.Load("people.xml"); // Загружаем в созданный объект файл для работы с ним (файлом)
            XmlElement? xRoot = xDoc.DocumentElement; // Получаем корневой элемент XML-файла
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot) // Обрабатываем все дочерние элементы корневого узла
                {
                    Person person = new Person();
                    XmlNode? attr = xnode.Attributes.GetNamedItem("name"); // Получает атрибут следующего элемента по имени "name"
                    person.Name = attr?.Value;

                    foreach (XmlNode childnode in xnode.ChildNodes) // Обрабатываем дочерние элементы
                    {
                        if (childnode.Name == "company") // Сравниваем имя элемента с company
                        {
                            person.Company = childnode.InnerText;
                        }

                        if (childnode.Name == "age") // Сравниваем имя элемента с company
                        {
                            person.Age = int.Parse(childnode.InnerText);
                        }
                    }

                    persons.Add(person);
                }
            }

            foreach (Person person in persons)
            {
                Console.WriteLine($"{person.Name}. Age:{person.Age}, company:{person.Company}");
            }
        }

        public void EditXmlFile()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("people.xml");
            XmlElement? xRoot = xDoc.DocumentElement;

            XmlElement personElem = xDoc.CreateElement("person");
            XmlAttribute nameAttr = xDoc.CreateAttribute("name");

            XmlElement companyElem = xDoc.CreateElement("company");
            XmlElement ageElem = xDoc.CreateElement("age");

            XmlText nameText = xDoc.CreateTextNode("George");
            XmlText companyText = xDoc.CreateTextNode("NHC");
            XmlText ageText = xDoc.CreateTextNode("35");

            nameAttr.AppendChild(nameText);
            companyElem.AppendChild(companyText);
            ageElem.AppendChild(ageText);

            personElem.Attributes.Append(nameAttr);
            personElem.AppendChild(companyElem);
            personElem.AppendChild(ageElem);

            xRoot?.AppendChild(personElem);
            xDoc.Save("people.xml");
        }
    }
}
