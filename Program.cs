using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Reflection;

namespace AlgoritmRassh
{
    class Program
    {
        static void Main(string[] args)
        {
            getDependenciesFromDB();
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
                    Program.currentMoment = newMoment;
                    Program.previousMoment = Program.allMoments.Count > 1 ? Program.allMoments[Program.allMoments.Count - 2] : null;
                    newMoment.init(initialParamsForOneMoment);
                }
            }
            end = DateTime.Now;
            Console.WriteLine(end - begin);
        }

        static public List<Moment> allMoments = new List<Moment>();
        static public Moment currentMoment = null;
        static public Moment previousMoment = null;
        /// <summary>
        /// Список всех установленных ограничений скорости
        /// </summary>
        static public List<VelocityRestriction> allVelocityRestrictions = new List<VelocityRestriction>()
        {
            new VelocityRestrictionSvetofor(20,"velocityRestrictionSvetofor","КЖ",400),
            new VelocityRestrictionSvetofor(5,"velocityRestrictionSvetofor","КЖ",400),
            new VelocityRestrictionPermanent(40,"velocityRestrictionPermanent","5",2,40,"5",3,40)
        };

        static public List<KeyValuePair<string, string>> dependencies = new List<KeyValuePair<string, string>>();
        //static public List<KeyValuePair<string, string>> dependencies = new List<KeyValuePair<string, string>>()
        //{
        //    new KeyValuePair<string, string>("moment", "trainCoordinate"),
        //    new KeyValuePair<string, string>("moment", "trainSvetofor"),
        //    new KeyValuePair<string, string>("moment", "trainVelocity"),
        //    new KeyValuePair<string, string>("moment", "svetofor"),
        //    new KeyValuePair<string, string>("moment", "activeVelocityRestrictions"),
        //    new KeyValuePair<string, string>("moment", "activeVelocityExcesses"),
        //    new KeyValuePair<string, string>("moment", "endedInPreviousMomentVelocityExcesses"),
        //    new KeyValuePair<string, string>("moment", "formulation"),

        //    new KeyValuePair<string, string>("velocityRestrictionSvetofor", "trainCoordinate"),
        //    new KeyValuePair<string, string>("velocityRestrictionSvetofor", "svetofor"),
        //    new KeyValuePair<string, string>("velocityRestrictionSvetofor", "trainSvetofor"),
        //    new KeyValuePair<string, string>("velocityRestrictionPermanent", "trainCoordinate"),

        //    new KeyValuePair<string, string>("activeVelocityRestrictions", "velocityRestrictionSvetofor"),
        //    new KeyValuePair<string, string>("activeVelocityRestrictions", "velocityRestrictionPermanent"),

        //    new KeyValuePair<string, string>("activeVelocityExcesses", "trainVelocity"),
        //    new KeyValuePair<string, string>("activeVelocityExcesses", "activeVelocityRestrictions"),

        //    new KeyValuePair<string, string>("endedInPreviousMomentVelocityExcesses", "activeVelocityExcesses"),

        //    new KeyValuePair<string, string>("formulation", "endedInPreviousMomentVelocityExcesses")
        //};

        /// <summary>
        /// Исходные данные для расшифровки посекундно (из поездки и номограммы)
        /// </summary>
        static public List<InitialParamsForOneMoment> allInitialParams = new List<InitialParamsForOneMoment>()
        {
            new InitialParamsForOneMoment()
            {
                kmCoordinate = "5",
                pkCoordinate = 1,//3,
                mCoordiante = 52,
                colorLocoSvetofor = "КЖ",
                nameSvetofor = "m3",
                kmSvetofor = "5",
                pkSvetofor = 6,
                mSvetofor = 36,
                valueVelocity = 60//25
            },
            new InitialParamsForOneMoment()
            {
                kmCoordinate = "5",
                pkCoordinate = 2,//3,
                mCoordiante = 52,
                colorLocoSvetofor = "КЖ",
                nameSvetofor = "m3",
                kmSvetofor = "5",
                pkSvetofor = 9,
                mSvetofor = 36,
                valueVelocity = 60//25
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
                valueVelocity = 60//18
            }
        };
        static public Program.dependenciesPositiveStatus allDependenciesInitializedOrException(string eventName)
        {
            Program.dependenciesStatus dependenciesStatus = Program.checkDependinciesStatus(eventName);
            if (dependenciesStatus == Program.dependenciesStatus.NotInitialized)
            {
                string s = "Нет всех необходимых параметров для создания объекта класса " + eventName;
                //Console.WriteLine(s);
                throw new Exception(s);
            }
            return (Program.dependenciesPositiveStatus)dependenciesStatus;
        }
        static public Program.dependenciesPositiveStatus allDependenciesInitializedOrException(string eventName, Moment moment)
        {
            Program.dependenciesStatus dependenciesStatus = Program.checkDependenciesStatus(eventName, moment);
            if (dependenciesStatus == Program.dependenciesStatus.NotInitialized)
            {
                string s = "Нет всех необходимых параметров для создания объекта класса " + eventName;
                //Console.WriteLine(s);
                throw new Exception(s);
            }
            return (Program.dependenciesPositiveStatus)dependenciesStatus;
        }
        /// <summary>
        /// Проверка состояния параметров, необходимых для определения события eventName, в последний момент времени
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        static public Program.dependenciesStatus checkDependinciesStatus(string eventName)
        {
            if (Program.allMoments.Count == 0)
            {
                return Program.dependenciesStatus.NotInitialized;
            }
            return Program.checkDependenciesStatus(eventName, Program.allMoments[Program.allMoments.Count - 1]);
        }

        public enum dependenciesStatus : int
        {
            //Не все необходимые параметры инициализированы
            NotInitialized = 1,
            //Все необходимые параметры инициализированы, но есть параметры, у которых параметр exist = false
            ExistFalse = 2,
            //Все необходимые параметры инициализированы, у всех параметров exist = true
            ExistTrue = 3
        }
        public enum dependenciesPositiveStatus : int
        {
            ExistFalse = Program.dependenciesStatus.ExistFalse,
            ExistTrue = Program.dependenciesStatus.ExistTrue
        }
        /// <summary>
        /// Проверка состояния параметров, необходимых для определения события eventName, в заданнный момент времени
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="moment"></param>
        /// <returns></returns>
        static public Program.dependenciesStatus checkDependenciesStatus(string eventName, Moment moment)
        {
            List<string> dependencyNames = Program.getDependencyNamesList(eventName);
            if (dependencyNames.Count == 0)
            {
                return Program.dependenciesStatus.ExistTrue;
            }
            Type type = typeof(Moment);
            string beginOfOutString = "Проверка для поля " + eventName + ".";
            //int quantityDependenciesInitialized = 0;
            int quantityDependinciesWithExistTrue = 0;
            foreach (String dependencyName in dependencyNames)
            {
                FieldInfo fieldInfo = type.GetField(dependencyName);
                //Метод GetField возвращает null, если поле не найдено в классе
                if (fieldInfo == null)
                {
                    Console.WriteLine(beginOfOutString + " Поля " + dependencyName + " нет в объекте класса Moment.");
                    //continue;
                    return Program.dependenciesStatus.NotInitialized;
                }
                //если поле dependencyName найдено в классе Moment, получаем его значение
                var fieldValue = fieldInfo.GetValue(moment);
                if (!(fieldValue is InterfaceExist))
                {
                    //Console.WriteLine(beginOfOutString + " Поле " + dependencyName + " не наследует интерфейс InterfaceExist.");
                    //continue;
                    return Program.dependenciesStatus.NotInitialized;
                }
                if (fieldValue == null)
                {
                    //Console.WriteLine(beginOfOutString + " Поле " + dependencyName + " не инициализировано.");
                    //continue;
                    return Program.dependenciesStatus.NotInitialized;
                }
                //quantityDependenciesInitialized++;
                InterfaceExist interfaceExist = (InterfaceExist)fieldValue;
                if (!interfaceExist.exist)
                {
                    //Console.WriteLine(beginOfOutString + " Поле " + dependencyName + " имеет значение поля exist равное false.");
                    continue;
                }
                quantityDependinciesWithExistTrue++;
            }

            //if (quantityDependenciesInitialized != dependencyNames.Count)
            //{
            //    return Program.dependenciesStatus.NotInitialized;
            //}
            if (quantityDependinciesWithExistTrue != dependencyNames.Count)
            {
                return Program.dependenciesStatus.ExistFalse;
            }
            return Program.dependenciesStatus.ExistTrue;
        }

        static public List<string> getDependencyNamesList (string eventName)
        {
            List<string> dependencyNames = new List<string>();
            foreach (KeyValuePair<string, string> dependency in Program.dependencies)
            {
                if (dependency.Key == eventName)
                {
                    dependencyNames.Add(dependency.Value);
                }
            }
            return dependencyNames;
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
