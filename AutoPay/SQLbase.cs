using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Project
{
    class SQLbase
    {
        private static string NameOfDataBase = "";

        public SQLbase() { }

        static public void Insert(string str)
        {
            SqlConnection sqlConnection = new SqlConnection($"server=localhost;Trusted_Connection=Yes;DataBase={NameOfDataBase};");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = str;

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        static public DataTable Select(string str)
        {
            DataTable dataTable = new DataTable();

            SqlConnection sqlConnection = new SqlConnection($"server=localhost;Trusted_Connection=Yes;DataBase={NameOfDataBase};");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = str;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик

            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            sqlConnection.Close();

            return dataTable;
        }
    }
}
