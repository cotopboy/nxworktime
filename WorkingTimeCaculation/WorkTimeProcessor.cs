using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WorkingTimeCaculation
{
    public class WorkTimeProcessor
    {
        internal List<WorkTimeRecordExport> ProcessFile(DataTable dataTable)
        {
            List<RawRecord> rawRecordList = this.GetRawRecordList(dataTable);

            // todo : convert rawRecordList to WorkTimeRecord list;

            // preprocess WorktimeRecordEntry. remove XX:XX

            // Calculate working time .

            // re-assign working time according to Date and tocal working hours.



            var ret = new List<WorkTimeRecordExport>();
            return ret;
        }

        private List<RawRecord> GetRawRecordList(DataTable data)
        {
            List<RawRecord> rawList = new List<RawRecord>();
            for (int i = 1; i < data.Rows.Count; ++i)
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
