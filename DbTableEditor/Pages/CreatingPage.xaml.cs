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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DbTableEditor
{
    /// <summary>
    /// Логика взаимодействия для CreatingPage.xaml
    /// </summary>
    public partial class CreatingPage : Page
    {
        public CreatingPage()
        {
            InitializeComponent();
            this.DataContext = new CreatingViewModel(new SqlContext(new MessageBoxService()));
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.mainPage);
        }
    }
}
