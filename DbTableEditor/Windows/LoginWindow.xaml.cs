using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DbTableEditor
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static string user = string.Empty;
        public static string password = string.Empty;
        public static string server = string.Empty;
        public static string db = string.Empty;
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            server = Server.Text.ToString();
            db = DataBase.Text.ToString();
            user = User.Text.ToString();
            password = Password.Password.ToString();
            try
            {
                MainWindow mw = new MainWindow(server, db, user, password);
                mw.Show();
                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void SystemUserChecked(object sender, RoutedEventArgs e)
        {
            Server.Text = "RasilPC";
            DataBase.Text = "TestDB";
            User.Text = "user1";
            Password.Password = "sa";
        }
        private void SystemUserUnchecked(object sender, RoutedEventArgs e)
        {
            Server.Clear();
            DataBase.Clear();
            User.Clear();
            Password.Clear();
        }
    }
}
