using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using WebDoor.Logic;

namespace WebDoor.Controllers
{
    public class ReservationsController : ApiController
    {
        public string Get(int year, int month, int day)
        {
            ReservationManager manager = new ReservationManager();
            var dayReservations = manager.GetDayReservation(year, month, day);
            string result = JsonConvert.SerializeObject(dayReservations);

            return result;
        }


    }
}
