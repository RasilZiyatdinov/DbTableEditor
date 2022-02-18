using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTableEditor
{
    //поведение окна сообщений
    public interface IMessageBoxService
    {
        bool ShowMessage(string text, string caption, MessageType type);
    }
}
