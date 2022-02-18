using GalaSoft.MvvmLight.Command;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbTableEditor
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> TableList { get; set; } //список таблиц

        private string selectedTable;
        private string delimiter;
        private bool isHeaders;
        private DataView data;
        private SqlContext sqlContext;

        public ICommand DeleteCommand { get; } //удалить выбранную таблицу
        public ICommand RefreshCommand { get; } //обновить список таблиц
        public ICommand ExportCommand { get; } //экспортировать данные выбранной таблицы в .CSV

        public MainViewModel(SqlContext sqlCont)
        {
            TableList = new ObservableCollection<string>();
            sqlContext = sqlCont;
            DeleteCommand = new RelayCommand(DropTable);
            RefreshCommand = new RelayCommand(InitTableList);
            ExportCommand = new RelayCommand(Export);

            InitTableList();
        }
        public string SelectedTable //выбранная таблица (из списка)
        {
            get => selectedTable;
            set
            {
                selectedTable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedTable"));
                if (selectedTable != null) ShowTable(selectedTable);
            }
        }

        public DataView Data //отображение данных выбранной таблицы
        {
            get => data;
            set
            {
                data = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Data"));
            }
        }

        public string Delimiter //разделитель для экспорта
        {
            get => delimiter;
            set
            {
                delimiter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Delimiter"));
            }
        }

        public bool IsHeaders //экспорт с заголовками или без
        {
            get => isHeaders;
            set
            {
                isHeaders = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsHeaders"));
            }
        }

        private async void DropTable() //удаление выбранной таблицы
        {
            if (SelectedTable != null)
            {
                string query = $"DROP TABLE {SelectedTable}";
                await sqlContext.DropQueryAsync(query, SelectedTable);
                Data = null;
            }
        }

        private void ShowTable(string tableName) //вывод данных выбранной таблицы
        {
            string query = $"SELECT * FROM {tableName}";
            DataTable dt = sqlContext.SelectQuery(query);
            Data = dt.DefaultView;
        }

        private void InitTableList() //получить список таблиц подключенной БД
        {
            string query = $"SELECT TABLE_NAME FROM {ConnectionOptions.DataBaseName}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            TableList.Clear();
            DataTable dt = sqlContext.SelectQuery(query);
            foreach (DataRow item in dt.Rows)
            {
                TableList.Add(item["TABLE_NAME"].ToString());
            }
        }

        private void Export() //экспорт данных выбранной таблицы в .CSV
        {
            DataTable dt = Data.ToTable();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = $"{SelectedTable}.csv";

            if (sfd.ShowDialog() == true)
                ExporterCSV.ExportToCSV(dt, sfd.FileName, Delimiter, IsHeaders);
        }
    }
}
