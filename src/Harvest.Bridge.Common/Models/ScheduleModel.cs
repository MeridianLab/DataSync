using Newtonsoft.Json;
using System;

namespace Harvest.Bridge.Common.Models
{
    public class ScheduleModel
    {
        public int DayOfWeek { get; set; }
        
        [JsonIgnore]
        public string Day
        {
            get
            {
                if (DayOfWeek < 0) { return "Default"; }
                else if(DayOfWeek == 0) { return System.DayOfWeek.Sunday.ToString(); }
                else if (DayOfWeek == 1) { return System.DayOfWeek.Monday.ToString(); }
                else if (DayOfWeek == 2) { return System.DayOfWeek.Tuesday.ToString(); }
                else if (DayOfWeek == 3) { return System.DayOfWeek.Wednesday.ToString(); }
                else if (DayOfWeek == 4) { return System.DayOfWeek.Thursday.ToString(); }
                else if (DayOfWeek == 5) { return System.DayOfWeek.Friday.ToString(); }
                else if (DayOfWeek == 6) { return System.DayOfWeek.Saturday.ToString(); }
                return string.Empty;
            }
        }
        public bool UseDefault { get; set; }
        public bool IsDefault { get { return DayOfWeek == -1; } }
        public bool DayEnabled { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int FrequencyInMinutes { get; set; }

        [JsonIgnore]
        public int DefaultForActualDayOfWeek { get; set; }
        [JsonIgnore]
        public string DefaultForActualDayOfWeekStr { get; set; }

        [JsonIgnore]
        public DateTime NextScheduleRunTime { get; set; }
    }
}
