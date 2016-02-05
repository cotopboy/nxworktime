using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.Configuration;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Math.ExtensionMethods;

namespace WorkingTimeCaculation
{
    public class DateTimeSorter
    {
        private FileDbEngine<HolidayConfig> dbEngine = new FileDbEngine<HolidayConfig>("holidays", ".xml");

        private HolidayConfig db;

        public DateTimeSorter()
        {
            this.db = dbEngine.LoadFileDB();
        }

        public string GetDateType(DateTime input)
        {
            if (IsSpecialData(input)) return "Holiday";

            if (IsWeekend(input)) return "Weekend";

            return "Normal";
        }

        private bool IsWeekend(DateTime input)
        {
            var holidaySet = GetHolidaySet(input);

            return input.IsWeekEnd() || holidaySet.GetShiftedWeekendDates().Contains(input);
        }

        private bool IsSpecialData(DateTime input)
        {
            var holidaySet = GetHolidaySet(input);

            return holidaySet.GetDateList().Contains(input);
        }

        private HolidaySet GetHolidaySet(DateTime input)
        {
            int year = input.Year;

            var holidaySet = db.HolidaySets.FirstOrDefault(x => x.Year == year);

            if (holidaySet == null) Console.WriteLine("Error (holidaySet == null)");
            return holidaySet;
        }
    }

    [Serializable]
    public class HolidayConfig
    {
        public List<HolidaySet> HolidaySets { get; set; }
    }

    [Serializable]
    public class HolidaySet
    {
        public int Year { get; set; }

        public DateTime NewYearDay            { get; set; }        
        public DateTime SpringFestivalDays1   { get; set; }
        public DateTime SpringFestivalDays2   { get; set; }
        public DateTime SpringFestivalDays3   { get; set; }
        public DateTime QingMingFestivalDay   { get; set; }
        public DateTime LaborDay              { get; set; }
        public DateTime DragonBoatFestivalDay { get; set; }
        public DateTime MidAutumnFestivalDay  { get; set; }

        public List<DateTime> GetDateList()
        {
            List<DateTime> list = new List<DateTime>();
            
            list.Add(NewYearDay           );
            list.Add(SpringFestivalDays1  );
            list.Add(SpringFestivalDays2  );
            list.Add(SpringFestivalDays3  );
            list.Add(QingMingFestivalDay  );
            list.Add(LaborDay             );
            list.Add(DragonBoatFestivalDay);
            list.Add(MidAutumnFestivalDay);

            return list;
        }

        public List<DateTime> GetShiftedWeekendDates()
        {
            List<DateTime> shiftedWeekendList = new List<DateTime>();

            List<DateTime> springList = new List<DateTime>() { SpringFestivalDays1, SpringFestivalDays2, SpringFestivalDays3 };

            int springWeekendCount = springList.Count(x => x.DayOfWeek == DayOfWeek.Sunday || x.DayOfWeek == DayOfWeek.Saturday);

            for (int i = 1; i <= springWeekendCount; i++)
            {
                 DateTime temp = SpringFestivalDays3.AddDays(i);
                 
                 if(temp.DayOfWeek == DayOfWeek.Sunday)
                 {
                    temp = temp.AddDays(1);
                 }

                 shiftedWeekendList.Add(temp);
                
            }

            List<DateTime> otherHolidays = new List<DateTime>() 
            {
                NewYearDay             ,
                QingMingFestivalDay    ,
                LaborDay               ,
                DragonBoatFestivalDay  ,
                MidAutumnFestivalDay   ,
            };

            foreach (var item in otherHolidays)
            {
                if (item.DayOfWeek == DayOfWeek.Sunday)
                {
                    shiftedWeekendList.Add(item.AddDays(1));
                }
                else if( item.DayOfWeek == DayOfWeek.Saturday)
                {
                    shiftedWeekendList.Add(item.AddDays(2));
                }
            }

            return shiftedWeekendList;

        }

    }
    /*
        NormalWork { get; set; }

        public double NormalOverwork { get; set; }

        public double WeekendOverwork { get; set; }

        public double HolidayOverwork { get; set; }
     */

}
