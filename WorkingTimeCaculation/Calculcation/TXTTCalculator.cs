using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkingTimeCaculation.Calculcation
{
    class TXTTCalculator
    {
        private PauseSectionProvider pauseProvider = new PauseSectionProvider();

        internal void Calculcate(WorkingTimeRecord record)
        {
            TimeSpan start = record.CheckTimeDetail.sectionList[0].StartTimeSpan.Value;

            TimeSpan morningTime = TimeSpan.FromHours(12) - start;
          
            record.TotalWorkTime = morningTime + record.CheckTimeDetail.sectionList[1].GetDiffTimeSpan.Value;

            record.IsProcessed = true;
        }
    }
}
