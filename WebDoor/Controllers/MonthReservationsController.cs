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
    public class MonthReservationsController : ApiController
    {
        public string Get(int year,int month)
        {
            ReservationManager manager = new ReservationManager();
            var defaultFreeHelmets = manager.GetMonthFreeHelmetsDefault(year, month);

            string result = JsonConvert.SerializeObject(defaultFreeHelmets);

            return result;
        }

        public int Get(int year, int month, int day)
        {
            ReservationManager manager = new ReservationManager();
            return manager.GetFreeHelmetCount(year, month, day);
        }
    }
}
