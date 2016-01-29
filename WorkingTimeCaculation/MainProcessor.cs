using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Utilities.IO;

namespace WorkingTimeCaculation
{
    public class MainProcessor
    {
        public MainProcessor()
        {
               
        }

        public void StartProcess()
        {
            string[] files = Directory.GetFiles(DirectoryHelper.CurrentExeDirectory, "*.xls");

            foreach (var file in files)
            {               
                ProcessSinlgeFile(file);
            }
        }

        private void ProcessSinlgeFile(string file)
        {
            WorkTimeProcessor worktimeProcessor = new WorkTimeProcessor();

            ExcelReader excelReader = new ExcelReader();
            ExcelExporter excelExport = new ExcelExporter(); 

            var dataTable = excelReader.ReadExcel(file);

            List<WorkTimeRecord> retList =  worktimeProcessor.ProcessFile(dataTable);

            string outputFileName = file.Replace(".", "_result.");

            excelExport.ExportToResultExcel(dataTable, outputFileName);

        }



    }
}
