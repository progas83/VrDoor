using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorDataModel
{
    public class Visitor
    {
        public Visitor()
        {
            this.Reservations = new List<Reservation>();
        }
        public int Id { get; set; }

        public string UniqueTelephoneNumber { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Pwd { get; set; }

        public int DiscountParameterId { get; set; }

        public PARAM DiscountParameter { get; set; }

        public virtual List<Reservation> Reservations { get; set; }
    }
}
