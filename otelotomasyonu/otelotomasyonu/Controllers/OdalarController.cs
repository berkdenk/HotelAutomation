using otelotomasyonu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace otelotomasyonu.Controllers
{
    public class OdalarController : Controller
    {
        // GET: Odalar
        otelaspEntities db = new otelaspEntities();
        
        public class kayit_bilgileri
        {
            public string tc1 { get; set; }
            public string tc2 { get; set; }
            public string tc3 { get; set; }
            public string adsoyad1 { get; set; }
            public string adsoyad2 { get; set; }
            public string adsoyad3 { get; set; }
            public int kayit_oda_id { get; set; }
            public DateTime kayit_giris_tarih { get; set; }
            public DateTime kayit_cikis_tarih { get; set; }
            public Boolean kayit_gecerlilik { get; set; }
            public kayit_bilgileri()
            {
                kayit_gecerlilik = true;
                tc2 = "";
                tc3 = "";
                adsoyad2 = "";
                adsoyad3 = "";
            }

        }
        public class Oda_Class
        {
            public List<odalar_kayit> oda_rezervasyon { get; set; }
            public List<odalar> oda_bilgileri { get; set; }
            public Oda_Class()
            {
                this.oda_rezervasyon = new List<odalar_kayit>();
                this.oda_bilgileri = new List<odalar>();
            }
        }

        [Authorize]
        public ActionResult Index()
        {
            Oda_Class model = new Oda_Class();
            model.oda_bilgileri= db.odalar.ToList();
            model.oda_rezervasyon = db.odalar_kayit.ToList();
            var list = db.odalar.ToList();
            odalar gecici;
            for (int i = 0; i < model.oda_bilgileri.Count-1; i++)
            {
                for (int x = i; x < model.oda_bilgileri.Count; x++)
                {
                    if (model.oda_bilgileri[i].oda_id > model.oda_bilgileri[x].oda_id)
                    {
                        gecici = model.oda_bilgileri[x];
                        model.oda_bilgileri[x] = model.oda_bilgileri[i];
                        model.oda_bilgileri[i] = gecici;
                    }

                }
            }
            
            return View(model);
        }


        [Authorize]
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(kayit_bilgileri Data)
        {
           
            odalar_kayit bilgi = new odalar_kayit();
            musteriler yenimus = new musteriler();
            if (Data.tc3 != "") 
            {
                yenimus.kaldigi_gtarih = Data.kayit_giris_tarih;
                yenimus.kaldigi_ctarih = Data.kayit_cikis_tarih;
                yenimus.mus_ad = Data.adsoyad3;
                yenimus.mus_tc = Data.tc3;
                yenimus.kaldigi_oda = Data.kayit_oda_id;
                db.musteriler.Add(yenimus);
                db.SaveChanges();
            }
            if(Data.tc2!="")
            {
                yenimus.kaldigi_gtarih = Data.kayit_giris_tarih;
                yenimus.kaldigi_ctarih = Data.kayit_cikis_tarih;
                yenimus.mus_ad = Data.adsoyad2;
                yenimus.mus_tc = Data.tc2;
                yenimus.kaldigi_oda = Data.kayit_oda_id;
                db.musteriler.Add(yenimus);
                db.SaveChanges();
            }
            if (Data.tc1!="")
            {
                yenimus.kaldigi_gtarih = Data.kayit_giris_tarih;
                yenimus.kaldigi_ctarih = Data.kayit_cikis_tarih;
                yenimus.mus_ad = Data.adsoyad1;
                yenimus.mus_tc = Data.tc1;
                yenimus.kaldigi_oda = Data.kayit_oda_id;
                db.musteriler.Add(yenimus);
                db.SaveChanges();
            }
            bilgi.kayit_oda_id = Data.kayit_oda_id;
            bilgi.kayit_mus_tc = Data.tc1;
            bilgi.kayit_giris_tarih = Data.kayit_giris_tarih;
            bilgi.kayit_cikis_tarih = Data.kayit_cikis_tarih;
            bilgi.kayit_gecerlilik = Data.kayit_gecerlilik;
            


            db.odalar_kayit.Add(bilgi);
            db.SaveChanges();
            islemkaydet yeni_islem = new islemkaydet();
            yeni_islem.log_olustur(db,Data.kayit_oda_id+" nolu oda:"+Data.tc1+" tcli kisiye baslangic tarihi:"+Data.kayit_giris_tarih+" cikis tarihi:"+Data.kayit_cikis_tarih+" olmak üzere verilmistir." , Session["k_ad"].ToString(), DateTime.Now);
            return View();
        }



        
    }
}