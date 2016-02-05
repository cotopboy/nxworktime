using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Math.ExtensionMethods;


namespace WorkingTimeCaculation
{
    public class WorkingTimeRecord
    {
        public string Department { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string WorkRecordRawContent { get; set; }

        public CheckTimeDetails CheckTimeDetail { get; set; }

        public WorkingResultSet ResultSet { get; set; }

        public TimeSpan TotalWorkTime { get; set; }

        public bool IsProcessed { get; set; }

        public WorkingTimeRecord()
        {
            ResultSet = new WorkingResultSet();
        }
    }

    public class WorkingResultSet
    {


        public double NormalWork { get; set; }

        public double NormalOverwork { get; set; }

        public double WeekendOverwork { get; set; }

        public double HolidayOverwork { get; set; }

    }
}
