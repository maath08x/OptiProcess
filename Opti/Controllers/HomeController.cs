using Newtonsoft.Json;
using Opti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Opti.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult PrimeiroGrafico()
        {
            Dashboards d = new Dashboards();
            return Json(d.PrimeiroGrafico(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SegundoGrafico()
        {
            Dashboards d = new Dashboards();
            return Json(d.SegundoGrafico(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string TerceiroGrafico()
        {
            Dashboards d = new Dashboards();
            string txt = JsonConvert.SerializeObject(d.TerceiroGrafico());

            return txt;
        }
    }
}