using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTableEditor
{
    //поле таблицы
    public class TableProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; } //имя поля
        public string Type { get; set; } //тип поля
        private bool isPrimaryKey;       //первичный ключ

        public bool IsPrimaryKey
        {
            get => isPrimaryKey;
            set
            {
                isPrimaryKey = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsPrimaryKey"));
            }
        }

    }
}
