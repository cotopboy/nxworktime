using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WorkingTimeCaculation
{
    public class ExcelReader
    {

        public DataTable ReadExcel(string file)
        {
            DataTable dt = null;
            try
            {
                using (ExcelHelper excelHelper = new ExcelHelper(file))
                {
                    dt = excelHelper.ExcelToDataTable("工人", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return dt;
        }
    }
}
