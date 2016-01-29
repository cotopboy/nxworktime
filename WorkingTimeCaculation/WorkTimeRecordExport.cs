using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkingTimeCaculation
{
    public class WorkTimeRecordExport
    {
        public string Department { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public string WorkRecordRawContent { get; set; }

        public double NormalWork { get; set; }

        public double NormalOverwork { get; set; }

        public double WeekendOverwork { get; set; }

        public double HolidayOverwork { get; set; }

    }
}
