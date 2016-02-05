using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkingTimeCaculation.Calculcation
{
    public class SimpleWorkTimeCalculator
    {
        internal void Calculate(WorkingTimeRecord record)
        {
            TimeSpan sum = TimeSpan.FromSeconds(0);


            foreach (var item in record.CheckTimeDetail.sectionList)
            {
                if (item.GetDiffTimeSpan.HasValue)
                {
                    sum += item.GetDiffTimeSpan.Value;
                }
            }


            record.IsProcessed = true;
            record.TotalWorkTime = sum;
        }
    }
}
