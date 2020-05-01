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
        static public List<KeyValuePair<string, string>> dependencies = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("prevyshenieSkorosti","coordinate"),
            new KeyValuePair<string, string>("prevyshenieSkorosti", "locoSvetofor"),
            new KeyValuePair<string, string>("prevyshenieSkorosti", "velocity"),
            new KeyValuePair<string, string>("prevyshenieSkorosti", "svetofor"),
            new KeyValuePair<string, string>("prevyshenieSkorosti", "allVelocityRestrictions")
        };

        static bool checkNecessaryParams (string eventName, Moment moment)
        {
            List<string> necessaryParamNames = new List<string>();
            foreach(KeyValuePair<string,string> dependency in dependencies) {
                if (dependency.Key == eventName)
                {
                    necessaryParamNames.Add(dependency.Value);
                }
            }
            if (necessaryParamNames.Count == 0)
            {
                return true;
            }
            Type type = typeof(Moment);
            string beginOfOutString = "Проверка для поля " + eventName + ".";
            int quantityFoundNecessaryParams = 0;
            foreach (String necessaryParamName in necessaryParamNames)
            {
                FieldInfo fieldInfo = type.GetField(necessaryParamName);
                var fieldValue = fieldInfo.GetValue(moment);
                if (!(fieldValue is Object)) {
                    Console.WriteLine(beginOfOutString + " Поле " + necessaryParamName + " имеет тип поля не Object.");
                    continue;
                }
                if (fieldValue == null)
                {
                    Console.WriteLine(beginOfOutString + " Поле " + necessaryParamName + " не инициализировано.");
                    continue;
                }
                //Работаем с полем exist
                //Метод GetField возвращает null, если поле не найдено в классе
                FieldInfo existField = fieldValue.GetType().GetField("exist");
                if (existField == null)
                {
                    Console.WriteLine(beginOfOutString + " Поле " + necessaryParamName + " не имеет поля exist.");
                    continue;
                }
                var exist = existField.GetValue(fieldValue);
                if (!(exist is bool))
                {
                    Console.WriteLine(beginOfOutString + " Поле " + necessaryParamName + " имеет поле exist, у которого тип не равен bool.");
                    continue;
                }                
                if (!(bool)exist)
                {
                    Console.WriteLine(beginOfOutString + " Поле " + necessaryParamName + " имеет поле exist равное false.");
                   continue;
                }
                quantityFoundNecessaryParams++;
            }

            if (quantityFoundNecessaryParams == necessaryParamNames.Count) {
                return true;
            }
            return false;
        }


        static void Main(string[] args)
        {
            //getFile();
            //Reflection reflection = new Reflection
            //{
            //    a = "5",
            //    b = "6"
            //};
            //Type type = typeof(Reflection);
            //FieldInfo fieldInfo = type.GetField("a");
            //Console.WriteLine(fieldInfo.GetValue(reflection).GetType());
            //Console.WriteLine(fieldInfo.GetValue(reflection));
            //string a = null;
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
