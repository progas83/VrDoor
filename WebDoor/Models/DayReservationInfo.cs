using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDoor.Models
{
    public class DayReservationInfo
    {
        public DayReservationInfo(DateTime dateReservation)
        {
            this.DateReservation = dateReservation;
        }
        public DateTime DateReservation { get; set; }
        public List<HourReservationInfo> FreeHelmets { get; set; }


    }
}