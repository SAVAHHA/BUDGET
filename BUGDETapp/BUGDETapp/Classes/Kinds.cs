using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BUGDETapp.Classes
{
    public static class Kinds
    {
        public static IList<Kind> KindList { get; set; }
        public static IList<string> KindStrings { get; set; }
        static Kinds()
        {
            KindList = new List<Kind>();
            KindStrings = new List<string>();

            string connectionString = @"Data Source=SAVAHHA\SQLEXPRESS01;Initial Catalog=BUDGET;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Kind_of_income", sqlConnection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    object id = sqlDataReader.GetValue(0);
                    object name = sqlDataReader.GetValue(1);

                    KindList.Add(new Kind { ID = Convert.ToInt32(id.ToString()), Name = name.ToString() });
                    KindStrings.Add(name.ToString());
                }
            }
            sqlConnection.Close();
        }
    }
}
