using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DbTableEditor
{
    public class MessageBoxService : IMessageBoxService
    {
        public bool ShowMessage(string text, string caption, MessageType type)
        {
            switch (type)
            {
                case MessageType.Error:
                    {
                        MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                        return true;
                    }
                case MessageType.Warning:
                    {
                        var result = MessageBox.Show(text, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                        return result == MessageBoxResult.Yes;
                    }
                case MessageType.Notice:
                    {
                        MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Information);
                        return true;
                    }
                default:
                    return false;
            }
        }
    }
}
