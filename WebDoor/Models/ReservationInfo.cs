using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebDoor.Models
{
  
    public class ReservationInfo
    {
        //public TimeSpan TimeStart { get; set; }

        //public TimeSpan TimeEnd { get; set; }

        public HoursReservation[] Time { get; set; }

        public int Places { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

       // [DataMember(Name = "date")]
        public DateInfo Date { get; set; }

        //[DataMember(Name = "date.day")]
        //public string day { get; set; }
    }

    [DataContract]
    public class HoursReservation
    {
        
        [DataMember(Name = "time-start")]
        public string TimeStart { get; set; }

        [DataMember(Name = "time-end")]
        public string TimeEnd { get; set; }
    }

    public class DateInfo
    {
        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}