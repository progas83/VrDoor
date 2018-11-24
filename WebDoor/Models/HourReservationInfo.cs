using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDoor.Models
{
    public class HourReservationInfo
    {
        public HourReservationInfo(int startHour, int freeHelmets)
        {
            this.StartHour = startHour;
            this.FreeHelmets = freeHelmets;
        }
        public int StartHour { get; set; }
        public int FreeHelmets { get; set; }
    }
}