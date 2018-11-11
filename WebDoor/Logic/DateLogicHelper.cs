using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDoor.Models;

namespace WebDoor.Logic
{
    public class DateLogicHelper
    {
        public int GetDaysInMonth(int year,int monthNumber)
        {
            return System.DateTime.DaysInMonth(year, monthNumber);
        }

        private readonly int lastDayOfWeekSundayNumber = 6;
        public List<CalendarWeek> GetWeeksOfMonth(int year, int monthNumber)
        {
            List<CalendarWeek> weeks = new List<CalendarWeek>();
            int daysInMonth = this.GetDaysInMonth(year, monthNumber);
            CalendarWeek tmpWeek = null;
            int weekOfMonth = 0;
            for(int i=1;i<=daysInMonth;i++)
            {
                if(tmpWeek==null)
                {
                    tmpWeek = new CalendarWeek();
                    tmpWeek.WeekOfMonth = weekOfMonth;
                }
              int dayOfWeek = (int) new DateTime(year, monthNumber, i).DayOfWeek;
                int arrayIndexOfWeekDay = dayOfWeek - 1;
                if(arrayIndexOfWeekDay<0)
                {
                    arrayIndexOfWeekDay = this.lastDayOfWeekSundayNumber;
                }

                tmpWeek.DaysOfWeek[arrayIndexOfWeekDay] = new CalendarDay() { DayOfMonth = i };

                if(arrayIndexOfWeekDay == this.lastDayOfWeekSundayNumber)
                {
                    weeks.Add(tmpWeek);
                    tmpWeek = null;
                    weekOfMonth++;
                }
            }

            if (tmpWeek != null)
            {
                weeks.Add(tmpWeek);
            }

            return weeks;
        }
    }
}