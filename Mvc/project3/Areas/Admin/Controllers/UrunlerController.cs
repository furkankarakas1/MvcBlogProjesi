using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project3.Controllers;
using project3.Models;
namespace project3.Areas.Admin.Controllers
{
    [Authorize]
    public class UrunlerController : Controller
    {
        // GET: Admin/Urunler
        public ActionResult Index()
        {
            using(kahve2020Entities db=new kahve2020Entities())
            {
                var model = db.urunler.ToList();
                return View(model);

            }
            
        }

        public ActionResult Yeni()
        {
            var model = new urunler();
            return View("UrunForm", model);
        
        }

        public ActionResult Guncelle(int id)
        {
            using (kahve2020Entities db = new kahve2020Entities())
            {
                var model = db.urunler.Find(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("UrunForm", model);

            }
        }

        public ActionResult Kaydet(urunler gelenUrun)
        {
            if (!ModelState.IsValid)
            {
                return View("UrunForm", gelenUrun);
            }
            using (kahve2020Entities db = new kahve2020Entities())
            {

                if (gelenUrun.id == 0) // yeni ürün kayıt
                {
                    if (gelenUrun.fotoFile == null)
                    {
                        ViewBag.HataFoto = "Lütfen Resim yükle";
                        return View("UrunForm", gelenUrun);
                    }

                    string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                    gelenUrun.foto = fotoAdi;
                    db.urunler.Add(gelenUrun);
                    gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    TempData["urun"] = "Ürün eklendi";
                }
                else // güncelle
                {
                    var GuncellenecekVeri = db.urunler.Find(gelenUrun.id);
                    if(gelenUrun.fotoFile != null)
                    {
                        string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                        gelenUrun.foto = fotoAdi;
                        gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    }
                    db.Entry(GuncellenecekVeri).CurrentValues.SetValues(gelenUrun);
                    TempData["urun"] = "ürün güncellendi";
             
                }
                db.SaveChanges();
                return RedirectToAction("index");

            }
            
        }

        public ActionResult Sil(int id)
        {
            using (kahve2020Entities db = new kahve2020Entities())
            {
                var silinecekUrun = db.urunler.Find(id);
                if (silinecekUrun == null)
                {
                    return HttpNotFound();
                }

                db.urunler.Remove(silinecekUrun);
                db.SaveChanges();
                TempData["urun"] = "ürün silindi";

                return RedirectToAction("index");

            }
        }
    }

}