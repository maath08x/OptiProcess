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

        /*
        [HttpPost]
        public string Adicionar()
        {
            .descricao = Request.Params["Descricao"];
            maquinarios.nome = Request.Params["Nome"];
            maquinarios.tipoMaquinario = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            maquinarios.statusMaquinario = Convert.ToInt32((Request.Params["Status"] == "" ? "0" : Request.Params["Status"]));
            return maquinariosModel.Adicionar(maquinarios);
        }

        

        [HttpPost]
        public string Alterar()
        {
            maquinarios.descricao = Request.Params["Descricao"];
            maquinarios.nome = Request.Params["Nome"];
            maquinarios.tipoMaquinario = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            return maquinariosModel.Alterar(maquinarios);
        }
        */
      
    }
}