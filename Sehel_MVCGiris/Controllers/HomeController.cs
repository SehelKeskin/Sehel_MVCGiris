using Sehel_MVCGiris.Data;
using Sehel_MVCGiris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sehel_MVCGiris.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Aşağıdaki formu doldurarak bizimle ietişime geçebilirsiniz.";

            return View();
        }

        public ActionResult Projelerim()
        {
            using (var db = new ApplicationDbContext())
            {
                var projects = db.Projects.ToList();
                return View(projects);

            }
        }

        public ActionResult Kvkk()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult Projects()
        {
            using (var db=new ApplicationDbContext())
            {
                var projects = db.Projects.ToList();
                return View(projects);

            }
        }
 

        [HttpPost]//post işleminde çalışıyor.
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)//hata var mı kontrol ediyor dataanationda.
            {
                try {
                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

                    mailMessage.From = new System.Net.Mail.MailAddress("sehelcigdem@gmail.com", "Sehel KESKİN");
                    mailMessage.Subject = "İletişim Formu: " + model.FirstName + " ";

                    mailMessage.To.Add("sehelcigdem@gmail.com");
                    string body;
                    body = "Ad Soyad: " + model.FirstName + " " + model.LastName + "<br />";
                    body += "E-posta: " + model.Email + "<br />";
                    body += "Telefon: " + model.Phone + "<br />";
                    body += "Mesaj: " + model.Message + "<br />";
                    body += "Tarih: " + DateTime.Now.ToString("dd MMMM yyyy") + "<br />";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = body;

                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new System.Net.NetworkCredential("sehelcigdem@gmail.com", "Allah123");
                    smtp.EnableSsl = true;
                    smtp.Send(mailMessage);
                    ViewBag.Message = "Mesajınız gönderildi. Teşekkür ederiz.";
                
                }
              catch
                {
                    ViewBag.Error = "Form gönderimi başarısız oldu tekrar deneriniz.";

                }
        }
            return View(model);
        }



    }
}