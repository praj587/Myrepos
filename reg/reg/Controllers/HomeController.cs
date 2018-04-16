using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using reg.Models;
using Rotativa;
using Rotativa.MVC;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace reg.Controllers
{
    public class HomeController : Controller
    {
        parveenEntities entities = new parveenEntities();
        public ActionResult Index()
        {
            //return View();
           
            var data = entities.tbl_Reg.Select(x => x);
            return View(data.ToList());
        }
        public ActionResult PrintAllReport()
        {
            var report = new ActionAsPdf("Index");
            return report;
        }
        public ActionResult IDcard(int id)
        {
            var cus = entities.tbl_Reg.Where(u => u.Cus_Id==id).First();
            ViewBag.Name = cus.Cus_Name;
            ViewBag.Service = cus.Cus_Services;
            using (MemoryStream ms = new MemoryStream())
            {

                string level = "Q";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(cus.Cus_Mobile, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {

                        using (Bitmap bitMap = qrCode.GetGraphic(20))
                        {
                            bitMap.Save(ms, ImageFormat.Png);
                            ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                          
                        }
                    }
                }
                // QRCodeGenerator.QRCode qrCode = new QRCode(qrCodeData)

            }


            return View(cus);
        }
        public ActionResult PrintQRcode(int id)
        {
            var report = new ActionAsPdf("IDcard", new { id = id });
            return report;
        }
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}