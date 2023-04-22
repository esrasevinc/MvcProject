using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProje.Models.Entity;

namespace MvcProje.Controllers
{
    public class MusteriController : Controller
    {

        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TblMusteriler select d;
            if(!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MusteriAdi.Contains(p));
            }
            return View(degerler.ToList());
            //var degerler = db.TblMusteriler.ToList();
            //return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TblMusteriler t1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TblMusteriler.Add(t1);
            db.SaveChanges();
            return View();
        }

        public ActionResult Sil(int id)
        {
            var mus = db.TblMusteriler.Find(id);
            db.TblMusteriler.Remove(mus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TblMusteriler.Find(id);
            return View("MusteriGetir", mus);
        }

        public ActionResult Guncelle(TblMusteriler t1)
        {
            var mus = db.TblMusteriler.Find(t1.MusteriID);
            mus.MusteriAdi = t1.MusteriAdi;
            mus.MusteriSoyadi = t1.MusteriSoyadi;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}