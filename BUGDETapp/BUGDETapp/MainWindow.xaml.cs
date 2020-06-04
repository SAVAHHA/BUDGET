using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using BUGDETapp.Classes;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BUGDETapp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            incomeListView.ItemsSource = Incomes.IncomeList;
            expenseListView.ItemsSource = Expenses.ExpenseList;
        }

        public void GetData()
        {
            string connectionString = @"Data Source=SAVAHHA\SQLEXPRESS01;Initial Catalog=BUDGET;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM expenses WHERE user_id=@userId", sqlConnection);
            command.Parameters.AddWithValue("@user_id", App.UserID);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    object expenseId = sqlDataReader.GetValue(0);
                    object date = sqlDataReader.GetValue(1);
                    object sum = sqlDataReader.GetValue(2);
                    object typeId = sqlDataReader.GetValue(4);

                    Expenses.ExpenseList.Add(new Expense { ID = Convert.ToInt32(expenseId.ToString()), Date = DateTime.Parse(date.ToString()), Sum = Convert.ToInt32(sum.ToString()), UserID = App.UserID, TypeID = Convert.ToInt32(typeId.ToString()) });
                }
            }
            SqlCommand command2 = new SqlCommand("SELECT * FROM income WHERE user_id=@userId", sqlConnection);
            command2.Parameters.AddWithValue("@user_id", App.UserID);
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    object incomeId = sqlDataReader.GetValue(0);
                    object date = sqlDataReader.GetValue(1);
                    object sum = sqlDataReader.GetValue(2);
                    object kindId = sqlDataReader.GetValue(4);

                    Incomes.IncomeList.Add(new Income { ID = Convert.ToInt32(incomeId.ToString()), Date = DateTime.Parse(date.ToString()), Sum = Convert.ToInt32(sum.ToString()), UserID = App.UserID, KindID = Convert.ToInt32(kindId.ToString()) });
                }
            }
        }
    }
}
