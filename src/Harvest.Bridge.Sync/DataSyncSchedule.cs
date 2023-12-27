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
    public class DataSyncSchedule
    {
        public List<ScheduleModel> Schedules { get; set; }

        public ScheduleModel CalculateNextRunTime()
        {
            return CalculateNextRunTime(DateTime.Now);
        }
        public ScheduleModel CalculateNextRunTime(DateTime datetime)
        {
            ScheduleModel retSchedule = null;
            LoadSchedule();
            int crntDay = (int)datetime.DayOfWeek;
            // Find first day for schedule
            retSchedule = FindNextEnabledTimeSlot(crntDay);
            if (retSchedule != null)
            {
                // Make sure we aren't past defined end time, if so get next day
                int nextScheduledHour = datetime.AddMinutes(retSchedule.FrequencyInMinutes).Hour;
                if (nextScheduledHour >= retSchedule.EndTime || retSchedule.NextScheduleRunTime.Hour >= retSchedule.EndTime)
                {
                    retSchedule = FindNextEnabledTimeSlot((int)retSchedule.DefaultForActualDayOfWeek + 1);
                    AdjustDayOfWeek(retSchedule, datetime);
                }
                else
                {
                    AdjustDayOfWeek(retSchedule, datetime);
                }

                if(retSchedule.NextScheduleRunTime.Hour < retSchedule.StartTime)
                {
                    if (retSchedule.NextScheduleRunTime.Hour > 0)
                    {
                        // Need to reset time to midnight
                        retSchedule.NextScheduleRunTime = retSchedule.NextScheduleRunTime.AddHours(-retSchedule.NextScheduleRunTime.Hour);
                    }
                    retSchedule.NextScheduleRunTime = retSchedule.NextScheduleRunTime.AddHours(retSchedule.StartTime);
                }
                else
                {
                    retSchedule.NextScheduleRunTime = retSchedule.NextScheduleRunTime.AddMinutes(retSchedule.FrequencyInMinutes);
                }
            }

            if (retSchedule == null)
            {
                retSchedule = new ScheduleModel() { FrequencyInMinutes = 30, NextScheduleRunTime = DateTime.MaxValue };
            }

            return retSchedule;
        }

        private void AdjustDayOfWeek(ScheduleModel crntDaySchedule, DateTime datetime)
        {
            crntDaySchedule.NextScheduleRunTime = datetime;
            int targetDayOfWeek = crntDaySchedule.DayOfWeek;
            if (crntDaySchedule.IsDefault)
            {
                targetDayOfWeek = (int)crntDaySchedule.DefaultForActualDayOfWeek;
            }
            if ((int)crntDaySchedule.NextScheduleRunTime.DayOfWeek != targetDayOfWeek)
            {
                // Need to reset time to midnight
                crntDaySchedule.NextScheduleRunTime = crntDaySchedule.NextScheduleRunTime.AddHours(-crntDaySchedule.NextScheduleRunTime.Hour);
                crntDaySchedule.NextScheduleRunTime = crntDaySchedule.NextScheduleRunTime.AddMinutes(-crntDaySchedule.NextScheduleRunTime.Minute);
                crntDaySchedule.NextScheduleRunTime = crntDaySchedule.NextScheduleRunTime.AddSeconds(-crntDaySchedule.NextScheduleRunTime.Second);
                do
                {
                    crntDaySchedule.NextScheduleRunTime = crntDaySchedule.NextScheduleRunTime.AddDays(1);
                } while ((int)crntDaySchedule.NextScheduleRunTime.DayOfWeek != targetDayOfWeek);
            }
        }

        private ScheduleModel FindNextEnabledTimeSlot(int dayOfWeek)
        {
            ScheduleModel retDaySchedule = null;
            for (int d = dayOfWeek; d <= 7; d++)
            {
                ScheduleModel dayM = Schedules.FirstOrDefault(m => m.DayOfWeek == d);
                if (dayM != null && dayM.DayEnabled)
                {
                    retDaySchedule = dayM;
                    break;
                }
            }

            if(retDaySchedule == null)
            {
                // start from beginning of week
                for (int d = 0; d <= 7; d++)
                {
                    ScheduleModel dayM = Schedules.FirstOrDefault(m => m.DayOfWeek == d);
                    if (dayM != null && dayM.DayEnabled)
                    {
                        retDaySchedule = dayM;
                        break;
                    }
                }
            }

            // Check if should return default
            if(retDaySchedule != null && retDaySchedule.UseDefault)
            {
                ScheduleModel defaultSch = Schedules.FirstOrDefault(m => m.DayOfWeek == -1);
                defaultSch.DefaultForActualDayOfWeek = retDaySchedule.DayOfWeek;
                defaultSch.DefaultForActualDayOfWeekStr = Enum.GetName(typeof(DayOfWeek), retDaySchedule.DayOfWeek);
                retDaySchedule = defaultSch;
            }
            else
            {
                retDaySchedule.DefaultForActualDayOfWeek = retDaySchedule.DayOfWeek;
                retDaySchedule.DefaultForActualDayOfWeekStr = Enum.GetName(typeof(DayOfWeek), retDaySchedule.DayOfWeek);
            }

            return retDaySchedule;
        }

        public void LoadSchedule()
        {
            DALConfiguration dalConfig = new DALConfiguration();
            string json = dalConfig.GetConfigValue("Schedule");
            if (string.IsNullOrEmpty(json))
            {
                Schedules = new List<ScheduleModel>();
                Schedules.Add(new ScheduleModel() { DayOfWeek = -1, DayEnabled = true, StartTime = 7, EndTime = 19, FrequencyInMinutes = 30 });
                Schedules.Add(new ScheduleModel() { DayOfWeek = 0, UseDefault = true, DayEnabled = true });
                Schedules.Add(new ScheduleModel() { DayOfWeek = 1, UseDefault=true, DayEnabled=true });
                Schedules.Add(new ScheduleModel() { DayOfWeek = 2, UseDefault = true, DayEnabled = true });
                Schedules.Add(new ScheduleModel() { DayOfWeek = 3, UseDefault = true, DayEnabled = true });
                Schedules.Add(new ScheduleModel() { DayOfWeek = 4, UseDefault = true, DayEnabled = true });
                Schedules.Add(new ScheduleModel() { DayOfWeek = 5, UseDefault = true, DayEnabled = true });
                Schedules.Add(new ScheduleModel() { DayOfWeek = 6, UseDefault = true, DayEnabled = true });
            }
            else
            {
                Schedules = (List<ScheduleModel>)JsonCommonHelper.DeserializeObject(json, typeof(List<ScheduleModel>));
            }
        }

        public void SaveSchedule()
        {
            string json = JsonCommonHelper.SerializeObject(Schedules);
            DALConfiguration dalConfig = new DALConfiguration();
            dalConfig.UpdateConfig("Schedule", json);

            LoadSchedule();
        }

        public void SaveNextScheduleInfo(ScheduleModel nextSchedule)
        {
            DALConfiguration dalConfig = new DALConfiguration();
            dalConfig.UpdateConfig("RuntimeSync_NextScheduledRuntime", nextSchedule.NextScheduleRunTime.ToString());
            dalConfig.UpdateConfig("RuntimeSync_FrequencyInMinutes", nextSchedule.FrequencyInMinutes.ToString());
        }
    }
}
