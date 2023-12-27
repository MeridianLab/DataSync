using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models
{
    public class ScheduleFullRunModel
    {
        public List<DateTime> ScheduleDays { get; set; }
        public DateTime LastCompleteDate { get; set; }
        public bool HasStartedTodaysRun { get; set; }
        public DateTime CurrentRunningForDate { get; set; }
        public DateTime NextStartDate { get; set; }
        public int StartTime { get; set; }
        public int TotalDays { get; set; }
        public bool IsEnabled { get; set; }
    }
}
