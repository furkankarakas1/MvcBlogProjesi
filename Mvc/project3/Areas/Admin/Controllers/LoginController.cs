using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project3.Models;
using System.Web.Security;
namespace project3.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alogin(kullanicilar kullaniciFormu, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("index", kullaniciFormu);
            }

            using(kahve2020Entities db = new kahve2020Entities())
            {
                var kullaniciVarmi = db.kullanicilar.FirstOrDefault(
                    x => x.kad == kullaniciFormu.kad && x.sifre == kullaniciFormu.sifre);
                if(kullaniciVarmi != null)
                {
                    FormsAuthentication.SetAuthCookie(kullaniciVarmi.kad, kullaniciFormu.BeniHatirla);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("/index", "urunler");
                    }

                    
                }
                ViewBag.Hata = "Kullanici adi veya sifre hatalı";
                return View("index");
            }
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index");
        }
    }
}