using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DbTableEditor
{
    public class ExporterCSV
    {
        public static void ExportToCSV(DataTable dtDataTable, string strFilePath, string delimiter, bool includeHeaders)
        {
            try
            {
                StreamWriter sw = new StreamWriter(strFilePath, false);
                if (includeHeaders)
                {
                    for (int i = 0; i < dtDataTable.Columns.Count; i++)
                    {
                        sw.Write(dtDataTable.Columns[i]);
                        if (i < dtDataTable.Columns.Count - 1)
                        {
                            sw.Write(delimiter);
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                foreach (DataRow dr in dtDataTable.Rows)
                {
                    for (int i = 0; i < dtDataTable.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString();
                            value = value.Trim();
                            if (value.Contains(delimiter))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString().Trim());
                            }
                        }
                        if (i < dtDataTable.Columns.Count - 1)
                        {
                            sw.Write(delimiter);
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}
