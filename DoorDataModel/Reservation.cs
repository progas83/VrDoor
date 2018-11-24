using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorDataModel
{
    public class Reservation
    {
        public Reservation()
        {
            this.HourReservations = new List<HourReservation>();
        }

        public Nullable<DateTime> DateReservation { get; set; }
        public int Id { get; set; }
        public List<HourReservation> HourReservations { get; set; }
    }
}
