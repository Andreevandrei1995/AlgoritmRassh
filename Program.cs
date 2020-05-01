using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AlgoritmRassh
{
    class Program
    {
        static void Main(string[] args)
        {
            getFile();
            Reflection reflection = new Reflection
            {
                a = "5",
                b = "6"
            };
            Type type = typeof(Reflection);
            FieldInfo fieldInfo = type.GetField("a");
            Console.WriteLine(fieldInfo.GetValue(reflection).GetType());
            Console.WriteLine(fieldInfo.GetValue(reflection));
            string a = null;
        }

        static void getFile()
        {
            string FileName = "data.xml";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<item><name>wrench</name></item>");

            // Add a price element.
            XmlElement newElem = doc.CreateElement("price");
            newElem.InnerText = "10.95";
            doc.DocumentElement.AppendChild(newElem);

            // Save the document to a file. White space is
            // preserved (no white space).
            doc.PreserveWhitespace = true;
            doc.Save(FileName);
            if (!File.Exists(FileName))
            {
                Console.WriteLine("Файл настроек отсутствует");
            }
            else
            {
                Console.WriteLine("Файл настроек присутствует");
            }
        }
    }

    class Reflection
    {
        public string a;
        public string b;
    }
}
