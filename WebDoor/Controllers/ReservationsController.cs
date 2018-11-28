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
        /// <summary>
        /// Gets data of reservations by current date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public string Get(int year, int month, int day)
        {
            ReservationManager manager = new ReservationManager();
            var dayReservations = manager.GetDayReservation(year, month, day);
            string result = JsonConvert.SerializeObject(dayReservations);

            return result;
        }

        /// <summary>
        /// Put reservation for current date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="reservedCount"></param>
        /// <param name="visitorData"></param>
        public void Put(int year, int month, int day, int reservedCount, string visitorData, string visitorName)
        {
        }

    }
}
