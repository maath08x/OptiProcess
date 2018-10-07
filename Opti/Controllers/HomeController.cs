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

        private static DashboardModel dashboardModel = new DashboardModel();
        private static Dashboard dashboard = new Dashboard();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /*
        [HttpGet]
        public JsonResult Dashboard()
        {
            //dashboard.pedidoID = Convert.ToInt16(Request.Params["id"]);
            List<Dashboard> lp = dashboard.Pesquisar();

            return Json(lp, JsonRequestBehavior.AllowGet);
        }
        */
    }
}