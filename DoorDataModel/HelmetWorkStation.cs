using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorDataModel
{
    public class HelmetWorkStation
    {
        public HelmetWorkStation()
        {
            this.Reservations = new List<Reservation>();
        }
        public int Id { get; set; }

        [Index(IsUnique =true)]
        public int WorkStationNumber { get; set; }

        public int HelmetTypeId { get; set; }

        public PARAM HelmetType { get; set; }

        public virtual List<Reservation> Reservations { get; set; }

        [NotMapped]
        public  List<PARAM> AvailableReservationTypes { get; set; }

        [NotMapped]
        public PARAM ReservedType { get; set; }
    }
}
