using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Configuration;

namespace oxinhem.Controllers
{
    public class HomeController : Controller
    {

        public string SendEmail(
            string Email,string password,
            string displayName,string subject,
            string EmailDestinatio,string body)
        {          
            string rtn = "";
            //Setting From , To and CC
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                mail.From = new MailAddress(Email, displayName);
                mail.To.Add(EmailDestinatio);
                mail.Subject = subject;
                mail.Body = body;
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["mailport"]);
                SmtpServer.Host = "oxinhem.se";
                //SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential(Email,password);
                //SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.EnableSsl = false;

                //SmtpServer.Send("info@oxinhem.se", "rezasafa2005@yahoo.com", "oxin", body);
                SmtpServer.Send(mail);
                //mail.From = new MailAddress(Email, displayName);
                //mail.To.Add(new MailAddress(EmailDestinatio));
                //mail.Subject = subject;
                //mail.Body = body;
                //smtpClient.Send(mail);
                rtn = "OK";
            }
            catch(Exception ex)
            {
                rtn = ex.Message;
            }
            
            return rtn;
        }

        public ActionResult Index()
        {
            //SendEmail(ConfigurationManager.AppSettings["mailemail"],
            //        ConfigurationManager.AppSettings["mailpass"], 
            //        "oxinhem co", "Request", 
            //        ConfigurationManager.AppSettings["maildestenition"], 
            //        "salam \n adads \n asasdas \n");
            ViewBag.Message = "";
            return View();
        }


        //public ActionResult smail(string body)
        //{
        //    return Json();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string name, string address, string tel, string email, 
            string message, string service, string squaremeters, string area, string[] chk)
        {
            ViewBag.Message = "Your request is submitted. our operators will contact you in next 3 hours."  ;
            var db = new Models.MyAppdbContext();
            var req = new MyModels.Request();
            bool balcon = false;
            bool storage = false;
            bool garage = false;
            if (chk != null)
            {
                for (int i = 0; i < chk.Count(); i++)
                {
                    if (chk[i] == "storage")
                        storage = true;
                    if (chk[i] == "balcony")
                        balcon = true;
                    if (chk[i] == "garage")
                        garage = true;
                }
            }
            req.areaID = int.Parse(area);
            req.serviceID = int.Parse(service);
            req.squaremeterID = int.Parse(squaremeters);
            req.FullName = name;
            req.Message = message;
            req.Tel = tel;
            req.Email = email;
            req.Balcony = balcon;
            req.Storage = storage;
            req.Garage = garage;
            req.Address1 = address;
            req.Address2 = "";

            TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            DateTime thisDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeInfo);
            req.dttm = thisDate;

            db.requests.Add(req);
            db.SaveChanges();

            string Body = "";
            Body += " Area : " + db.areas.Where(m => m.areaID == req.areaID).FirstOrDefault().Title + "\n";
            Body += " Service : " + db.services.Where(m => m.serviceID == req.serviceID).FirstOrDefault().Title + "\n";
            Body += " Squaremeter : " + db.squareMeters.Where(m => m.squaremeterID == req.squaremeterID).FirstOrDefault().Title + "\n";
            Body += " FullName : " + name + "\n";
            Body += " Message : " + message + "\n";
            Body += " Tel : " + tel + "\n";
            Body += " Email : " + email + "\n";
            Body += " Balcony : " + balcon + "\n";
            Body += " Storage : " + storage + "\n";
            Body += " Garage : " + garage + "\n";
            Body += " Address : " + address + "\n";
            Body += " Time : " + thisDate.ToString() + "\n";

            SendEmail(ConfigurationManager.AppSettings["mailemail"],
                    ConfigurationManager.AppSettings["mailpass"],
                    "oxinhem co", "Request",
                    ConfigurationManager.AppSettings["maildestenition"],
                    Body);

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string email, string subject, string message)
        {
            ViewBag.Message = "Your Message is submitted. our operators will contact you as soon as possible.";
            var db = new Models.MyAppdbContext();
            var cont = new MyModels.Contact();

            TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            DateTime thisDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeInfo);
            cont.dttm = thisDate;

            //cont.dttm = DateTime.Now;
            cont.FullName = name;
            cont.Message = message;
            cont.Subject = subject;
            cont.EmailAddress = email;

            db.contacts.Add(cont);
            db.SaveChanges();

            string Body = "";
            Body += " Subject : " + subject + "\n";
            Body += " FullName : " + name + "\n";
            Body += " Message : " + message + "\n";
            Body += " Email : " + email + "\n";
            Body += " Time : " + thisDate.ToString() + "\n";
            //ConfigurationManager.AppSettings["mailhost"],
            //        int.Parse(ConfigurationManager.AppSettings["mailport"]),
            SendEmail(
            ConfigurationManager.AppSettings["mailemail"],
            ConfigurationManager.AppSettings["mailpass"],
            "oxinhem", "Contact us",
            ConfigurationManager.AppSettings["maildestenition"],
            Body
            );

            return View();
        }

        public ActionResult Cleaning()
        {
            return View();
        }

        public ActionResult Painting()
        {
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

    }

    public class CultureController : Controller
    {
        // GET: Culture
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }

    public class CultureFilter : IAuthorizationFilter
    {
        private readonly string defaultCulture;

        public CultureFilter(string defaultCulture)
        {
            this.defaultCulture = defaultCulture;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var values = filterContext.RouteData.Values;

            string culture = (string)values["culture"] ?? this.defaultCulture;

            CultureInfo ci = new CultureInfo(culture);

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }

    }

}