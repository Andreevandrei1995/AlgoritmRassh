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
            foreach (InitialParamsForOneMoment initialParamsForOneMoment in Program.allInitialParams)
            {
                Moment newMoment = new Moment();
                allMoments.Add(newMoment);
                lastMoment = newMoment;
                newMoment.init(initialParamsForOneMoment);
            }            
        }

        static public List<Moment> allMoments = new List<Moment>();
        static public Moment lastMoment = null;
        static public List<Object> allVelocityRestrictions = new List<Object>()
        {
            new VelocityRestrictionSvetofor(20,"velocityRestrictionSvetofor","КЖ",400)
        };

        static public List<KeyValuePair<string, string>> dependencies = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("velocityRestrictionSvetofor", "coordinate"),
            new KeyValuePair<string, string>("velocityRestrictionSvetofor", "svetofor"),
            new KeyValuePair<string, string>("velocityRestrictionSvetofor", "locoSvetofor"),

            new KeyValuePair<string, string>("prevyshenieSkorosti", "coordinate"),
            new KeyValuePair<string, string>("prevyshenieSkorosti", "locoSvetofor"),
            new KeyValuePair<string, string>("prevyshenieSkorosti", "velocity"),
            new KeyValuePair<string, string>("prevyshenieSkorosti", "svetofor"),
            new KeyValuePair<string, string>("prevyshenieSkorosti", "allActiveVelocityRestrictions")
        };

        static public List<InitialParamsForOneMoment> allInitialParams = new List<InitialParamsForOneMoment>()
        {
            new InitialParamsForOneMoment()
            {
                kmCoordinate = "5",
                pkCoordinate = 3,
                mCoordiante = 52,
                colorLocoSvetofor = "КЖ",
                nameSvetofor = "m3",
                kmSvetofor = "5",
                pkSvetofor = 6,
                mSvetofor = 36,
                valueVelocity = 25
            },
            new InitialParamsForOneMoment()
            {
                kmCoordinate = "5",
                pkCoordinate = 3,
                mCoordiante = 52,
                colorLocoSvetofor = "КЖ",
                nameSvetofor = "m3",
                kmSvetofor = "5",
                pkSvetofor = 9,
                mSvetofor = 36,
                valueVelocity = 25
            },
            new InitialParamsForOneMoment()
            {
                kmCoordinate = "5",
                pkCoordinate = 3,
                mCoordiante = 52,
                colorLocoSvetofor = "КЖ",
                nameSvetofor = "m3",
                kmSvetofor = "5",
                pkSvetofor = 6,
                mSvetofor = 36,
                valueVelocity = 18
            }
        };

        static public void allNecessaryParamsFoundOrException (string eventName)
        {
            if (!Program.checkNecessaryParams(eventName))
            {
                string s = "Нет всех необходимых параметров для создания объекта класса " + eventName;
                Console.WriteLine(s);
                throw new Exception(s);
            }
        }

        static public void allNecessaryParamsFoundOrException(string eventName, Moment moment)
        {
            if (!Program.checkNecessaryParams(eventName, moment))
            {
                string s = "Нет всех необходимых параметров для создания объекта класса " + eventName;
                Console.WriteLine(s);
                throw new Exception(s);
            }
        }

        static public bool checkNecessaryParams (string eventName)
        {
            if (allMoments.Count == 0)
            {
                return false;
            }
            return checkNecessaryParams(eventName, allMoments[allMoments.Count - 1]);
        }

        static public bool checkNecessaryParams (string eventName, Moment moment)
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
                //Метод GetField возвращает null, если поле не найдено в классе
                if (fieldInfo == null)
                {
                    Console.WriteLine(beginOfOutString + " Поля " + necessaryParamName + " нет в объекте класса Moment.");
                    continue;
                }
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
                if (!(fieldValue is InterfaceExist))
                {
                    Console.WriteLine(beginOfOutString + " Поле " + necessaryParamName + " не наследует интерфейс InterfaceExist.");
                    continue;
                }               
                quantityFoundNecessaryParams++;
            }

            if (quantityFoundNecessaryParams == necessaryParamNames.Count) {
                return true;
            }
            return false;
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
