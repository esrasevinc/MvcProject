using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProje.Models.Entity;

namespace MvcProje.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TblUrunler.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAdi,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TblUrunler t1)
        {
            var ktg = db.TblKategoriler.Where(m => m.KategoriID == t1.TblKategoriler.KategoriID).FirstOrDefault();
            t1.TblKategoriler = ktg;
            db.TblUrunler.Add(t1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.TblUrunler.Find(id);
            db.TblUrunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urn = db.TblUrunler.Find(id);
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAdi,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            return View("UrunGetir", urn);
        }

        public ActionResult Guncelle(TblUrunler t1)
        {
            var urn = db.TblUrunler.Find(t1.UrunID);
            urn.UrunAdi = t1.UrunAdi;
            urn.Marka = t1.Marka;
            urn.Fiyat = t1.Fiyat;
            urn.Stok = t1.Stok;
           
            urn.UrunKategori = t1.UrunKategori;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}