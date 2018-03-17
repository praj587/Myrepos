using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using reg.Models;
using System.IO;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Text;
using System.Net;

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
            if (ModelState.IsValid)
            {
                //var Mobileno = Convert.ToString(Request["txtAmount"].ToString());
                using (var context = new regmodelcontext())
                {
                    var user = context.regusers.Where(u => u.Cus_Mobile == regdata.Cus_Mobile).FirstOrDefault();

                    if (user == null)
                    {
                        context.regusers.Add(regdata);
                        context.SaveChanges();
                       // return View("Success");
                        return RedirectToAction("Success", new { Qrimput = regdata.Cus_Mobile});
                    }
                    else
                    {
                        return View("Failure");
                    }
                }
            }
            return View("Success");
        }
        public ActionResult Success(string Qrimput)
        {
            using (MemoryStream ms = new MemoryStream())
            {

                string level = "Q";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(Qrimput, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {

                        using (Bitmap bitMap = qrCode.GetGraphic(20))
                        {
                            bitMap.Save(ms, ImageFormat.Png);
                            ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                            mailsend(ViewBag.QRCodeImage);
                        }
                    }
                }
               // QRCodeGenerator.QRCode qrCode = new QRCode(qrCodeData)
               
            }
            return View();
        }

        public ActionResult Failure()
        {
            return View();
        }

        protected void mailsend(string img)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("slogicsolutions@gmail.com", "Admin123$");
                String msg = "<html><body>this is a <img src=\"" + img + "\"></body></html>";
                MailMessage mm = new MailMessage("slogicsolutions@gmail.com", "deva_v@ymail.com", "test", "test");
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);


            }

            catch (Exception ex)
            { Response.Write("Failed"); }
        }
    }
}