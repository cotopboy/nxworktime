using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Math.ExtensionMethods;


namespace WorkingTimeCaculation
{
    public class CheckTimeParser
    {

        public CheckTimeDetails Parse(string input)
        {
            CheckTimeDetails checkTimeDetails = new CheckTimeDetails();

            checkTimeDetails.RawText = input;

            string [] array = input.Split(' ');


            foreach (var item in array)
            {
                if (!item.IsNullOrEmpty())
                {
                    CheckTimeSection section = CheckTimeSection.Parse(item);
                    checkTimeDetails.AddSection(section);
                }
            }

            return checkTimeDetails;

        }

    }

    public class CheckTimeDetails
    {
        public string RawText { get; set; }

        public List<CheckTimeSection> sectionList { get; set; }

        public CheckTimeDetails()
        {
            sectionList = new List<CheckTimeSection>();
        }

        public void AddSection(CheckTimeSection section)
        {
            this.sectionList.Add(section);
        }

        public bool IsSectionClear
        {
            get { return !sectionList.Any(x => !x.IsSectionComplete); }
        }

        public CheckTimeSection GetMorningSection()
        {
            var item = this.sectionList.FirstOrDefault(x => x.StartTimeSpan <= TimeSpan.FromHours(14)
                                                    && x.EndTimeSpan <= TimeSpan.FromHours(14));

            return item; 
        }

        public bool IsTXXTPattern
        { 
            get{
                return this.sectionList.Count == 2
                    && this.sectionList[0].StartTimeSpan.HasValue
                    && !this.sectionList[0].EndTimeSpan.HasValue
                    && !this.sectionList[1].StartTimeSpan.HasValue
                    && this.sectionList[1].EndTimeSpan.HasValue;
            
            }
        }

        public bool IsTTXTPattern
        {
            get
            {
                return this.sectionList.Count == 2
                    && this.sectionList[0].StartTimeSpan.HasValue
                    && this.sectionList[0].EndTimeSpan.HasValue
                    && !this.sectionList[1].StartTimeSpan.HasValue
                    && this.sectionList[1].EndTimeSpan.HasValue;

            }
        }

        public bool IsTXTXPattern
        {
            get 
            {
                return this.sectionList.Count == 2
                 && this.sectionList[0].StartTimeSpan.HasValue
                 && !this.sectionList[0].EndTimeSpan.HasValue
                 && this.sectionList[1].StartTimeSpan.HasValue
                 && !this.sectionList[1].EndTimeSpan.HasValue;
            }
        }

        public bool IsTTXTTTPattern
        {
            get
            {
                return this.sectionList.Count == 3
                 && this.sectionList[0].StartTimeSpan.HasValue
                 && this.sectionList[0].EndTimeSpan.HasValue
                 && !this.sectionList[1].StartTimeSpan.HasValue
                 && this.sectionList[1].EndTimeSpan.HasValue
                 && this.sectionList[2].StartTimeSpan.HasValue
                 && this.sectionList[2].EndTimeSpan.HasValue;
            }
        }

        public bool IsTXXTTT
        {
            get
            {
                return this.sectionList.Count == 3
                 && this.sectionList[0].StartTimeSpan.HasValue
                 && !this.sectionList[0].EndTimeSpan.HasValue
                 && !this.sectionList[1].StartTimeSpan.HasValue
                 && this.sectionList[1].EndTimeSpan.HasValue
                 && this.sectionList[2].StartTimeSpan.HasValue
                 && this.sectionList[2].EndTimeSpan.HasValue;
            }
        }

        public bool IsTTXTXT
        {
            get
            {
                return this.sectionList.Count == 3
                 && this.sectionList[0].StartTimeSpan.HasValue
                 && this.sectionList[0].EndTimeSpan.HasValue
                 && !this.sectionList[1].StartTimeSpan.HasValue
                 && this.sectionList[1].EndTimeSpan.HasValue
                 && !this.sectionList[2].StartTimeSpan.HasValue
                 && this.sectionList[2].EndTimeSpan.HasValue;
            }
        }

