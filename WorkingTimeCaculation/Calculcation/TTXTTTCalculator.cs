using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkingTimeCaculation.Calculcation
{
    class TTXTTTCalculator
    {
        private PauseSectionProvider pauseProvider = new PauseSectionProvider();

        internal void Calculcate(WorkingTimeRecord record)
        {
            TimeSpan total = record.CheckTimeDetail.sectionList[0].GetDiffTimeSpan.Value;
            total += record.CheckTimeDetail.sectionList[2].GetDiffTimeSpan.Value;

            TimeSpan midEnd = record.CheckTimeDetail.sectionList[1].EndTimeSpan.Value;

            TimeSpan lastEnd = record.CheckTimeDetail.sectionList[2].EndTimeSpan.Value;

            TimeSpan minStart = TimeSpan.FromSeconds(0);
            if (midEnd > lastEnd)
            {   // mid evening
                 minStart = pauseProvider.GetEveningStart(record.Date);
            }
            else
            {  // mid afternoon
                 minStart = pauseProvider.GetAfternoonStart(record.Date);
            }

            TimeSpan workTimeMid = midEnd - minStart;

            total = +workTimeMid;


            record.IsProcessed = true;

            record.TotalWorkTime = total;

        }
    }
}
