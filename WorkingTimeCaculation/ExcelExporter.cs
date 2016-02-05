using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace WorkingTimeCaculation
{
    public class ExcelExporter
    {

        private IWorkbook workbook = null;
        private FileStream fs = null;


        internal int ExportToResultExcel(List<WorkTimeRecordExport> retList, string outputFileName)
        {
            ISheet sheet = null;

            fs = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (outputFileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (outputFileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet("Result");
                }
                else
                {
                    return -1;
                }

                IRow Header = sheet.CreateRow(0);

                Header.CreateCell(0).SetCellValue("部门");
                Header.CreateCell(1).SetCellValue("姓名");
                Header.CreateCell(2).SetCellValue("日期");
                Header.CreateCell(3).SetCellValue("考勤记录");
                Header.CreateCell(4).SetCellValue("正常出勤");
                Header.CreateCell(5).SetCellValue("平时加班");
                Header.CreateCell(6).SetCellValue("周末加班");
                Header.CreateCell(7).SetCellValue("法定加班");
                Header.CreateCell(8).SetCellValue("无法计算");

                for (int i = 0; i < retList.Count; ++i)
                {
                    IRow row = sheet.CreateRow(i+1);

                    var item = retList[i];

                    row.CreateCell(0).SetCellValue(item.Department);
                    row.CreateCell(1).SetCellValue(item.Name);
                    row.CreateCell(2).SetCellValue(item.Date);
                    row.CreateCell(3).SetCellValue(item.WorkRecordRawContent);
                    row.CreateCell(4).SetCellValue(item.NormalWork);
                    row.CreateCell(5).SetCellValue(item.NormalOverwork);
                    row.CreateCell(6).SetCellValue(item.WeekendOverwork);
                    row.CreateCell(7).SetCellValue(item.HolidayOverwork);
                    row.CreateCell(8).SetCellValue(item.Error);

                    //row.RowStyle.FillPattern = FillPattern.Bricks;

                }
                workbook.Write(fs); //写入到excel

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }

            return 0;
        }
    }
}
