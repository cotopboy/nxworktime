using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkingTimeCaculation
{
    public class WorkingTimePreprocessor
    {
        internal List<WorkingTimeRecord> Process(List<WorkingTimeRecord> recordList)
        {
            foreach (var item in recordList)
            {
                item.CheckTimeDetail.sectionList = RemoveDuplicatedItem(item.CheckTimeDetail.sectionList);

                item.CheckTimeDetail.sectionList = RemoveEveningUncheckOutItem(item.CheckTimeDetail.sectionList);

                item.CheckTimeDetail.sectionList = RemoveOneCheckItem(item.CheckTimeDetail.sectionList);
                     
            }

            return recordList;
        }

        private List<CheckTimeSection> RemoveOneCheckItem(List<CheckTimeSection> list)
        {           
            if(list.Count <= 1)
            {
                return list.Where(x =>x.IsSectionComplete).ToList();
            }
            else
            {
                return list;
            }       
        }

        private List<CheckTimeSection> RemoveEveningUncheckOutItem(List<CheckTimeSection> list)
        {
            List<CheckTimeSection> retlist = new List<CheckTimeSection>();

            foreach (var item in list)
            {
                bool isEvening = item.StartTimeSpan.HasValue && item.StartTimeSpan >= TimeSpan.FromHours(17);

                bool NoEnd = !item.EndTimeSpan.HasValue;

                if (isEvening && NoEnd)
                {
                    // 
                }
                else
                {
                    retlist.Add(item);
                }

            }

            return retlist;
        }


        private List<CheckTimeSection> RemoveDuplicatedItem(List<CheckTimeSection> list)
        {
            List<CheckTimeSection> retlist = new List<CheckTimeSection>();

            foreach (var item in list)
            {
                if (retlist.Any(x => x.RawText == item.RawText))
                {
                    // duplicated item. ignore.
                }
                else
                {
                    retlist.Add(item);
                }

            }

            return retlist;


        }
    }
}
