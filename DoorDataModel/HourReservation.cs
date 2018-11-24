using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorDataModel
{
    public class HourReservation
    {
        public HourReservation()
        {
            this.HelmetWorkStations = new List<HelmetWorkStation>();
        }

        public int Id { get; set; }
        public int VisitorId { get; set; }

        public virtual Visitor Visitor { get; set; }

        public TimeSpan StartReservation { get; set; }

        public TimeSpan EndReservation { get; set; }

        public List<HelmetWorkStation> HelmetWorkStations { get; set; }

    }
}
