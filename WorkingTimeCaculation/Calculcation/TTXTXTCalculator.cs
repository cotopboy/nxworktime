using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkingTimeCaculation.Calculcation
{
    class TTXTXTCalculator
    {
          private PauseSectionProvider pauseProvider = new PauseSectionProvider();

          internal void Calculcate(WorkingTimeRecord record)
          {
              var list = record.CheckTimeDetail.sectionList;

              TimeSpan total = list[0].GetDiffTimeSpan.Value;

              TimeSpan afternoonStart = pauseProvider.GetAfternoonStart(record.Date);

              TimeSpan eveningStart = pauseProvider.GetEveningStart(record.Date);

              total += list[1].EndTimeSpan.Value - afternoonStart;
              total += list[2].EndTimeSpan.Value - eveningStart;

              record.TotalWorkTime = total;
              record.IsProcessed = true;


          }
    }
}
