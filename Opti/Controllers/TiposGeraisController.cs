using Newtonsoft.Json;
using Opti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Opti.Controllers
{
    public class TiposGeraisController : Controller
    {
        private static TiposGerais tiposGerais = new TiposGerais();
        private static TiposGeraisModel tiposGeraisModel = new TiposGeraisModel();
        // GET: TiposGerais
        public ActionResult Index()
        {
            ViewBag.Title = "Tipos Gerais";
            ViewBag.AtributosSearch = new List<string>();
            ViewBag.AtributosGrid = new List<string>();
            ViewBag.AtributosEditable = new List<string>();
            ViewBag.AtributosAdd = new List<string>();

            ViewBag.AtributosSearch.Add("ID");
            ViewBag.AtributosSearch.Add("Nome");

            ViewBag.AtributosGrid.Add("ID");
            ViewBag.AtributosGrid.Add("Nome");

            ViewBag.AtributosEditable.Add("Nome");

            ViewBag.AtributosAdd.Add("Nome");

            return View();
        }

        [HttpPost]
        public string Adicionar()
        {
            tiposGerais.nome = Request.Params["Nome"];
            tiposGerais.telaID = Convert.ToInt32(Request.Params["TelaID"]);
            return tiposGeraisModel.Adicionar(tiposGerais);
        }

        [HttpGet]
        public string Pesquisar()
        {
            tiposGerais.tipoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            tiposGerais.nome = Request.Params["Nome"];
            List<TiposGerais> ltg = tiposGeraisModel.Pesquisar(tiposGerais.tipoID, tiposGerais.nome);

            string txt = JsonConvert.SerializeObject(ltg);

            return txt;
        }

        [HttpPost]
        public string Alterar()
        {
            tiposGerais.tipoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            tiposGerais.nome = Request.Params["Nome"];
            return tiposGeraisModel.Alterar(tiposGerais);
        }

        [HttpPost]
        public string Deletar()
        {
            tiposGerais.tipoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            return tiposGeraisModel.Deletar(tiposGerais.tipoID);
        }
    }
}