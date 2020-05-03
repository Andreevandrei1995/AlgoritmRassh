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

        #region Методы
        /// <summary>
        /// Конструктор соединения
        /// </summary>
        /// <param name="accessDbFileName"></param>
        public AccessDBConnection(string accessDbFileName)
        {
            if (!File.Exists(accessDbFileName))
            {
                DataBaseName = accessDbFileName;
            }
            else
            {
                throw new Exception("Отсутствует файл БД " + accessDbFileName);
            }
        }
        public OleDbConnection OpenConnection()
        {            
            this._connection = new OleDbConnection(GetConnectionQueryString());            
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
        private OleDbConnection CloseConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
                _connection.Close();
            return _connection;
        }        
        public string GetConnectionQueryString()
        {
            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseName;
        }        
        #endregion Методы
    }
}
