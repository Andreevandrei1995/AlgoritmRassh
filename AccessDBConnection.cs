using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

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
            DataBaseName = fileName;
            OpenConnection();
        }

        public void OpenConnection()
        {
            _connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseName + ".mdb;");
            if (_connection.State != System.Data.ConnectionState.Open)
                try
                {
                    _connection.Open();
                }
                catch (Exception er)
                {
                    Console.WriteLine("Не удалось открыть файл базы данных. Ошибка: " + er.Message);
                    _connection = null;
                }
        }
    }
}
