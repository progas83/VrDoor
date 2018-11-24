using System;
using System.Web.Http;
using WebDoor.Logic;

namespace WebDoor.Controllers
{
    public class ReservationAPIController : ApiController
    {
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public string Get(DateTime date)
        {
            ReservationManager reservationManager = new ReservationManager();
            var monthReservationInfo = reservationManager.CalculateReservations(date.Year, date.Month);

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(monthReservationInfo);
            return result;
        }

        public string Get()
        {
            // ReservationManager reservationManager = new ReservationManager();
            // reservationManager.CalculateReservations()
            return this.Get(DateTime.Now);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}