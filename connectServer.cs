using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
//using System.ServiceModel.Channels;
//using System.ServiceModel.Description;
//using System.ServiceModel.Dispatcher;
//using System.Text;
using System.Xml;
//using Постоянные_ограничения_скорости.ServiceReferenceEgisTps;
//using Постоянные_ограничения_скорости.WebReferenceService1;
//using Vtyagivanie.

namespace ConsoleApp2
{
    /// <summary>
    /// Соединение с сервером
    /// </summary>
    public class ConnectWithServer
    {
        #region Данные

        /// <summary>
        /// Соединение с базой данных
        /// </summary>
        public SqlConnection _connection;

        public SqlConnection Connection()
        {
            if (_connection != null && _connection.State != ConnectionState.Open)
            {
                if (_connection.State != ConnectionState.Closed)
                    _connection.Close();
                _connection.Open();
            }
            //вернуть подключение
            return _connection;
        }

        public SqlConnection ConnectionClose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
                _connection.Close();
            return _connection;
        }

        /// <summary>
        /// Имя базы данных
        /// </summary>
        public string DataBaseName;

        /// <summary>
        /// Признак использования библиотеки DBMSSOCN
        /// </summary>
        public bool NetworkLibraryDBMSSOCN;

        /// <summary>
        /// Имя сервера
        /// </summary>
        public string ServerName;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName;

        /// <summary>
        /// Признак входа с Windows Authentication
        /// </summary>
        public bool WindowsAuthentication;

        /// <summary>
        /// Имя рабочей станции
        /// </summary>
        public string WorkStationName;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password;

        /*
        /// <summary>
        /// Соединение с базой данных
        /// </summary>
        //public SqlConnection Connection2;
         
        /// <summary>
        /// Имя базы данных
        /// </summary>
        public string DataBaseName2;

        /// <summary>
        /// Признак использования библиотеки DBMSSOCN
        /// </summary>
        public bool NetworkLibraryDBMSSOCN2;

        /// <summary>
        /// Имя сервера
        /// </summary>
        public string ServerName2;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName2;

        /// <summary>
        /// Признак входа с Windows Authentication
        /// </summary>
        public bool WindowsAuthentication2;

        /// <summary>
        /// Имя рабочей станции
        /// </summary>
        public string WorkStationName2;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password2;
       
        /// <summary>
        /// Признак использования Web-сервиса при доступе к данным
        /// </summary>
        //public bool UseWebService;
        */


        #endregion

        #region Методы

        /// <summary>
        /// Конструктор для подключения ЕГИС ТПС
        /// </summary>
        public ConnectWithServer(string xmlFileName)
        {
            ReadXmlEgis(xmlFileName);//чтение настроек
        }


        /// <summary>
        /// Конструктор для подключения САР КПД (true - for, folse - master)
        /// </summary>
        public ConnectWithServer(string xmlFileName, bool db)
        {
            ReadXmlFor(xmlFileName);//чтение настроек
            if (!db) DataBaseName = "master";
        }


        public void MakeConnect()
        {
            var s = WindowsAuthentication
                      ? "Integrated Security=SSPI;" +
                        "Persist Security Info=False;Initial Catalog=DBXXXXXX;" +
                        "Data Source=SRXXXXXX;" +
                        "Packet Size=4096;Workstation ID=WSXXXXXX;"
                      : "Persist Security Info=False;" +
                        "User ID=USXXXXXX;Initial Catalog=DBXXXXXX;Data Source=SRXXXXXX;" +
                        "Packet Size=4096;" +
                        "Workstation ID=WSXXXXXX;" +
                        "Password=PSWXXXXX;";
            s = s.Replace("SRXXXXXX", ServerName);
            s = s.Replace("DBXXXXXX", DataBaseName);
            s = s.Replace("WSXXXXXX", WorkStationName);
            s = s.Replace("USXXXXXX", UserName);
            s = s.Replace("PSWXXXXX", Password);
            if (NetworkLibraryDBMSSOCN) s = s + "Network Library=DBMSSOCN";

            _connection = new SqlConnection(s);
            Connection();
            if (_connection.State != ConnectionState.Open)
                throw new Exception("Не удалось соединиться с базой данных");
        }

        /// <summary>
        /// Прочитать строку из XML-файла
        /// </summary>
        /// <param name="xmlNode">Раздел XML-файла, содержащий параметры соединения</param>
        /// <param name="name">Название параметра</param>
        /// <param name="defaultValue">Значение параметра по умолчанию</param>
        /// <returns></returns>
        private static string ReadStringFromXml(XmlNode xmlNode, string name, string defaultValue)
        {
            try
            {
                return xmlNode.Attributes[name].Value;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Чтение настроек из XML-файла для соединения  с  ЕГИС ТПС
        /// </summary>
        private void ReadXmlEgis(string xmlFileName) //= @"PermanentRestriction.xml")
        {
            // Чтение настроек
            if (!File.Exists(xmlFileName)) throw new Exception("Отсутствует файл настроек " + xmlFileName);
            var xml = new XmlDocument();
            xml.Load(xmlFileName);
            if (xml.DocumentElement == null) throw new Exception("Файл настроек " + xmlFileName + " испорчен");
            var xmlNode = xml.DocumentElement["Directories"];
            if (xmlNode == null) throw new Exception("В файле настроек " + xmlFileName + " отсутствует раздел связи с базой данных");


            ServerName = ReadStringFromXml(xmlNode, "ServerName", "");
            DataBaseName = ReadStringFromXml(xmlNode, "DataBaseName", "");
            WorkStationName = ReadStringFromXml(xmlNode, "WorkStationName", "");
            WindowsAuthentication = (ReadStringFromXml(xmlNode, "WindowsAuthentication", "1") == "1");
            if (!WindowsAuthentication)
            {
                UserName = ReadStringFromXml(xmlNode, "UserName", "");
                Password = ReadStringFromXml(xmlNode, "Password", "ЩЩЩ");
            }
            NetworkLibraryDBMSSOCN = (ReadStringFromXml(xmlNode, "NetworkLibraryDBMSSOCN", "0") == "1");
        }


        /// <summary>
        /// Чтение настроек из XML-файла для соединения с For - базой
        /// </summary>
        private void ReadXmlFor(string xmlFileName) //= @"PermanentRestriction.xml")
        {
            // Чтение настроек
            if (!File.Exists(xmlFileName)) throw new Exception("Отсутствует файл настроек " + xmlFileName);
            var xml = new XmlDocument();
            xml.Load(xmlFileName);
            if (xml.DocumentElement == null) throw new Exception("Файл настроек " + xmlFileName + " испорчен");
            var xmlNode = xml.DocumentElement["Directories"];
            if (xmlNode == null)
                throw new Exception("В файле настроек " + xmlFileName + " отсутствует раздел связи с базой данных");

            // Разбор параметров соединения
            ServerName = ReadStringFromXml(xmlNode, "ServerName", "");
            DataBaseName = ReadStringFromXml(xmlNode, "ForDataBaseName", "");
            WorkStationName = ReadStringFromXml(xmlNode, "WorkStationName", "");
            WindowsAuthentication = (ReadStringFromXml(xmlNode, "WindowsAuthentication", "1") == "1");
            if (!WindowsAuthentication)
            {
                UserName = ReadStringFromXml(xmlNode, "UserName", "");
                Password = ReadStringFromXml(xmlNode, "Password", "ЩЩЩ");
            }
            NetworkLibraryDBMSSOCN = (ReadStringFromXml(xmlNode, "NetworkLibraryDBMSSOCN", "0") == "1");
        }

        #endregion
    }
}


