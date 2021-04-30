﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;


namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            //var degerler = db.TBLURUNLER.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }

        [HttpGet]
    public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }
            
                                             ).ToList();
            ViewBag.value = degerler;
            return View(); 
        }
    [HttpPost]
    public ActionResult UrunEkle(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL (int id)
        {
            var urn = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
   
        public ActionResult UrunGetir(int id)
        {
            var urn = db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }

                                           ).ToList();
            ViewBag.value = degerler;

            return View("UrunGetir", urn);
        }
        public ActionResult Guncelle (TBLURUNLER p)
        {
            var urn = db.TBLURUNLER.Find(p.URUNID);
            urn.URUNADI = p.URUNADI;
            //urn.URUNKATEGORI = p1.URUNKATEGORI;
        
            urn.STOK = p.STOK;
            urn.MARKA = p.MARKA;
            urn.FIYAT = p.FIYAT;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urn.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}