using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using reg.Models;

namespace reg.Controllers
{
    public class RegController : Controller
    {
        // GET: Reg
        public ActionResult Index()
        {
           


            return View();
        }
        [ValidateInput(true)]
        [HttpPost]
        public ActionResult Register(tbl_Reg regdata)
        {
            if (!ModelState.IsValid)
            {
                //var Mobileno = Convert.ToString(Request["txtAmount"].ToString());
                using (var context = new regmodelcontext())
                {
                    var user = context.regusers.Where(u => u.Cus_Mobile == regdata.Cus_Mobile).FirstOrDefault();

                    if (user == null)
                        return View("Success");
                    else
                    {
                        return View("Failure");
                    }
                }
            }
            return View("Success");
        }
        public ViewResult Success()
        {
            return View();
        }

        public ActionResult Failure()
        {
            return View();
        }
    }
}