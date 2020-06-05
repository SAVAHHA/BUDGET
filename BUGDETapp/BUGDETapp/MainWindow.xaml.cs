﻿using System;
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
using System.Windows.Media.Animation;
using System.Threading.Tasks;

namespace BUGDETapp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public TextBox textBox { get; set; }
        public DatePicker datePicker { get; set; }
        public ComboBox comboBox { get; set; }
        public Button saveButton { get; set; }
        public Button saveChangesButton { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            GetData();
            incomeListView.ItemsSource = Incomes.IncomeList;
            expenseListView.ItemsSource = Expenses.ExpenseList;
        }

        public void GetData()
        {
            Incomes.IncomeList = new List<Income>();
            Expenses.ExpenseList = new List<Expense>();
            string connectionString = @"Data Source=SAVAHHA\SQLEXPRESS01;Initial Catalog=BUDGET;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM expenses WHERE user_id=@userId", sqlConnection);
            command.Parameters.AddWithValue("@userId", App.UserID);
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
            sqlConnection.Close();

            string connectionString2 = @"Data Source=SAVAHHA\SQLEXPRESS01;Initial Catalog=BUDGET;Integrated Security=True";
            SqlConnection sqlConnection2 = new SqlConnection(connectionString2);
            sqlConnection2.Open();
            SqlCommand command2 = new SqlCommand("SELECT * FROM income WHERE user_id=@userId", sqlConnection2);
            command2.Parameters.AddWithValue("@userId", App.UserID);
            SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            if (sqlDataReader2.HasRows)
            {
                while (sqlDataReader2.Read())
                {
                    object incomeId = sqlDataReader2.GetValue(0);
                    object date = sqlDataReader2.GetValue(1);
                    object sum = sqlDataReader2.GetValue(2);
                    object kindId = sqlDataReader2.GetValue(4);

                    Incomes.IncomeList.Add(new Income { ID = Convert.ToInt32(incomeId.ToString()), Date = DateTime.Parse(date.ToString()), Sum = Convert.ToInt32(sum.ToString()), UserID = App.UserID, KindID = Convert.ToInt32(kindId.ToString()) });
                }
            }
            sqlConnection2.Close();
        }

        private void addIncomeButton_Click(object sender, RoutedEventArgs e)
        {
            addIncomeButton.IsEnabled = false;
            textBox = new TextBox();
            datePicker = new DatePicker();
            comboBox = new ComboBox();
            saveButton = new Button { Content = "Сохранить"};
            saveButton.Click += SaveIncomeButton_Click;
            comboBox.ItemsSource = Kinds.KindStrings;
            addIncomeStackPanel.Children.Add(new Label { Content = "Введите сумму" });
            addIncomeStackPanel.Children.Add(textBox);
            addIncomeStackPanel.Children.Add(new Label { Content = "Выберете дату зачисления" });
            addIncomeStackPanel.Children.Add(datePicker);
            addIncomeStackPanel.Children.Add(new Label { Content = "Выберете тип зачисления" });
            addIncomeStackPanel.Children.Add(comboBox);
            addIncomeStackPanel.Children.Add(saveButton);

        }

        private void SaveIncomeButton_Click(object sender, RoutedEventArgs e)
        {
            int sum = Convert.ToInt32(textBox.Text);
            DateTime? date = datePicker.SelectedDate;
            string nameKind = comboBox.Text;
            int kindID = 0;
            foreach (var kind in Kinds.KindList)
            {
                if (kind.Name == nameKind)
                {
                    kindID = kind.ID;
                }
            }

            string connectionString = @"Data Source=SAVAHHA\SQLEXPRESS01;Initial Catalog=BUDGET;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Income(date,sum,user_id,Kind_id) VALUES(@date,@sum,@userId,@kindId)", sqlConnection);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@sum", sum);
            command.Parameters.AddWithValue("@userId", App.UserID);
            command.Parameters.AddWithValue("@kindId", kindID);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            Task.Delay(200);
            addIncomeStackPanel.Children.Clear();
            GetData();
            incomeListView.ItemsSource = Incomes.IncomeList;
            expenseListView.ItemsSource = Expenses.ExpenseList;
        }

        private void addExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            addExpenseButton.IsEnabled = false;
            textBox = new TextBox();
            datePicker = new DatePicker();
            comboBox = new ComboBox();
            saveButton = new Button { Content = "Сохранить" };
            saveButton.Click += SaveExpenseButton_Click;
            comboBox.ItemsSource = Types.TypeStrings;
            addExpensesStackPanel.Children.Add(new Label { Content = "Введите сумму" });
            addExpensesStackPanel.Children.Add(textBox);
            addExpensesStackPanel.Children.Add(new Label { Content = "Выберете дату расходов" });
            addExpensesStackPanel.Children.Add(datePicker);
            addExpensesStackPanel.Children.Add(new Label { Content = "Выберете тип расходов" });
            addExpensesStackPanel.Children.Add(comboBox);
            addExpensesStackPanel.Children.Add(saveButton);
        }

        private void SaveExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            int sum = Convert.ToInt32(textBox.Text);
            DateTime? date = datePicker.SelectedDate;
            string nametype = comboBox.Text;
            int typeID = 0;
            foreach (var type in Types.TypeList)
            {
                if (type.Name == nametype)
                {
                    typeID = type.ID;
                }
            }

            string connectionString = @"Data Source=SAVAHHA\SQLEXPRESS01;Initial Catalog=BUDGET;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Expenses(date,sum,user_id,Type_id) VALUES(@date,@sum,@userId,@typeId)", sqlConnection);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@sum", sum);
            command.Parameters.AddWithValue("@userId", App.UserID);
            command.Parameters.AddWithValue("@typeId", typeID);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            Task.Delay(200);
            addExpensesStackPanel.Children.Clear();
            GetData();
            incomeListView.ItemsSource = Incomes.IncomeList;
            expenseListView.ItemsSource = Expenses.ExpenseList;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            loginTextBox.IsReadOnly = false;
            passwordTextBox.IsReadOnly = false;
            saveChangesButton = new Button { Content = "Сохранить" };
            saveChangesButton.Click += SaveChangesButton_Click;
            saveButtonStackPanel.Children.Add(saveChangesButton);
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            string newlogin = loginTextBox.Text;
            string newpassword = passwordTextBox.Text;
            App.UserLogin = newlogin;
            App.UserPassword = newpassword;
            string connectionString = @"Data Source=SAVAHHA\SQLEXPRESS01;Initial Catalog=BUDGET;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("UPDATE client SET login=@newLogin, pass=@newPass WHERE user_id=@id", sqlConnection);
            command.Parameters.AddWithValue("@id", App.UserID);
            command.Parameters.AddWithValue("@newLogin", newlogin);
            command.Parameters.AddWithValue("@newPass", newpassword);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            Task.Delay(200);
            saveButtonStackPanel.Children.Clear();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
