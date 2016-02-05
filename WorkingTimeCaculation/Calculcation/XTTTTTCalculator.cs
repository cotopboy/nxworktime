using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkingTimeCaculation.Calculcation
{
    class XTTTTTCalculator
    {
        internal void Calculcate(WorkingTimeRecord record)
        {
            var list = record.CheckTimeDetail.sectionList;

            TimeSpan morningstart = list[0].EndTimeSpan.Value;

            TimeSpan morningTime = TimeSpan.FromHours(12) - morningstart;

            TimeSpan total = morningTime + list[1].GetDiffTimeSpan.Value + list[2].GetDiffTimeSpan.Value;

            record.TotalWorkTime = total;
            record.IsProcessed = true;
        }
    }
}
