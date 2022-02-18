using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTableEditor
{
    //свойства подключения к БД
    public static class ConnectionOptions
    {
        public static string DataBaseName { get; set; }

        private static SqlConnection connection;
        public static SqlConnection Connection
        {
            get => connection;
            set
            {
                connection = value;
            }
        }

        public static SqlConnection GetConnection() //открыть подключение
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }
        public static void CloseConnection() //закрыть подключение
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
