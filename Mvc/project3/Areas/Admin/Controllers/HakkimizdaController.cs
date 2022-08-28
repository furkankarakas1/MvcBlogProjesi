using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project3.Models;
using project3.Controllers;
using System.IO;

namespace project3.Areas.Admin.Controllers
{
    [Authorize]
    public class HakkimizdaController : Controller
    {
        // GET: Admin/Hakkimizda
        public ActionResult Index()
        {
            using (kahve2020Entities db=new kahve2020Entities()) {

                var model = db.hakkimizda.First();
            return View(model);
            
            }
        }
        public ActionResult HakkimizdaGuncelle()
        {
            using(kahve2020Entities db=new kahve2020Entities())
            {
                var model = db.hakkimizda.First();
                return View(model);
                    

            }
        }
        [HttpPost]
        public ActionResult Kaydet(hakkimizda GelenVeri)
        {
            using (kahve2020Entities db=new kahve2020Entities())
            {
                var GuncellenecekVeri = db.hakkimizda.First();
                if (!ModelState.IsValid)
                {
                    return View("HakkimizdaGuncelle", GelenVeri);
                }
                if (GelenVeri.fotoFile != null)
                {
                    GelenVeri.foto = Seo.DosyaAdiDuzenle(GelenVeri.fotoFile.FileName);
                    GelenVeri.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(GelenVeri.foto)));

                }

                db.Entry(GuncellenecekVeri).CurrentValues.SetValues(GelenVeri);
                db.SaveChanges();
                TempData["hakkimdaGuncelle"] = " ";
                return RedirectToAction("index", "hakkimizda");
            }
        }

    }
}