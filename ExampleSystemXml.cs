using System.Xml;
using System.Xml.Linq;

namespace XmlHelper
{
    public class ExampleSystemXml
    {
        public void ReadXmlFile()
        {
            string greeting = String.Format("{0} Using {1} {0}", "".PadRight(24, '*'), "System.Xml for reading XML File");
            Console.WriteLine(greeting);

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

            Console.WriteLine("".PadRight(greeting.Length, '-'));
        }

        public void EditXmlFile()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("people.xml");
            XmlElement? xRoot = xDoc.DocumentElement;

            XmlElement personElem = xDoc.CreateElement("person"); // Создаём новый элемент с именем person

            XmlAttribute nameAttr = xDoc.CreateAttribute("name"); // Создаём атрибут с именем name

            XmlElement companyElem = xDoc.CreateElement("company"); // Создаём элементы company и age
            XmlElement ageElem = xDoc.CreateElement("age");

            XmlText nameText = xDoc.CreateTextNode("George"); // Создаём текстовые значения для атрибута и элементов
            XmlText companyText = xDoc.CreateTextNode("NHC");
            XmlText ageText = xDoc.CreateTextNode("35");

            nameAttr.AppendChild(nameText); // Добавляем значение атрибуту name

            companyElem.AppendChild(companyText); // Добавляем значения элементам company и age
            ageElem.AppendChild(ageText);

            personElem.Attributes.Append(nameAttr); // Связываем атрибут name с элементом person

            personElem.AppendChild(companyElem); // Добавляем элементы company и age как дочерние элементы для person
            personElem.AppendChild(ageElem);

            xRoot?.AppendChild(personElem); // Добавляем элемент person как дочерний элемент к корневому
            xDoc.Save("people.xml");
        }


        /// <summary>
        /// Пример использования XPath при обработке файла XML
        /// </summary>
        public void UseXpath()
        {
            string greeting = String.Format("{0} Using {1} {0}", "".PadRight(24, '*'), "XPath to process XML File");
            Console.WriteLine(greeting);

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("people.xml");
            XmlElement? xRoot = xDoc.DocumentElement;

            XmlNodeList? nodes = xRoot.SelectNodes("*"); // Выбираем все дочерние элементы текущего узла ("*") 
            if (nodes is not null)
            {
                foreach (XmlNode node in nodes)
                {
                    Console.WriteLine(node.OuterXml);
                }
            }

            XmlNodeList? personNodes = xRoot.SelectNodes("person"); // Выбираем все элементы <person>...</person> ("person")
            if (personNodes is not null)
            {
                foreach (XmlNode node in personNodes)
                {
                    Console.WriteLine(node.SelectSingleNode("@name")?.Value); // В выбранных элементах получаем атрибут по имени name ("@name") и выводим его значение
                }
            }

            XmlNode? tomNode = xRoot?.SelectSingleNode("person[@name='Tom']"); // Выбираем один элемент person со значение атрибута name - Tom <person name='Tom'>...</person> ("person[@name='Tom']")
            Console.WriteLine(tomNode?.OuterXml);

            XmlNodeList? companyNodes = xRoot?.SelectNodes("//person/company"); // Выбираем все элементы company, являющиеся дочерними по отношению к элементам person
            if (companyNodes is not null)
            {
                foreach (XmlNode node in companyNodes)
                {
                    Console.WriteLine(node.InnerText);
                }
            }

            Console.WriteLine("".PadRight(greeting.Length, '-'));
        }

        /// <summary>
        /// Пример использования Linq (пространство имен System.Xml.Linq) для работы с XML
        /// </summary>
        public void UseLinqToXmlToCreateNewDocument()
        {
            XDocument xDoc = new XDocument(); // Создаём новый объект, инкапсулирующий документ XML

            XElement sam = new XElement("person"); // Используя базовый класс для представления элемента документа XML - XElement, - создаём новый объект person
            XAttribute samAttrName = new XAttribute("name", "Sam"); // Создаём новый атрибут, используя класс XAttribute
            XElement samCompanyElem = new XElement("company", "Oracle");
            XElement samAgeElem = new XElement("age", "41");

            sam.Add(samAttrName); // Добавляем к элементу sam атрибут samAttrName
            sam.Add(samCompanyElem); // Добавляем к элементу sam дочерний элемент samCompanyElem
            sam.Add(samAgeElem); // Добавляем к элементу sam дочерний элемент samAgeElem

            XElement alex = new XElement("person");
            XAttribute alexAttrName = new XAttribute("name", "Alex");
            XElement alexCompanyElem = new XElement("company", "Twitter");
            XElement alexAgeElem = new XElement("age", "23");

            alex.Add(alexAttrName);
            alex.Add(alexCompanyElem);
            alex.Add(alexAgeElem);

            XElement people = new XElement("people");
            people.Add(sam);
            people.Add(alex);

            xDoc.Add(people); // добавляем к документу корневой элемент people
            xDoc.Save("people2.xml"); // Сохраняем документ
        }

        public void UseLinqToXmlToEditExistDocument()
        {
            XDocument xDoc = XDocument.Load("people.xml");
            XElement xRoot = xDoc.Element("people");

            XElement sam = new XElement("person"); // Используя базовый класс для представления элемента документа XML - XElement, - создаём новый объект person
            XAttribute samAttrName = new XAttribute("name", "Sam"); // Создаём новый атрибут, используя класс XAttribute
            XElement samCompanyElem = new XElement("company", "Oracle");
            XElement samAgeElem = new XElement("age", "41");

            sam.Add(samAttrName); // Добавляем к элементу sam атрибут samAttrName
            sam.Add(samCompanyElem); // Добавляем к элементу sam дочерний элемент samCompanyElem
            sam.Add(samAgeElem); // Добавляем к элементу sam дочерний элемент samAgeElem

            XElement alex = new XElement("person");
            XAttribute alexAttrName = new XAttribute("name", "Alex");
            XElement alexCompanyElem = new XElement("company", "Twitter");
            XElement alexAgeElem = new XElement("age", "23");

            alex.Add(alexAttrName);
            alex.Add(alexCompanyElem);
            alex.Add(alexAgeElem);

            xRoot.Add(sam);
            xRoot.Add(alex);

            xDoc.Save("people.xml");
        }

        public void GetDatasetFromXmlByLinq()
        {
            string greeting = String.Format("{0} Using {1} {0}", "".PadRight(24, '*'), "Linq-to-XML to process XML File");
            Console.WriteLine(greeting);

            XDocument xDoc = XDocument.Load("people.xml");

            XElement? people = xDoc.Element("people");

            if (people is not null)
            {
                foreach (XElement person in people.Elements("person"))
                {
                    XAttribute? name = person.Attribute("name");
                    XElement? company = person.Element("company");
                    XElement? age = person.Element("age");

                    Console.WriteLine($"Person: {name?.Value}. Company - {company?.Value}, age - {age?.Value}");
                }
            }

            var nhc = xDoc.Element("people")?
                .Elements("person")
                .Where(p => p.Element("company")?.Value.ToLower() == "nhc")
                .Select(p => new
                {
                    name = p.Attribute("name")?.Value,
                    company = p.Element("company")?.Value,
                    age = p.Element("age")?.Value
                });

            if (nhc is not null)
            {
                foreach (var person in nhc)
                {
                    Console.WriteLine($"I've found him! His name is {person.name}, he works at {person.company} and he is {person.age} years old!");
                }
            }

            Console.WriteLine("".PadRight(greeting.Length, '-'));
        }
    }
}
