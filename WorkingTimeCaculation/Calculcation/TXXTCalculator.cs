using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkingTimeCaculation.Calculcation
{
    class TXXTCalculator
    {
        private PauseSectionProvider pauseProvider = new PauseSectionProvider();

        internal void Calculate(WorkingTimeRecord record)
        {
            TimeSpan start = record.CheckTimeDetail.sectionList[0].StartTimeSpan.Value;

            TimeSpan end = record.CheckTimeDetail.sectionList[1].EndTimeSpan.Value;

            var pauseList = pauseProvider.GetPauseSection(record.Date);

            TimeSpan pauseSum = TimeSpan.FromSeconds(0);

            foreach (var pause in pauseList)
            {

                if (pause.Start >= start && pause.End <= end)
                {
                    pauseSum += pause.Length;
                }

            }

            record.IsProcessed = true;

            record.TotalWorkTime = end - start - pauseSum;


        }
    }
}
