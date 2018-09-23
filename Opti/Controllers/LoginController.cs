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
    public class LoginController : Controller
    {
        private static Logins logins = new Logins();
        private static LoginsModel loginsModel = new LoginsModel();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public bool Autenticar()
        {
           
           //logins.login = Request.Params["Usuario"];
           //logins.senha = Request.Params["Senha"];

            logins.login =(Request.Params["usuario"] == "" ? "0" : Request.Params["usuario"]);
            logins.senha = (Request.Params["senha"] == "" ? "0" : Request.Params["senha"]);

            //return loginsModel.Autenticar(logins.login ,logins.senha);
            return true;
        }

        [HttpGet]
        public JsonResult Pesquisar()
        {
            logins.login = Request.Params["Usuario"];
            List<Logins> lp = loginsModel.Pesquisar(logins.login,0);

            return Json(lp, JsonRequestBehavior.AllowGet);
        }
    }
}