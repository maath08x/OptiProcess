using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Opti.Controllers
{
    public class UsuarioCadController : Controller
    {
        // GET: UsuarioCad
        public ActionResult Index()
        {
            return View();
        }


        /*[HttpPost]
        public string Adicionar()
        {
            maquinarios.descricao = Request.Params["Descricao"];
            maquinarios.nome = Request.Params["Nome"];
            maquinarios.tipoMaquinario = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            maquinarios.statusMaquinario = Convert.ToInt32((Request.Params["Status"] == "" ? "0" : Request.Params["Status"]));
            return maquinariosModel.Adicionar(maquinarios);
        }

        [HttpGet]
        public JsonResult Pesquisar()
        {
            maquinarios.maquinarioID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            maquinarios.nome = Request.Params["Nome"];
            maquinarios.tipoMaquinario = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            List<Maquinarios> lm = maquinariosModel.Pesquisar(maquinarios.maquinarioID, maquinarios.nome, 0);

            return Json(lm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string Alterar()
        {
            maquinarios.descricao = Request.Params["Descricao"];
            maquinarios.nome = Request.Params["Nome"];
            maquinarios.tipoMaquinario = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            return maquinariosModel.Alterar(maquinarios);
        }

        [HttpPost]
        public string Deletar()
        {
            maquinarios.maquinarioID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            return maquinariosModel.Deletar(maquinarios.maquinarioID);
        }

    */
    }
}