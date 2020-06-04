using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BUGDETapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    //Data Source=SAVAHHA\SQLEXPRESS01;Initial Catalog=BUDGET;Integrated Security=True
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string password = passwordTextBox.Password;

            string connectionString = @"Data Source=SAVAHHA\SQLEXPRESS01;Initial Catalog=BUDGET;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM client WHERE login=@login", sqlConnection);
            command.Parameters.AddWithValue("@login", login);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    object _id = sqlDataReader.GetValue(0);
                    object _name = sqlDataReader.GetValue(1);
                    object _login = sqlDataReader.GetValue(2);
                    object _password = sqlDataReader.GetValue(3);

                    if (_password.ToString() == password)
                    {
                        App.UserID = Convert.ToInt32(_id.ToString());
                        App.UserLogin = _login.ToString();
                        App.UserName = _name.ToString();
                        App.UserPassword = _password.ToString();

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неправильный пароль, попробуйте снова");
                        passwordTextBox.Password = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Пользователя с таким логином не существует");
                passwordTextBox.Password = "";
                loginTextBox.Text = "";
            }
            sqlConnection.Close();
        }

        private void registrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}
