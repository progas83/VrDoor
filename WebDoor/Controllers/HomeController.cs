using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoorDataModel;

namespace WebDoor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            MonthReservationsController apiTest = new MonthReservationsController();
            var result = apiTest.Get(2018,11);

            ReservationsController resController = new ReservationsController();
            var r = resController.Get(2018, 11, 28);

                return View();
        }
    }
}
