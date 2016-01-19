using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WorkingTimeCaculation
{
    public class RawRecord
    {
        public RawRecord(DataRow row)
        {
            this.Department      = row[0] as string;
            this.Name            = row[1] as string;
            this.Date            = row[2] as string;
            this.NormalWork      = row[4] as string;
            this.NormalOverwork  = row[5] as string;
            this.WeekendOverwork = row[6] as string;
            this.HolidayOverwork = row[7] as string;            
        }

        public string Department { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public string NormalWork { get; set; }

        public string NormalOverwork { get; set; }

        public string WeekendOverwork { get; set; }

        public string HolidayOverwork { get; set; }
    }

    public class Record
    {
        public string Department { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public double NormalWork { get; set; }

        public double NormalOverwork { get; set; }

        public double WeekendOverwork { get; set; }

        public double HolidayOverwork { get; set; }

        public Record (RawRecord raw)
	    {
            this.Department = raw.Department;
            this.Name = raw.Name;
            this.Date = DateTime.Parse(raw.Date);

            double outputValue = 0.0;
            if (double.TryParse(raw.NormalWork, out outputValue)) this.NormalWork = outputValue;
            if (double.TryParse(raw.NormalOverwork, out outputValue)) this.NormalOverwork = outputValue;
            if (double.TryParse(raw.WeekendOverwork, out outputValue)) this.WeekendOverwork = outputValue;
            if (double.TryParse(raw.HolidayOverwork, out outputValue)) this.HolidayOverwork = outputValue;
	    }
        
    }

    public class People
    {
        public string Name { get; set; }

        public string Department { get; set; }

        public List<Record> RecordList { get; set; }

        public People()
        {
            RecordList = new List<Record>();
        }

        public double TotalNormalWork     { get { return this.RecordList.Sum(x => x.NormalWork); } }
        public double TotalNormalOverwork { get { return this.RecordList.Sum(x => x.NormalOverwork); } }
        public double TotalWeekendOverwork { get { return this.RecordList.Sum(x => x.WeekendOverwork); } }
        public double TotalHolidayOverwork { get { return this.RecordList.Sum(x => x.HolidayOverwork); } }

        public string GetOutputContent()
        {
            return string.Format("部门:{0}\t姓名:{1:10}\t 正常出勤:{2}\t平时加班:{3}\t周末加班:{4}\t法定加班:{5}",
                 this.Department,
                 this.Name,
                 this.TotalNormalWork,
                 this.TotalNormalOverwork,
                 this.TotalWeekendOverwork,
                 this.TotalHolidayOverwork);
                
        }




    }
}