        public bool IsTXTTTT
        {
            get
            {
                return this.sectionList.Count == 3
                 && this.sectionList[0].StartTimeSpan.HasValue
                 && !this.sectionList[0].EndTimeSpan.HasValue
                 && this.sectionList[1].StartTimeSpan.HasValue
                 && this.sectionList[1].EndTimeSpan.HasValue
                 && this.sectionList[2].StartTimeSpan.HasValue
                 && this.sectionList[2].EndTimeSpan.HasValue;
            }
        }



        public bool IsXTTT
        {
            get
            {
                return this.sectionList.Count == 2
                 && !this.sectionList[0].StartTimeSpan.HasValue
                 && this.sectionList[0].EndTimeSpan.HasValue
                 && this.sectionList[1].StartTimeSpan.HasValue
                 && this.sectionList[1].EndTimeSpan.HasValue;
            }
        }


        public bool IsXTXT
        {
            get
            {
                return this.sectionList.Count == 2
                 && !this.sectionList[0].StartTimeSpan.HasValue
                 && this.sectionList[0].EndTimeSpan.HasValue
                 && !this.sectionList[1].StartTimeSpan.HasValue
                 && this.sectionList[1].EndTimeSpan.HasValue;
            }
        }

        public bool IsTXTT
        {
            get
            {
                return this.sectionList.Count == 2
                 && this.sectionList[0].StartTimeSpan.HasValue
                 && !this.sectionList[0].EndTimeSpan.HasValue
                 && this.sectionList[1].StartTimeSpan.HasValue
                 && this.sectionList[1].EndTimeSpan.HasValue;
            }
        }

        public bool IsXTTTTT
        {
            get 
            {
                return this.sectionList.Count == 3
               && !this.sectionList[0].StartTimeSpan.HasValue
               && this.sectionList[0].EndTimeSpan.HasValue
               && this.sectionList[1].StartTimeSpan.HasValue
               && this.sectionList[1].EndTimeSpan.HasValue
               && this.sectionList[2].StartTimeSpan.HasValue
               && this.sectionList[2].EndTimeSpan.HasValue;
            }
        }

        public bool IsTXXTXT {
            get
            {
                return this.sectionList.Count == 3
               && this.sectionList[0].StartTimeSpan.HasValue
               && !this.sectionList[0].EndTimeSpan.HasValue
               && !this.sectionList[1].StartTimeSpan.HasValue
               && this.sectionList[1].EndTimeSpan.HasValue
               && !this.sectionList[2].StartTimeSpan.HasValue
               && this.sectionList[2].EndTimeSpan.HasValue;
            }
        }

        
    }


    public class CheckTimeSection
    {
        public string Start { get; set; }

        public string End { get; set; }

        public int sectionIndex { get; set; }

        public string RawText { get; set; }

        public TimeSpan ? StartTimeSpan 
        {
            get 
            {                
                return StringToTimespan(Start);             
            }
        }

        public TimeSpan ? EndTimeSpan
        {
            get { return StringToTimespan(End); }
        }

        public TimeSpan ? GetDiffTimeSpan
        {
            get             
            {
                if (StartTimeSpan.HasValue && EndTimeSpan.HasValue)
                {
                    return EndTimeSpan - StartTimeSpan;
                }
                else
                {
                    return null;
                }
            
            }
        }

        public bool IsSectionComplete
        {
            get { return StartTimeSpan.HasValue && EndTimeSpan.HasValue; }
        }

        private TimeSpan? StringToTimespan(string inputString)
        {
            if (inputString.Contains("X")) return null;

            string [] array = inputString.Split(':');

            int hour = array[0].StrToInt();
            int mins = array[1].StrToInt();

            return new TimeSpan(hour, mins, 0);

        }

