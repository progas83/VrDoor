using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorDataModel;

namespace WebDoor.Logic
{
    public class WorkingPlanCaсhe
    {
        private static Dictionary<DayOfWeek, int> workingHours = new Dictionary<DayOfWeek, int>();

        private static WorkingPlan regularDay;

        private static WorkingPlan freeDay;
        static WorkingPlanCaсhe()
        {
            int regularDayHours = 9;
            int weekEndHours = 14;
            workingHours.Add(DayOfWeek.Monday, regularDayHours);
            workingHours.Add(DayOfWeek.Tuesday, regularDayHours);
            workingHours.Add(DayOfWeek.Wednesday, regularDayHours);
            workingHours.Add(DayOfWeek.Thursday, regularDayHours);
            workingHours.Add(DayOfWeek.Friday, regularDayHours);
            workingHours.Add(DayOfWeek.Saturday, weekEndHours);
            workingHours.Add(DayOfWeek.Sunday, weekEndHours);

            regularDay = new WorkingPlan();
            regularDay.StartWorking = new TimeSpan(14, 0, 0);
            regularDay.EndWorking = new TimeSpan(22,0,0);


            freeDay = new WorkingPlan();
            freeDay.StartWorking = new TimeSpan(10, 0, 0);
            freeDay.EndWorking = new TimeSpan(23, 0, 0);
        }

        public static int GetWorkingHours(DayOfWeek dayOfWeek)
        {
            return workingHours[dayOfWeek];
        }

        public static WorkingPlan GetDayWorkingPlan(DayOfWeek dayOfWeek)
        {
            WorkingPlan result = regularDay;
            if(dayOfWeek == DayOfWeek.Sunday || dayOfWeek==DayOfWeek.Saturday)
            {
                result = freeDay;
            }
            return result;
        }
    }
}