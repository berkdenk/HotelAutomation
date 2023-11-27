using otelotomasyonu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace otelotomasyonu
{
    
    public class islemkaydet
    {
        public void log_olustur(otelaspEntities db,string aciklama,string yapan,DateTime zaman)
        {
            otel_log yeni_log = new otel_log();
            yeni_log.islem_aciklama = aciklama;
            yeni_log.islem_yapan = yapan;
            yeni_log.islem_yapilan_zaman = zaman;
            db.otel_log.Add(yeni_log);
            db.SaveChanges();
        }
        
    }
}