        internal static CheckTimeSection Parse(string input)
        {
            string[] array = input.Split('-');

            CheckTimeSection section = new CheckTimeSection();

            section.Start = array[0];
            section.End = array[1];
            section.RawText = input.Trim();

            return section;
        }
    }
}
/*
08:39-12:33 17:23-18:00 18:00-21:24
08:36-12:36 17:24-18:02 18:02-21:25
08:43-12:39 XX:XX-21:27 17:22-17:51
08:40-12:32 XX:XX-17:24
08:39-12:39 17:25-18:12 18:12-21:28
08:52-12:35 17:28-18:13 18:13-21:27
08:42-12:32 XX:XX-17:25
08:35-12:35 17:28-18:12 18:12-21:30
08:40-12:31 17:25-18:06 18:06-21:27
08:57-12:33 17:28-18:14 18:14-21:28
08:40-12:36 XX:XX-17:24
08:44-12:28 17:28-18:02 18:02-21:25
08:36-12:30 17:25-18:04 18:04-21:21
08:41-12:29 XX:XX-17:21
08:35-12:31 17:23-18:00 18:00-21:23
08:39-12:17 17:22-18:04 18:04-21:28
08:56-11:55
08:42-12:00 17:21-18:01 18:01-21:28
08:44-12:31 17:27-18:06 18:06-21:22
08:40-12:30 XX:XX-21:25 17:26-17:55
08:25-12:29 XX:XX-17:27
08:41-12:39 17:24-18:01 18:01-21:26

08:45-12:34 17:26-18:01 18:01-21:23
08:51-12:27 XX:XX-17:25
08:41-12:33 XX:XX-21:25 17:22-17:58
08:46-12:31 XX:XX-21:25 17:25-17:55
08:40-12:51 XX:XX-17:23
08:39-12:35 17:26-18:01 18:01-21:27
08:45-11:53 13:11-18:06 18:06-21:29

08:30-11:56 13:25-18:55 18:55-21:25
08:28-11:55 13:26-18:54 18:54-21:25
08:29-11:54 13:26-18:56 18:56-21:27
08:31-11:53 13:27-17:27
08:34-11:56 13:25-18:52 18:52-21:23
08:29-11:51 13:27-18:35 18:35-21:28
08:41-11:54 13:25-17:23
08:31-11:54 13:27-18:57 18:57-21:33
08:34-11:54 13:29-18:59 18:59-21:26
08:31-11:50 13:28-18:55 18:55-21:23
08:29-11:55 13:26-17:23
08:36-11:54 13:26-18:55 18:55-XX:XX
08:31-11:55 13:27-17:24
08:34-11:53 13:27-17:20
08:29-11:58 13:26-18:59 18:59-21:30
08:28-11:52 13:26-18:50 18:50-21:24
08:31-11:56 XX:XX-18:52 18:52-21:46
08:36-12:01 13:26-18:52 18:52-21:25
08:31-11:55 13:27-17:33 18:55-21:22
08:37-11:54 13:27-18:56 18:56-21:33
08:30-11:51 13:27-17:26
08:32-11:57 13:26-18:51 18:51-21:24
08:31-11:53 13:28-18:57 18:57-21:25
08:31-11:53 13:26-18:59 18:59-21:23
08:41-11:49 13:25-18:54 18:54-21:21
08:36-11:55 13:27-18:57 18:57-21:26
08:33-11:56 13:27-18:52 18:56-21:25
08:39-11:55 13:28-17:23
08:31-XX:XX 13:31-18:57 18:57-21:21
08:33-11:53 13:28-17:25

08:03-11:51

08:00-XX:XX XX:XX-17:28
08:05-XX:XX XX:XX-19:49 19:49-XX:XX
07:55-XX:XX XX:XX-19:49 19:49-XX:XX



08:00-XX:XX XX:XX-19:53 19:53-XX:XX
08:00-XX:XX XX:XX-19:47 19:47-XX:XX
08:00-XX:XX XX:XX-19:44 19:44-XX:XX
08:00-XX:XX XX:XX-17:14
08:02-12:45
07:53-XX:XX XX:XX-19:54 19:54-XX:XX
08:01-XX:XX XX:XX-19:51 19:51-XX:XX
07:57-XX:XX XX:XX-19:47 19:47-XX:XX
08:01-XX:XX XX:XX-19:48 19:48-XX:XX
07:58-XX:XX XX:XX-19:43 19:43-XX:XX
07:47-XX:XX XX:XX-19:49 19:49-XX:XX
07:59-XX:XX XX:XX-19:57 19:57-XX:XX
07:57-XX:XX XX:XX-20:01 20:01-XX:XX
07:55-XX:XX XX:XX-19:42 19:42-XX:XX
07:50-XX:XX XX:XX-19:50 19:50-XX:XX
08:01-XX:XX XX:XX-19:40 19:40-XX:XX
07:57-XX:XX XX:XX-19:48 19:48-XX:XX
07:58-XX:XX XX:XX-19:52 19:52-XX:XX
07:57-XX:XX XX:XX-19:52 19:52-XX:XX
07:57-XX:XX XX:XX-19:41 19:41-XX:XX
08:01-XX:XX XX:XX-19:53 19:53-XX:XX
08:00-XX:XX XX:XX-19:42 19:42-XX:XX



07:55-XX:XX XX:XX-19:53 19:53-XX:XX

07:54-XX:XX XX:XX-19:45 19:45-XX:XX



07:47-XX:XX XX:XX-19:50 19:50-XX:XX
07:49-XX:XX XX:XX-19:52 19:52-XX:XX
07:50-XX:XX XX:XX-19:53 19:53-XX:XX


07:55-XX:XX XX:XX-19:59 19:59-XX:XX
07:56-XX:XX XX:XX-19:49 19:49-XX:XX
07:50-XX:XX XX:XX-19:51 19:51-XX:XX
07:53-XX:XX XX:XX-19:52 19:52-XX:XX
07:47-XX:XX XX:XX-19:56 19:56-XX:XX
07:50-XX:XX XX:XX-19:47 19:47-XX:XX

07:57-XX:XX XX:XX-19:54 19:54-XX:XX
07:57-XX:XX XX:XX-19:51 19:51-XX:XX
07:59-XX:XX XX:XX-19:48 19:48-XX:XX
07:55-XX:XX XX:XX-19:47 19:47-XX:XX
07:50-XX:XX XX:XX-19:48 19:48-XX:XX
07:52-XX:XX XX:XX-19:52 19:52-XX:XX
07:55-XX:XX XX:XX-19:53 19:53-XX:XX
07:59-XX:XX XX:XX-19:40 19:40-XX:XX
07:59-XX:XX XX:XX-19:47 19:47-XX:XX
07:57-XX:XX XX:XX-19:48 19:48-XX:XX


07:47-XX:XX XX:XX-22:05 XX:XX-22:05
07:54-XX:XX XX:XX-17:32
07:56-XX:XX XX:XX-22:19 XX:XX-22:19
07:45-XX:XX XX:XX-17:33
07:57-XX:XX XX:XX-22:41 XX:XX-22:41
08:02-XX:XX XX:XX-17:30
08:00-XX:XX XX:XX-22:10 XX:XX-22:10
07:59-XX:XX XX:XX-17:31
07:59-XX:XX XX:XX-22:06 XX:XX-22:06
07:57-XX:XX XX:XX-17:37
08:30-XX:XX XX:XX-22:03 XX:XX-22:03
07:55-XX:XX XX:XX-17:32
07:42-XX:XX XX:XX-22:30 XX:XX-22:30

08:32-XX:XX XX:XX-17:46
08:17-XX:XX XX:XX-17:34
08:13-XX:XX XX:XX-17:31
08:14-XX:XX XX:XX-20:56 XX:XX-20:56
08:22-XX:XX XX:XX-17:37

 */