using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDoor.Models
{
    public class CalendarWeek
    {
        public CalendarWeek()
        {
            this.DaysOfWeek = new CalendarDay[7];
        }
        public int WeekOfMonth { get; set; }

        public CalendarDay[] DaysOfWeek { get; set; }
    }
}