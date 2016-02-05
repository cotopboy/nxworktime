using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WorkingTimeCaculation.Calculcation;

namespace WorkingTimeCaculation
{
    public class WorkTimeProcessor
    {

        private DateTimeSorter timeSorter = new DateTimeSorter();

        private PauseSectionProvider pauseSectionProvider = new PauseSectionProvider();

        public WorkTimeProcessor()
        {

        }

        internal List<WorkTimeRecordExport> ProcessFile(DataTable dataTable)
        {
            List<RawRecord> rawRecordList = this.GetRawRecordList(dataTable);

            List<WorkingTimeRecord> recordList = new RawRecordConverter().ConvertRawRecord(rawRecordList);

            List<WorkingTimeRecord> recordList2 = new WorkingTimePreprocessor().Process(recordList);

            CalculateWorkingTime(recordList2);

            var notProcessList = recordList2.Where(x => !x.IsProcessed).ToList();

            Console.WriteLine("The following items are not processed, please calculate manually!");
            foreach (var item in notProcessList)
            {
                Console.WriteLine(item.Name + " " + item.Date + " "+ item.CheckTimeDetail.RawText);
            }

            DistributeWorkTime(recordList2);

            List<WorkTimeRecordExport> retList = CovertToExport(recordList2);

            return retList;
        }

        private void DistributeWorkTime(List<WorkingTimeRecord> recordList2)
        {
            DateTime checkDate = new DateTime (2016,5,2);
            foreach (var item in recordList2)
            {
                DateTime date = item.Date;

                if (date > checkDate)
                {
                    Environment.Exit(0);
                }

                string type = timeSorter.GetDateType(date);

                if (type == "Holiday")
                {
                    item.ResultSet.HolidayOverwork = Round(item.TotalWorkTime.TotalHours);
                }
                else if (type == "Weekend")
                {
                    item.ResultSet.WeekendOverwork = Round(item.TotalWorkTime.TotalHours);
                }
                else 
                {
                    if (item.TotalWorkTime.TotalHours <= 8)
                    {
                        item.ResultSet.NormalWork = Round(item.TotalWorkTime.TotalHours);
                    }
                    else
                    {
                        item.ResultSet.NormalWork = 8;
                        item.ResultSet.NormalOverwork = Round(item.TotalWorkTime.TotalHours - 8);
                    }
                }
            }
        }

        private double Round(double input)
        {
            return Math.Round(input / 0.5) * 0.5; 
        }

        private List<WorkTimeRecordExport> CovertToExport(List<WorkingTimeRecord> recordList2)
        {
            List<WorkTimeRecordExport> retList = new List<WorkTimeRecordExport>();


            foreach (var input in recordList2)
            {
                WorkTimeRecordExport exportItem = new WorkTimeRecordExport();
                exportItem.Date = input.Date.ToString("yyyy-MM-dd");
                exportItem.Department = input.Department;
                exportItem.Name = input.Name;
                exportItem.WorkRecordRawContent = input.WorkRecordRawContent;

                exportItem.NormalWork = input.ResultSet.NormalWork;
                exportItem.NormalOverwork = input.ResultSet.NormalOverwork;
                exportItem.WeekendOverwork = input.ResultSet.WeekendOverwork;
                exportItem.HolidayOverwork = input.ResultSet.HolidayOverwork;

                exportItem.Error = input.IsProcessed ? "" : "[#####################]";
                retList.Add(exportItem);


            }

            return retList;
        }

        private void CalculateWorkingTime(List<WorkingTimeRecord> inputList)
        {
            foreach (var record in inputList)
            {
                if (record.CheckTimeDetail.IsSectionClear)
                {
                    new SimpleWorkTimeCalculator().Calculate(record);
                }
                else if (record.CheckTimeDetail.IsTXXTPattern)
                {
                    new TXXTCalculator().Calculate(record);
                }
                else if (record.CheckTimeDetail.IsTTXTPattern)
                {
                    new TTXTCalculator().Calculcate(record);
                }
                else if (record.CheckTimeDetail.IsTXTXPattern)
                {
                    new TXTXCalculator().Calculcate(record);
                }
                else if (record.CheckTimeDetail.IsTTXTTTPattern)
                {
                    new TTXTTTCalculator().Calculcate(record);
                }
                else if (record.CheckTimeDetail.IsXTTT)
                {
                    new XTTTCalculator().Calculate(record);
                }
                else if (record.CheckTimeDetail.IsXTXT)
                {
                    new XTXTCalculator().Calculate(record);
                }
                else if (record.CheckTimeDetail.IsTXTT)
                {
                    new TXTTCalculator().Calculcate(record);                    
                }
                else if (record.CheckTimeDetail.IsTXTTTT)
                {
                    new TXTTTTCalculator().Calculcate(record);
                }
                else if (record.CheckTimeDetail.IsXTTTTT)
                { 
                    new XTTTTTCalculator().Calculcate(record);     
                }
                else if (record.CheckTimeDetail.IsTXXTXT)
                {
                    new TXXTXTCalculator().Calculate(record);
                }
                else if (record.CheckTimeDetail.IsTXXTTT)
                {
                    new TXXTTTCalculator().Calculcate(record);
                }
                else if (record.CheckTimeDetail.IsTTXTXT)
                {
                    new TTXTXTCalculator().Calculcate(record);
                }
                else
                {
                    new OtherCalculator().Calculate(record);
                }
            }
        }

        private List<RawRecord> GetRawRecordList(DataTable data)
        {
            List<RawRecord> rawList = new List<RawRecord>();
            for (int i = 0; i < data.Rows.Count; ++i)
            {
                var raw = new RawRecord(data.Rows[i]);

                if (raw.Department == null || raw.Department == "")
                {
                    continue;
                }
                else
                {
                    rawList.Add(raw);
                }                
            }
            return rawList;
        }
    }

    public class RawRecord
    {
        public RawRecord(DataRow row)
        {
            this.Department = row[0] as string;
            this.Name = row[1] as string;
            this.Date = row[2] as string;
            this.WorkingTimeRecordRawText = row[3] as string; 
        }

        public string Department { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public string WorkingTimeRecordRawText { get; set; }
    }
}
