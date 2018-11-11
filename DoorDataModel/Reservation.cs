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
            this.HelmetWorkStations = new List<HelmetWorkStation>();
        }
        public int Id { get; set; }

        public int VisitorId { get; set; }

        public Visitor Visitor { get; set; }

        //public int HelmetId { get; set; }

        //public HelmetWorkStation Helmet { get; set; }

        public DateTime StartReservation { get; set; }

        public DateTime EndReservation { get; set; }

        public List<HelmetWorkStation> HelmetWorkStations { get; set; }
    }
}
