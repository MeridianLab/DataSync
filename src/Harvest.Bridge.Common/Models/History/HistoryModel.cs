using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models.History
{
    public class HistoryModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string LogLevel { get; set; }

        public string StepDetails { get; set; }
    }
}
