using Harvest.Bridge.Common.Models;
using Harvest.Bridge.DAL;
using Harvest.Bridge.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Sync
{
    public class DataSyncFullSchedule
    {
        public ScheduleFullRunModel GetSchedule()
        {
            DALConfiguration dalConfig = new DALConfiguration();
            string json = dalConfig.GetConfigValue("FullRunSchedule");
            ScheduleFullRunModel schedule = new ScheduleFullRunModel();
            if (string.IsNullOrEmpty(json) == false)
            {
                schedule = (ScheduleFullRunModel)JsonCommonHelper.DeserializeObject(json, typeof(ScheduleFullRunModel));
            }
            return schedule;
        }

        public void SaveSchedule(ScheduleFullRunModel fullRunModel) 
        {
            string json = JsonCommonHelper.SerializeObject(fullRunModel);
            DALConfiguration dalConfig = new DALConfiguration();
            dalConfig.UpdateConfig("FullRunSchedule", json);
        }

        public ScheduleFullRunModel SaveNextFullRunSchedule(DateTime startScheduleForDt)
        {
            ScheduleFullRunModel scheduleFull = GetSchedule();
            if (scheduleFull.IsEnabled)
            {
                scheduleFull.ScheduleDays = new List<DateTime>();
                DateTime startDate = startScheduleForDt.AddDays(-scheduleFull.TotalDays).Date;
                for(int i=scheduleFull.TotalDays;i>0;i--)
                {
                    startDate = startDate.AddDays(+1);
                    scheduleFull.ScheduleDays.Add(startDate);
                }

                scheduleFull.CurrentRunningForDate = DateTime.MinValue;
                scheduleFull.NextStartDate = startScheduleForDt.Date;
                SaveSchedule(scheduleFull);
            }
            return scheduleFull;
        }
    }
}
