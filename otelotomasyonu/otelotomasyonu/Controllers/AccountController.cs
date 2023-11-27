using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using otelotomasyonu.Models;

namespace otelotomasyonu.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        otelaspEntities db = new otelaspEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult Login(yetkili y)
        {
            var bilgiler = db.yetkili.FirstOrDefault(x => x.u_ad == y.u_ad && x.u_password == y.u_password);
            
            if (bilgiler!=null)
            {
                
                FormsAuthentication.SetAuthCookie(bilgiler.u_ad, false);
                Session["k_ad"] = bilgiler.u_ad;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.hata = "Kullanici Adı veya şifre hatalı";
            }
            return View();
        }
        [Authorize(Roles = "1")]
        public ActionResult Register()
        {
            return View();
        }
        public class Kullanici_ekle
        {
            public string u_ad { get; set; }
            public string u_password { get; set; }
            public string u_yetki { get; set; }
            
        }
        [HttpPost]
        [Authorize(Roles = "1")]
        public ActionResult Register(Kullanici_ekle data)
        {
            yetkili yenikisi = new yetkili();
            yenikisi.u_ad = data.u_ad;
            yenikisi.u_password = data.u_password;
            if (data.u_yetki == "Ceo") yenikisi.u_yetki = 1;
            else if (data.u_yetki == "Müdür") yenikisi.u_yetki = 2;
            else yenikisi.u_yetki = 3;
            yenikisi.u_gecerli = true;
            db.yetkili.Add(yenikisi);

            db.SaveChanges();
            islemkaydet yeni_islem = new islemkaydet();
            yeni_islem.log_olustur(db, yenikisi.u_ad + " adlı " + yenikisi.u_yetki + " yetkili kisi eklendi.", Session["k_ad"].ToString(), DateTime.Now);
       
            return Redirect("Register");
        }

        [Authorize(Roles = "1")]
        public ActionResult Kullanici_Duzenle()
        {
            var list = db.yetkili.ToList();
            return View(list);
        }
        [HttpGet]
        [Authorize(Roles = "1")]
        public ActionResult Sil(int u_id)
        {

            var rezerve = db.yetkili.Find(u_id);
            rezerve.u_gecerli = false;
            db.SaveChanges();
            islemkaydet yeni_islem = new islemkaydet();
            yeni_islem.log_olustur(db,u_id+" user idli kisi silindi." , Session["k_ad"].ToString(), DateTime.Now);

            return Redirect("Kullanici_Duzenle");
        }
        [HttpGet]
        [Authorize(Roles = "1")]
        public ActionResult Guncelle(int u_id, string u_ad, string u_password, string u_yetki)
        {

            var kisi = db.yetkili.Find(u_id);
            var eski_ad = kisi.u_ad;
            var eski_sifre = kisi.u_password;
            var eski_yetki = kisi.u_yetki;
            kisi.u_ad = u_ad;
            kisi.u_password = u_password;
            if (u_yetki == "Ceo") kisi.u_yetki =1;
            else if (u_yetki == "Müdür") kisi.u_yetki = 1;
            else kisi.u_yetki = 3;
            db.SaveChanges();
            islemkaydet yeni_islem = new islemkaydet();
            yeni_islem.log_olustur(db, " Adı:"+eski_ad + " => " + u_ad +" Sifresi:"+eski_sifre+" =>"+u_password+" Yetkisi:"+eski_yetki+" =>"+kisi.u_yetki+ " olarak degistirildi.", Session["k_ad"].ToString(), DateTime.Now);
            return Redirect("Kullanici_Duzenle");
        }


    }
}