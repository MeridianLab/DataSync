using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Common.Models
{
    public class LogDetail
    {
        public LogDetail(CategoryEnum category, string logValue) 
        { 
            Category= category;
            LogValue= logValue;
            CreateDate = DateTime.Now;
        }
        public CategoryEnum Category { get; set; }
        public string LogLevel { get; set; }
        public string LogValue { get; set; }
        public DateTime CreateDate { get; set; }

        public override string ToString()
        {
            return $"{CreateDate.ToString()}-{LogLevel}: {LogValue}{Environment.NewLine}";
        }
    }
}
