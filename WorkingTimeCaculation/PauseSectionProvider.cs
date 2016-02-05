using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkingTimeCaculation
{
   public  class PauseSectionProvider
   {
       public List<PauseSection> GetPauseSection(DateTime refDateTime)
       { 
           List<PauseSection> retList = new List<PauseSection> ();

           if (refDateTime.Month >= 10 || refDateTime.Month <= 4) // winter
           {
               retList.Add(new PauseSection {Start = TimeSpan.FromHours(12),End= TimeSpan.FromHours(13.5)});
               retList.Add(new PauseSection { Start = TimeSpan.FromHours(17.5), End = TimeSpan.FromHours(19) }); 
           }
           else // summer
           {
               retList.Add(new PauseSection { Start = TimeSpan.FromHours(12), End = TimeSpan.FromHours(14) });
               retList.Add(new PauseSection { Start = TimeSpan.FromHours(18), End = TimeSpan.FromHours(19)+ TimeSpan.FromMinutes(10) }); 
           }

           return retList;
       }

       public TimeSpan GetAfternoonStart(DateTime refDateTime)
       {
           if (refDateTime.Month >= 10 || refDateTime.Month <= 4) // winter
           {
               return TimeSpan.FromHours(13.5);
           }
           else // summer
           {
               return TimeSpan.FromHours(14);
           }
       }

       public TimeSpan GetEveningStart(DateTime refDateTime)
       {
           if (refDateTime.Month >= 10 || refDateTime.Month <= 4) // winter
           {
               return TimeSpan.FromHours(19);
           }
           else // summer
           {
               return TimeSpan.FromHours(19) + TimeSpan.FromMinutes(10);
           }
       }
       
   }

    public class PauseSection
    {
        public TimeSpan Start{get;set;}

        public TimeSpan End{get;set;}

        public TimeSpan Length
        {
            get { return End - Start; } 
        }

    }

}
