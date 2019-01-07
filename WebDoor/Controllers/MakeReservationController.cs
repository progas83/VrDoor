using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using WebDoor.Logic;
using WebDoor.Models;

namespace WebDoor.Controllers
{
    public class MakeReservationController : ApiController
    {
        public string Post([FromBody]string data)
        {

            ReservationInfo reservation = JsonConvert.DeserializeObject<ReservationInfo>(data);
            ReservationManager rm = new ReservationManager();
            string reservationResult = rm.ReserveHelmets(reservation);
            return reservationResult;
        }

        public bool Get([FromBody]string data)
        {
            return false;
        }

        public bool Put([FromBody]string data)
        {
            string res = data.ToString();
            return false;
        }
    }
}
