using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace AlgoritmRassh
{
    /// <summary>
    /// Соединение с сервером
    /// </summary>
    public class DBConnection
    {
        #region Данные
        /// <summary>
        /// Соединение с базой данных
        /// </summary>
        public SqlConnection _connection;
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
        #endregion Данные

        #region Методы
        /// <summary>
        /// Чтение настроек из XML-файла для соединения
        /// </summary>
        public DBConnection (string xmlFileName)
        {
            // Чтение настроек
            if (!File.Exists(xmlFileName)) throw new Exception("Отсутствует файл настроек " + xmlFileName);
            var xml = new XmlDocument();
            xml.Load(xmlFileName);
            if (xml.DocumentElement == null) throw new Exception("Файл настроек " + xmlFileName + " испорчен");
            var xmlNode = xml.DocumentElement["Directories"];
            if (xmlNode == null) throw new Exception("В файле настроек " + xmlFileName + " отсутствует раздел связи с базой данных");


            ServerName = GetAttributeValueFromXml(xmlNode, "ServerName", "");
            DataBaseName = GetAttributeValueFromXml(xmlNode, "DataBaseName", "");
            WorkStationName = GetAttributeValueFromXml(xmlNode, "WorkStationName", "");
            WindowsAuthentication = (GetAttributeValueFromXml(xmlNode, "WindowsAuthentication", "1") == "1");
            if (!WindowsAuthentication)
            {
                UserName = GetAttributeValueFromXml(xmlNode, "UserName", "");
                Password = GetAttributeValueFromXml(xmlNode, "Password", "");
            }
            NetworkLibraryDBMSSOCN = (GetAttributeValueFromXml(xmlNode, "NetworkLibraryDBMSSOCN", "0") == "1");
        }
        private SqlConnection OpenConnection()
        {
            this._connection = new SqlConnection(GetConnectionQueryString());
            if (_connection != null && _connection.State != ConnectionState.Open)
            {
                this.CloseConnection();
                _connection.Open();
            }
            if (_connection.State != ConnectionState.Open)
                throw new Exception("Не удалось соединиться с базой данных");
            //вернуть подключение
            return _connection;
        }
        private SqlConnection CloseConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
                _connection.Close();
            return _connection;
        }
        public string GetConnectionQueryString()
        {
            var query = WindowsAuthentication
                      ? "Integrated Security=SSPI;" +
                        "Persist Security Info=False;Initial Catalog=DBXXXXXX;" +
                        "Data Source=SRXXXXXX;" +
                        "Packet Size=4096;Workstation ID=WSXXXXXX;"
                      : "Persist Security Info=False;" +
                        "User ID=USXXXXXX;Initial Catalog=DBXXXXXX;Data Source=SRXXXXXX;" +
                        "Packet Size=4096;" +
                        "Workstation ID=WSXXXXXX;" +
                        "Password=PSWXXXXX;";
            query = query.Replace("SRXXXXXX", ServerName);
            query = query.Replace("DBXXXXXX", DataBaseName);
            query = query.Replace("WSXXXXXX", WorkStationName);
            query = query.Replace("USXXXXXX", UserName);
            query = query.Replace("PSWXXXXX", Password);
            if (NetworkLibraryDBMSSOCN) query = query + "Network Library=DBMSSOCN";
            return query;
        }
        /// <summary>
        /// Прочитать строку из XML-файла
        /// </summary>
        /// <param name="xmlNode">Раздел XML-файла, содержащий параметры соединения</param>
        /// <param name="name">Название параметра</param>
        /// <param name="defaultValue">Значение параметра по умолчанию</param>
        /// <returns></returns>
        private static string GetAttributeValueFromXml(XmlNode xmlNode, string name, string defaultValue)
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
        #endregion Методы
    }
}