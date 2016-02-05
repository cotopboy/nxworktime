using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Utilities.IO;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Math.ExtensionMethods;

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

            List<WorkTimeRecordExport> retList = worktimeProcessor.ProcessFile(dataTable);

            string DirPath = DirectoryHelper.CombineWithCurrentExeDir("output");

            if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);


            FileInfo fileInfo = new FileInfo(file);
            string outputFileName = Path.Combine(DirPath, fileInfo.Name.Replace(".", DateTime.Now.ToFileNameSafeString() + "."));

            excelExport.ExportToResultExcel(retList, outputFileName);

        }



    }
}
