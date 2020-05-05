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
            DateTime begin;
            DateTime end;

            begin = DateTime.Now;
            for(int i = 0; i < 10000; i++)
            {
                foreach (InitialParamsForOneMoment initialParamsForOneMoment in Program.allInitialParams)
                {
                    int index = i * Program.allInitialParams.Count + Program.allInitialParams.IndexOf(initialParamsForOneMoment);
                    Moment newMoment = new Moment(index);
                    Program.allMoments.Add(newMoment);
                    Program.lastMoment = newMoment;
                    Program.previousMoment = Program.allMoments.Count > 1 ? Program.allMoments[Program.allMoments.Count - 2] : null;
                    newMoment.init(initialParamsForOneMoment);
                }
            }
            end = DateTime.Now;
            Console.WriteLine(end - begin);
        }

        static public List<Moment> allMoments = new List<Moment>();
        static public Moment lastMoment = null;
        static public Moment previousMoment = null;
        /// <summary>
        /// Список всех установленных ограничений скорости
        /// </summary>
        static public List<VelocityRestriction> allVelocityRestrictions = new List<VelocityRestriction>()
        {
            new VelocityRestrictionSvetofor(20,"velocityRestrictionSvetofor","КЖ",400),
            new VelocityRestrictionSvetofor(5,"velocityRestrictionSvetofor","КЖ",400)
        };

        //static public List<KeyValuePair<string, string>> dependencies = new List<KeyValuePair<string, string>>();
        static public List<KeyValuePair<string, string>> dependencies = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("moment", "trainCoordinate"),
            new KeyValuePair<string, string>("moment", "trainSvetofor"),
            new KeyValuePair<string, string>("moment", "svetofor"),
            new KeyValuePair<string, string>("moment", "trainVelocity"),
            new KeyValuePair<string, string>("moment", "allActiveVelocityRestrictions"),
            new KeyValuePair<string, string>("moment", "allActiveVelocityExcesses"),
            new KeyValuePair<string, string>("moment", "formulation"),

            new KeyValuePair<string, string>("velocityRestrictionSvetofor", "trainCoordinate"),
            new KeyValuePair<string, string>("velocityRestrictionSvetofor", "svetofor"),
            new KeyValuePair<string, string>("velocityRestrictionSvetofor", "trainSvetofor"),

            new KeyValuePair<string, string>("allActiveVelocityRestrictions", "velocityRestrictionSvetofor"),

            new KeyValuePair<string, string>("allActiveVelocityExcesses", "trainVelocity"),
            new KeyValuePair<string, string>("allActiveVelocityExcesses", "allActiveVelocityRestrictions"),

            new KeyValuePair<string, string>("formulation", "allActiveVelocityExcesses")
        };

        /// <summary>
        /// Исходные данные для расшифровки посекундно (из поездки и номограммы)
        /// </summary>
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
        static public Program.PositiveNecessaryParamsStatus allNecessaryParamsFoundOrException(string eventName)
        {
            Program.NecessaryParamsStatus necessaryParamsStatus = Program.checkNecessaryParams(eventName);
            if (necessaryParamsStatus == Program.NecessaryParamsStatus.NotInitialized)
            {
                string s = "Нет всех необходимых параметров для создания объекта класса " + eventName;
                //Console.WriteLine(s);
                throw new Exception(s);
            }
            return (Program.PositiveNecessaryParamsStatus)necessaryParamsStatus;
        }
        static public Program.PositiveNecessaryParamsStatus allNecessaryParamsFoundOrException(string eventName, Moment moment)
        {
            Program.NecessaryParamsStatus necessaryParamsStatus = Program.checkNecessaryParams(eventName, moment);
            if (necessaryParamsStatus == Program.NecessaryParamsStatus.NotInitialized)
            {
                string s = "Нет всех необходимых параметров для создания объекта класса " + eventName;
                //Console.WriteLine(s);
                throw new Exception(s);
            }
            return (Program.PositiveNecessaryParamsStatus)necessaryParamsStatus;
        }
        /// <summary>
        /// Проверка состояния параметров, необходимых для определения события eventName, в последний момент времени
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        static public Program.NecessaryParamsStatus checkNecessaryParams(string eventName)
        {
            if (Program.allMoments.Count == 0)
            {
                return Program.NecessaryParamsStatus.NotInitialized;
            }
            return Program.checkNecessaryParams(eventName, Program.allMoments[Program.allMoments.Count - 1]);
        }

        public enum NecessaryParamsStatus : int
        {
            //Не все необходимые параметры инициализированы
            NotInitialized = 1,
            //Все необходимые параметры инициализированы, но есть параметры, у которых параметр exist = false
            ExistFalse = 2,
            //Все необходимые параметры инициализированы, у всех параметров exist = true
            ExistTrue = 3
        }
        public enum PositiveNecessaryParamsStatus : int
        {
            ExistFalse = Program.NecessaryParamsStatus.ExistFalse,
            ExistTrue = Program.NecessaryParamsStatus.ExistTrue
        }
        /// <summary>
        /// Проверка состояния параметров, необходимых для определения события eventName, в заданнный момент времени
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="moment"></param>
        /// <returns></returns>
        static public Program.NecessaryParamsStatus checkNecessaryParams(string eventName, Moment moment)
        {
            List<string> necessaryParamNames = Program.getListNecessaryParamNames(eventName);
            if (necessaryParamNames.Count == 0)
            {
                return Program.NecessaryParamsStatus.ExistTrue;
            }
            Type type = typeof(Moment);
            string beginOfOutString = "Проверка для поля " + eventName + ".";
            //int quantityFoundNecessaryParams = 0;
            int quantityFoundNecessaryParamsWithExistTrue = 0;
            foreach (String necessaryParamName in necessaryParamNames)
            {
                FieldInfo fieldInfo = type.GetField(necessaryParamName);
                //Метод GetField возвращает null, если поле не найдено в классе
                if (fieldInfo == null)
                {
                    Console.WriteLine(beginOfOutString + " Поля " + necessaryParamName + " нет в объекте класса Moment.");
                    //continue;
                    return Program.NecessaryParamsStatus.NotInitialized;
                }
                //если поле necessaryParamName найдено в классе Moment, получаем его значение
                var fieldValue = fieldInfo.GetValue(moment);
                if (!(fieldValue is InterfaceExist))
                {
                    //Console.WriteLine(beginOfOutString + " Поле " + necessaryParamName + " не наследует интерфейс InterfaceExist.");
                    //continue;
                    return Program.NecessaryParamsStatus.NotInitialized;
                }
                if (fieldValue == null)
                {
                    //Console.WriteLine(beginOfOutString + " Поле " + necessaryParamName + " не инициализировано.");
                    //continue;
                    return Program.NecessaryParamsStatus.NotInitialized;
                }
                //quantityFoundNecessaryParams++;
                InterfaceExist interfaceExist = (InterfaceExist)fieldValue;
                if (!interfaceExist.exist)
                {
                    //Console.WriteLine(beginOfOutString + " Поле " + necessaryParamName + " имеет значение поля exist равное false.");
                    continue;
                }
                quantityFoundNecessaryParamsWithExistTrue++;
            }

            //if (quantityFoundNecessaryParams != necessaryParamNames.Count)
            //{
            //    return Program.NecessaryParamsStatus.NotInitialized;
            //}
            if (quantityFoundNecessaryParamsWithExistTrue != necessaryParamNames.Count)
            {
                return Program.NecessaryParamsStatus.ExistFalse;
            }
            return Program.NecessaryParamsStatus.ExistTrue;
        }

        static public List<string> getListNecessaryParamNames (string eventName)
        {
            List<string> necessaryParamNames = new List<string>();
            foreach (KeyValuePair<string, string> dependency in Program.dependencies)
            {
                if (dependency.Key == eventName)
                {
                    necessaryParamNames.Add(dependency.Value);
                }
            }
            return necessaryParamNames;
        }

        static private void getDependenciesFromDB()
        {
            string fileName = "DBConnections\\DBViolationAlgoritm.mdb";
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
                    //Console.WriteLine("В БД нет данных.");
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
                //Console.WriteLine("Не удалось прочитать саблицу зависимостей из БД");
            }
            accessDBConnection.CloseConnection();
        }
    }
}
