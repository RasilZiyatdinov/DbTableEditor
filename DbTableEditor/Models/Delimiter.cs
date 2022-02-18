using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTableEditor
{
    public static class Delimiter
    {
        //варианты разделителей при экспорте в .CSV
        public static Dictionary<string, string> GetDelimiters()
        {
            Dictionary<string, string> choices = new Dictionary<string, string>();
            choices.Add(",", "Запятая");
            choices.Add(";", "Точка с запятой");
            choices.Add("\t", "Tab");
            return choices;
        }
    }
}
