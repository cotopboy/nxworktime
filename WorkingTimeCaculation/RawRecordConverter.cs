using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace WorkingTimeCaculation
{
     class RawRecordConverter
     {

        private CheckTimeParser CheckTimeParser = new CheckTimeParser ();

        internal List<WorkingTimeRecord> ConvertRawRecord(List<RawRecord> rawRecordList)
        {
            List<WorkingTimeRecord> retList = new List<WorkingTimeRecord>();

            foreach (var item in rawRecordList)
            {
                WorkingTimeRecord temp = new WorkingTimeRecord();

                temp.Name = item.Name;
                temp.Department = item.Department;
                temp.Date = DateTime.ParseExact(item.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                temp.CheckTimeDetail =  CheckTimeParser.Parse(item.WorkingTimeRecordRawText);
                retList.Add(temp);

            }
            return retList;
        }
    }
}
