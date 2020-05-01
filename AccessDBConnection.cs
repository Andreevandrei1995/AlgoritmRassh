using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace AlgoritmRassh
{
    class AccessDBConnection
    {

        #region Данные
        /// <summary>
        /// Соединение с базой данных
        /// </summary>
        public OleDbConnection _connection;

        /// <summary>
        /// Имя базы данных
        /// </summary>
        public string DataBaseName;
        
        #endregion Данные

        /// <summary>
        /// Конструктор соединения
        /// </summary>
        /// <param name="fileName"></param>
        public AccessDBConnection(string fileName)
        {
            if (!File.Exists(fileName))
            {
                DataBaseName = fileName;
            }
            else
            {
                Console.WriteLine("Файл "+ fileName + " не найден");
            }
        }        

        private OleDbConnection OpenConnection()
        {
            //this._connection = new OleDbConnection(GetConnectionQueryString());
            this._connection = new OleDbConnection(GetConnectionQueryString());
            
            if (_connection != null && _connection.State != ConnectionState.Open)
            {
                if (_connection.State != ConnectionState.Closed)
                    _connection.Close();
                _connection.Open();
            }
            if (_connection.State != ConnectionState.Open)
                throw new Exception("Не удалось соединиться с базой данных");
            //вернуть подключение
            return _connection;
        }

        private OleDbConnection CloseConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
                _connection.Close();
            return _connection;
        }

        #region Методы
        public string GetConnectionQueryString()
        {
            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseName + ".mdb;";
        }        
        #endregion Методы

    }
}
