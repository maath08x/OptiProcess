using Opti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Opti.Controllers
{
    public class PessoasController : Controller
    {
        private static Pessoas pessoas = new Pessoas();
        private static PessoasModel pessoasModel = new PessoasModel();
        // GET: Pessoas
        public ActionResult Index()
        {
            ViewBag.Title = "Pessoas";
            ViewBag.AtributosEditable = new List<string>();
            ViewBag.AtributosAdd = new List<string>();
            ViewBag.AtributosSearch = new List<string>();
            ViewBag.AtributosGrid = new List<string>();

            ViewBag.AtributosSearch.Add("ID");
            ViewBag.AtributosSearch.Add("Nome");

            ViewBag.AtributosEditable.Add("Nome");
            ViewBag.AtributosEditable.Add("Fantasia");
            ViewBag.AtributosEditable.Add("Nascimento");
            ViewBag.AtributosEditable.Add("Documento");
            ViewBag.AtributosEditable.Add("Cidade");
            ViewBag.AtributosEditable.Add("Estado");
            ViewBag.AtributosEditable.Add("Rua");
            ViewBag.AtributosEditable.Add("Numero");
            ViewBag.AtributosEditable.Add("Descrição");
            ViewBag.AtributosEditable.Add("Email");
            ViewBag.AtributosEditable.Add("Telefone");

            ViewBag.AtributosAdd.Add("Nome");
            ViewBag.AtributosAdd.Add("Fantasia");
            ViewBag.AtributosAdd.Add("Nascimento");
            ViewBag.AtributosAdd.Add("Documento");
            ViewBag.AtributosAdd.Add("Cidade");
            ViewBag.AtributosAdd.Add("Estado");
            ViewBag.AtributosAdd.Add("Rua");
            ViewBag.AtributosAdd.Add("Numero");
            ViewBag.AtributosAdd.Add("Descrição");
            ViewBag.AtributosAdd.Add("Email");
            ViewBag.AtributosAdd.Add("Telefone");

            ViewBag.AtributosGrid.Add("ID");
            ViewBag.AtributosGrid.Add("Nome");
            ViewBag.AtributosGrid.Add("Fantasia");
            ViewBag.AtributosGrid.Add("Tipo");
            ViewBag.AtributosGrid.Add("Nascimento");
            ViewBag.AtributosGrid.Add("Documento");
            ViewBag.AtributosGrid.Add("Cidade");
            ViewBag.AtributosGrid.Add("Estado");
            ViewBag.AtributosGrid.Add("Rua");
            ViewBag.AtributosGrid.Add("Numero");
            ViewBag.AtributosGrid.Add("Descrição");
            ViewBag.AtributosGrid.Add("Email");
            ViewBag.AtributosGrid.Add("Telefone");

            return View();
        }

        [HttpGet]
        public JsonResult Pesquisar()
        {
            pessoas.pessoaID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            pessoas.nome = Request.Params["Nome"];
            pessoas.tipoPessoa = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            List<Pessoas> lp = pessoasModel.Pesquisar(pessoas.pessoaID, pessoas.nome, pessoas.tipoPessoa);

            return Json(lp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string Adicionar()
        {
            pessoas.cidade = Request.Params["Cidade"];
            pessoas.documento = Convert.ToInt32(Request.Params["Documento"]);
            pessoas.dtCadastro = DateTime.Now;
            pessoas.email = Request.Params["Email"];
            pessoas.estado = Request.Params["Estado"];
            pessoas.fantasia = Request.Params["Fantasia"];
            pessoas.nascimento = Convert.ToDateTime(Request.Params["Nascimento"]);
            pessoas.nome = Request.Params["Nome"];
            pessoas.numero = Request.Params["Numero"];
            pessoas.rua = Request.Params["Rua"];
            pessoas.telefone = Request.Params["Telefone"];
            pessoas.tipoPessoa = Convert.ToInt32(Request.Params["Tipo"]);
            

            return pessoasModel.Adicionar(pessoas);
        }

        [HttpPost]
        public string Alterar()
        {
            pessoas.pessoaID = Convert.ToInt32(Request.Params["ID"]);
            pessoas.cidade = Request.Params["Cidade"];
            pessoas.documento = Convert.ToInt32(Request.Params["Documento"]);
            pessoas.dtCadastro = DateTime.Now;
            pessoas.email = Request.Params["Email"];
            pessoas.estado = Request.Params["Estado"];
            pessoas.fantasia = Request.Params["Fantasia"];
            pessoas.nascimento = Convert.ToDateTime(Request.Params["Nascimento"]);
            pessoas.nome = Request.Params["Nome"];
            pessoas.numero = Request.Params["Numero"];
            pessoas.rua = Request.Params["Rua"];
            pessoas.telefone = Request.Params["Telefone"];
            pessoas.tipoPessoa = Convert.ToInt32(Request.Params["Tipo"]);


            return pessoasModel.Alterar(pessoas);
        }

        [HttpPost]
        public string Deletar()
        {
            pessoas.pessoaID = Convert.ToInt32(Request.Params["ID"]);
            return pessoasModel.Deletar(pessoas.pessoaID,0);
        }


    }
}