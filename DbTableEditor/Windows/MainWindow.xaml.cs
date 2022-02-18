using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DbTableEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainPage mainPage;

        public MainWindow(string server, string db, string user, string password)
        {
            InitializeComponent();
            ConnectDataBase(server, db, user, password);
        }
        private void ConnectDataBase(string server, string db, string user, string password)
        {
            string connectionString = $"Server={server};Database={db};User Id={user};Password={password};";
            ConnectionOptions.Connection = new SqlConnection(connectionString);
            ConnectionOptions.DataBaseName = db;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            mainPage = new MainPage();
            MainFrame.Content = mainPage;
        }
    }
}
