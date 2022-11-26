using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCourceApp.Scrips
{
    class DataBase
    {
        public readonly MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=couseuiop");
        public void openConBd()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConBd()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }


        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
