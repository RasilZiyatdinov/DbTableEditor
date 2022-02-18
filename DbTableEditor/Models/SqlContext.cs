using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTableEditor
{
    //основные sql-запросы к БД
    public class SqlContext
    {
        private IMessageBoxService messageBoxService;
        public SqlContext(IMessageBoxService _messageBoxService)
        {
            messageBoxService = _messageBoxService;
        }
        public DataTable SelectQuery(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand(query, ConnectionOptions.GetConnection()))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                return dataTable;
            }
            catch (SqlException exception)
            {
                messageBoxService.ShowMessage(exception.Message + "\nЗапрос неверный", "Ошибка", MessageType.Error);
                return null;
            }
            finally
            {
                ConnectionOptions.CloseConnection();
            }
        }

        //удаление таблицы
        public async Task DropQueryAsync(string query, string tableName)
        {
            bool result = messageBoxService.ShowMessage($"Вы действительно хотите удалить таблицу \"{tableName}\"?", "Предупреждение", MessageType.Warning);
            if (result)
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, ConnectionOptions.GetConnection()))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    messageBoxService.ShowMessage($"Выполнено успешно", "Результат", MessageType.Notice);
                }
                catch (SqlException exception)
                {
                    messageBoxService.ShowMessage(exception.Message + "\nЗапрос неверный", "Ошибка", MessageType.Error);
                }
                finally
                {
                    ConnectionOptions.CloseConnection();
                }
            }
        }

        //создание таблицы
        public async Task CreateQueryAsync(string query)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(query, ConnectionOptions.GetConnection()))
                {
                    await command.ExecuteNonQueryAsync();
                }
                messageBoxService.ShowMessage($"Выполнено успешно", "Результат", MessageType.Notice);
            }
            catch (SqlException exception)
            {
                messageBoxService.ShowMessage(exception.Message + "\nЗапрос неверный", "Ошибка", MessageType.Error);
            }
            finally
            {
                ConnectionOptions.CloseConnection();
            }
        }
    }
}
