using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorDataModel;

namespace WebDoor.Logic
{
    public class TimeReservation
    {
      

        public TimeReservation(HourReservation hourReservation, int activeHelmets)
        {
            this.StartTime = hourReservation.StartReservation;
            this.EndTime = hourReservation.EndReservation;
            this.AvailableHelmets = this.CountAvailableHelmets(hourReservation.HelmetWorkStations, activeHelmets);
        }

        private int CountAvailableHelmets(List<HelmetWorkStation> busyHelments, int activeHelmets)
        {
            if(busyHelments == null || busyHelments.Count == 0)
            {
                return activeHelmets;
            }
            else
            {
                return busyHelments.Count - activeHelmets;
            }
        }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int AvailableHelmets { get; set; }
    }
}