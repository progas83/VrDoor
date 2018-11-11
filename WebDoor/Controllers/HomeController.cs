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
            using (var db = new DoorDbContext())
            {
                var res = db.PARAMS.ToList();
            }

                return View();
        }
    }
}
