using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkingTimeCaculation.Calculcation
{
    class TXTTTTCalculator
    {
        private PauseSectionProvider pauseProvider = new PauseSectionProvider();

        internal void Calculcate(WorkingTimeRecord record)
        {
            var list = record.CheckTimeDetail.sectionList;

            TimeSpan start = list[0].StartTimeSpan.Value;

            TimeSpan morningTime = TimeSpan.FromHours(12) - start;

            record.TotalWorkTime = morningTime + list[1].GetDiffTimeSpan.Value +  list[2].GetDiffTimeSpan.Value;

            record.IsProcessed = true;
        }

    }
}
