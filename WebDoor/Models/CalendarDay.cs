using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDoor.Models
{
    public class CalendarDay
    {
        public CalendarDay()//int availableWorkStations)
        {
            //this.AvailableWorkStations = availableWorkStations;
        }
       // public int WeekOfMonth { get; set; }

      //  public int DayOfWeek { get; set; }

        public int DayOfMonth { get; set; }

        public int AvailableWorkStations { get; set; }
    }
}