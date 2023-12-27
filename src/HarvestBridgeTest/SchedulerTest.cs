using Harvest.Bridge.Common.Models;
using Harvest.Bridge.Sync;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvest.Bridge.Test
{
    [TestClass]
    public class SchedulerTest
    {
        public SchedulerTest() { }

        [TestMethod]
        public void SyncSchedulerTest()
        {
            // 11/20/2023 7:07:13 PM
            DataSyncSchedule dataSyncSchedule = new DataSyncSchedule();

            ScheduleModel nextSchedule = dataSyncSchedule.CalculateNextRunTime();
            Console.WriteLine(nextSchedule.NextScheduleRunTime.ToString());

            DateTime dtTest = DateTime.Parse("11/20/2023 6:37:09 PM");
            nextSchedule = dataSyncSchedule.CalculateNextRunTime(dtTest);
            Assert.AreEqual(nextSchedule.NextScheduleRunTime.Hour, 7);
            Console.WriteLine(nextSchedule.NextScheduleRunTime.ToString());

        }

        [TestMethod]
        public void FullRunScheduleTest()
        {
            DataSyncFullSchedule fullDataSync = new DataSyncFullSchedule();
            ScheduleFullRunModel scheduleFullRun = fullDataSync.GetSchedule();
            Assert.IsNotNull(scheduleFullRun);

            scheduleFullRun.TotalDays = 5;
            scheduleFullRun.StartTime = 9; // 8 pm
            scheduleFullRun.IsEnabled = true;

            fullDataSync.SaveSchedule(scheduleFullRun);
            scheduleFullRun = fullDataSync.GetSchedule();
            Assert.AreNotEqual(0, scheduleFullRun.StartTime);

            fullDataSync.SaveNextFullRunSchedule(DateTime.Now);

            scheduleFullRun = fullDataSync.GetSchedule();
            Assert.IsNotNull(scheduleFullRun.ScheduleDays);
        }

        [TestMethod]
        public void CheckTimeToRunFullRun()
        {
            DataSyncFullSchedule fullDataSync = new DataSyncFullSchedule();
            ScheduleFullRunModel scheduleFullRun = fullDataSync.GetSchedule();
            Assert.IsNotNull(scheduleFullRun);

            fullDataSync.SaveSchedule(scheduleFullRun);

            for (int x = 0; x < 10; x++)
            {
                // Should be running so should not do anything
                HandleSchedule();

                scheduleFullRun = fullDataSync.GetSchedule();
                fullDataSync.SaveSchedule(scheduleFullRun);

                HandleSchedule();
            }
        }

        private void HandleSchedule()
        {
            DataSyncFullSchedule dataSyncFull = new DataSyncFullSchedule();
            ScheduleFullRunModel schedule = dataSyncFull.GetSchedule();

            if (schedule.IsEnabled && DateTime.Now > schedule.NextStartDate &&
    DateTime.Now.Hour >= schedule.StartTime)
            {
                if (schedule.LastCompleteDate < DateTime.Now)
                {
                    // Find index of current running date so we can go to next
                    int index = schedule.ScheduleDays.IndexOf(schedule.CurrentRunningForDate.Date) + 1;
                    if (index < schedule.ScheduleDays.Count)
                    {
                        schedule.CurrentRunningForDate = schedule.ScheduleDays[index];
                        System.Diagnostics.Debug.WriteLine($"Time To start process for day {schedule.CurrentRunningForDate} Index:{index}");
                        new DataSyncFullSchedule().SaveSchedule(schedule);

                        System.Diagnostics.Debug.WriteLine("Run catchup for date:" + schedule.CurrentRunningForDate);
                    }
                    else
                    {
                        // Done
                        schedule = new DataSyncFullSchedule().SaveNextFullRunSchedule(DateTime.Now.AddDays(1));
                    }

                }
            }
        }
    }
}
