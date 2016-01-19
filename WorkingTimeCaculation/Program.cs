using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Math.ExtensionMethods;
using System.IO;
using Utilities.IO;


namespace WorkingTimeCaculation
{
    class Program
    {

        static List<People> peopleList = new List<People>();

        static void Main(string[] args)
        {
            string [] files = Directory.GetFiles(DirectoryHelper.CurrentExeDirectory, "*.xls");
            foreach (var file in files)
            {
                DataTable dt = ReadExcel(file);

                ProcessTable(dt,file);
                
            }
         
        }

        private static void ProcessTable(DataTable data,string name)
        {

            string outputName = Path.GetFileNameWithoutExtension(name) + "_Result.txt";

            string outputFullPath = Path.Combine(DirectoryHelper.CurrentExeDirectory, outputName);

            People people = null;
            if (data == null) return;
            for (int i = 1; i < data.Rows.Count; ++i)
            {            
              

                var raw = new RawRecord(data.Rows[i]);


                if (raw.Department == null || raw.Department== "")
                {
                    peopleList.Add(people);
                    people = null;
                    continue;
                }

                if (people == null && raw.Department != "")
                {
                    people = new People();

                    people.Name = (string)data.Rows[i][1];
                    people.Department = (string)data.Rows[i][0];
                }
                else
                {
                    var record = new Record(raw);
                    people.RecordList.Add(record);
                }
            }

            StringBuilder builder = new StringBuilder();

            foreach (var item in peopleList)
            {
                builder.AppendLine(item.GetOutputContent());
            }

            FileInfo file = new FileInfo(outputFullPath);

            file.Save(builder.ToString(), Encoding.UTF8);

            
        }

        static DataTable ReadExcel(string file)
        {
            DataTable dt = null;
            try
            {
                using (ExcelHelper excelHelper = new ExcelHelper(file))
                {
                    dt = excelHelper.ExcelToDataTable("MySheet", true);                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }


            return dt;
        }

        private static void PrintData(DataTable data)
        {
            if (data == null) return;
            for (int i = 0; i < data.Rows.Count; ++i)
            {
                for (int j = 0; j < data.Columns.Count; ++j)
                    Console.Write("{0} ", data.Rows[i][j]);
                Console.Write("\n");
            }
        }
    }
}
