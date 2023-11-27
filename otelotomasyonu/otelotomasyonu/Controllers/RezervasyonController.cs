using otelotomasyonu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace otelotomasyonu.Controllers
{
    public class RezervasyonController : Controller
    {
        // GET: Rezervasyon
        otelaspEntities db = new otelaspEntities();
        [Authorize]
        public ActionResult Index()
        {
            var list=db.odalar_kayit.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult Guncelle(int kayit_id,string giris_tarih,string cikis_tarih,string tc)
        {
            
            var rezerve = db.odalar_kayit.Find(kayit_id);
            var eski_giris = rezerve.kayit_giris_tarih;
            var eski_cikis = rezerve.kayit_cikis_tarih;
            var eski_tc = rezerve.kayit_mus_tc;
            rezerve.kayit_giris_tarih = Convert.ToDateTime(giris_tarih);
            rezerve.kayit_cikis_tarih = Convert.ToDateTime(cikis_tarih);
            rezerve.kayit_mus_tc = tc;

            db.SaveChanges();
            islemkaydet yeni_islem = new islemkaydet();
            yeni_islem.log_olustur(db, kayit_id + " kayit idli rezervasyon degisiklikleri="+"Giris Tarihi:"+eski_giris+" =>"+rezerve.kayit_giris_tarih+" Cikis Tarihi:"+eski_cikis+" =>"+rezerve.kayit_cikis_tarih+" Musteri tc:"+eski_tc+" =>"+rezerve.kayit_mus_tc+" olarak degistirildi.", Session["k_ad"].ToString(), DateTime.Now);
            return Redirect("Index");
        }

        public ActionResult Sil(int kayit_id)
        {

            var rezerve = db.odalar_kayit.Find(kayit_id);
            rezerve.kayit_gecerlilik = false;

            db.SaveChanges();
            islemkaydet yeni_islem = new islemkaydet();
            yeni_islem.log_olustur(db, kayit_id + " kayit idli rezervasyon silindi.", Session["k_ad"].ToString(), DateTime.Now);
            return Redirect("Index");
        }




    }
}