using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project3.Models;
namespace project3.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hakkimizda()
        {
            using(kahve2020Entities db=new kahve2020Entities())
            {
                var model = db.hakkimizda.Find(1);

                return View(model);
            }
           
        }

        
        public ActionResult Urunler()
        {

            using (kahve2020Entities db = new kahve2020Entities())
            {
                var model = db.urunler.Where(x=>x.aktif==true).OrderBy(x=>x.sira).ToList();


                return View(model);
            }
        }

        [Route ("urun/{id}/{baslik}")]
        public ActionResult UrunDetay(int id)
        {
            using(kahve2020Entities db=new kahve2020Entities())
            {
                var model = db.urunler.Where(x => x.aktif == true && x.id == id).FirstOrDefault();
               
                    if (model == null)
                    {
                        return HttpNotFound();
                    }


                    return View(model);
                
            }
        }


        public ActionResult Magza()
        {
            return View();
        }
    }
}