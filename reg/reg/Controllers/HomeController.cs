using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using reg.Models;

namespace reg.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return View();
            parveenEntities entities = new parveenEntities();
            return View(from user in entities.tbl_Reg.Take(10)
                        select user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}