using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DbTableEditor
{
    //создание таблицы
    public class CreatingViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TableProperty> PropertyList { get; set; } //список полей таблицы
        public ICommand CreateCommand { get; } //создать таблицу
        public ICommand AddCommand { get; } //добавить поле
        public ICommand DeleteCommand { get; } //убрать поле

        private string tableName;
        private SqlContext sqlContext;

        public event PropertyChangedEventHandler PropertyChanged;

        public string TableName
        {
            get => tableName;
            set
            {
                tableName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TableName"));
            }
        }
        public CreatingViewModel(SqlContext sqlCont)
        {
            PropertyList = new ObservableCollection<TableProperty>();
            sqlContext = sqlCont;
            CreateCommand = new RelayCommand(CreateTable);
            AddCommand = new RelayCommand(AddRow);
            DeleteCommand = new RelayCommand(DeleteRow);
        }
        private void AddRow()
        {
            PropertyList.Add(new TableProperty());
        }
        private void DeleteRow()
        {
            if (PropertyList.Count > 0)
                PropertyList.RemoveAt(PropertyList.Count - 1);
        }
        private async void CreateTable()
        {
            string query = $"CREATE TABLE {TableName} (";
            foreach (var item in PropertyList)
            {
                if (item.IsPrimaryKey)
                {
                    query += item.Name + " " + item.Type + " PRIMARY KEY, ";
                }
                else
                {
                    query += item.Name + " " + item.Type + ", ";
                }
            }
            query = query.Substring(0, query.Length - 2);
            query += ");";
            await sqlContext.CreateQueryAsync(query);
            PropertyList.Clear();
            TableName = string.Empty;
        }
    }
}
