using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
            //getDependenciesFromDB();
            foreach (InitialParamsForOneMoment initialParamsForOneMoment in Program.allInitialParams)
            {
                Moment newMoment = new Moment(Program.allInitialParams.IndexOf(initialParamsForOneMoment));
                Program.allMoments.Add(newMoment);
                Program.lastMoment = newMoment;
                Program.previousMoment = Program.allMoments.Count > 1 ? Program.allMoments[Program.allMoments.Count - 2] : null;                
                newMoment.init(initialParamsForOneMoment);
            }
        }

        static public List<Moment> allMoments = new List<Moment>();
        static public Moment lastMoment = null;
        static public Moment previousMoment = null;
        static public List<VelocityRestriction> allVelocityRestrictions = new List<VelocityRestriction>()
        {
            new VelocityRestrictionSvetofor(20,"velocityRestrictionSvetofor","КЖ",400)
        };

        static public List<KeyValuePair<string, string>> dependencies = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("velocityRestrictionSvetofor", "currentCoordinate"),
            new KeyValuePair<string, string>("velocityRestrictionSvetofor", "svetofor"),
            new KeyValuePair<string, string>("velocityRestrictionSvetofor", "locoSvetofor"),

            new KeyValuePair<string, string>("allActiveVelocityExcesses", "currentVelocity"),
            new KeyValuePair<string, string>("allActiveVelocityExcesses", "allActiveVelocityRestrictions"),

            new KeyValuePair<string, string>("formulation", "allActiveVelocityExcesses")
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
        static public void allNecessaryParamsFoundOrException(string eventName)
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
        static public bool checkNecessaryParams(string eventName)
        {
            if (allMoments.Count == 0)
            {
                return false;
            }
            return checkNecessaryParams(eventName, allMoments[allMoments.Count - 1]);
        }
        static public bool checkNecessaryParams(string eventName, Moment moment)
        {
            List<string> necessaryParamNames = new List<string>();
            foreach (KeyValuePair<string, string> dependency in dependencies)
            {
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
                if (!(fieldValue is Object))
                {
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

            if (quantityFoundNecessaryParams == necessaryParamNames.Count)
            {
                return true;
            }
            return false;
        }

        static void getDependenciesFromDB()
        {
            string fileName = "DBViolationAlgoritm.mdb";
            AccessDBConnection accessDBConnection = new AccessDBConnection(fileName);
            accessDBConnection.OpenConnection();
            string script = @"SELECT Obj.Name, DepObj.Name
                FROM (Dependencies 
                INNER JOIN Objects AS Obj 
                ON Dependencies.ObjectID = Obj.ID) 
                INNER JOIN Objects AS DepObj 
                ON Dependencies.DependentObjectID = DepObj.ID ";
            OleDbCommand command = new OleDbCommand(script, accessDBConnection._connection);
            try
            {
                OleDbDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    Console.WriteLine("В БД нет данных.");
                }
                while (reader.Read())
                {
                    dependencies.Add(new KeyValuePair<string, string>(reader.GetString(0), reader.GetString(1)));
                }
                reader.Close();
                reader = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось прочитать саблицу зависимостей из БД");
            }            
        }
    }
}
