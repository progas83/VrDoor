using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebDoor.Logic;

namespace WebDoor.Controllers
{
    public class MonthReservationsController : ApiController
    {
        public string Get(int year,int month)
        {
            string result = string.Empty;
            try
            {
                ReservationManager manager = new ReservationManager();
                var defaultFreeHelmets = manager.GetMonthFreeHelmetsDefault(year, month);

                result = JsonConvert.SerializeObject(defaultFreeHelmets);
            }
            catch(Exception ex)
            {
               // Logger.GetLogger().Log(ex);
            }
           

            return result;
        }

        public int Get(int year, int month, int day)
        {
            ReservationManager manager = new ReservationManager();
            return manager.GetFreeHelmetCount(year, month, day);
        }
    }
}
