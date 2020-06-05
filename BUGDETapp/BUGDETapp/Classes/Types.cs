using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BUGDETapp.Classes
{
    public static class Types
    {
        public static IList<Type> TypeList { get; set; }
        public static IList<string> TypeStrings { get; set; }
        static Types()
        {
            TypeList = new List<Type>();
            TypeStrings = new List<string>();

            string connectionString = @"Data Source=SAVAHHA\SQLEXPRESS01;Initial Catalog=BUDGET;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Expenses_types", sqlConnection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    object id = sqlDataReader.GetValue(0);
                    object name = sqlDataReader.GetValue(1);

                    TypeList.Add(new Type { ID = Convert.ToInt32(id.ToString()), Name = name.ToString() });
                    TypeStrings.Add(name.ToString());
                }
            }
            sqlConnection.Close();
        }
    }
}

