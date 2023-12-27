using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models.History
{
    public class HistoryStepModel
    {
        public Guid Id { get; set; }
        public Guid BridgeId { get; set; }
        public string StepName { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
        public string ProjectName { get; set; }
        public string LogLevel { get; set; }
    }
}
