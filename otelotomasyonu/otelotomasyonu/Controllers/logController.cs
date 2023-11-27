using otelotomasyonu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace otelotomasyonu.Controllers
{
    public class logController : Controller
    {
        // GET: log
        otelaspEntities db = new otelaspEntities();
        [Authorize(Roles = "1,2")]
        public ActionResult Index()
        {
            var list = db.otel_log.ToList();
            return View(list);
        }
    }
}