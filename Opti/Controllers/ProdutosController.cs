using Newtonsoft.Json;
using Opti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Opti.Controllers
{
    public class ProdutosController : Controller
    {
        private static Produtos produtos = new Produtos();
        private static ProdutosFilhos produtosFilhos = new ProdutosFilhos();
        private static ProdutosModel produtosModel = new ProdutosModel();
        // GET: Produtos
        public ActionResult Index()
        {
            ViewBag.Title = "Produtos";
            ViewBag.AtributosSearch = new List<string>();
            ViewBag.AtributosGrid = new List<string>();
            ViewBag.AtributosEditable = new List<string>();
            ViewBag.AtributosAdd = new List<string>();

            ViewBag.AtributosSearch.Add("ID");
            ViewBag.AtributosSearch.Add("Nome");

            ViewBag.AtributosGrid.Add("ID");
            ViewBag.AtributosGrid.Add("Nome");
            ViewBag.AtributosGrid.Add("Descrição");
            ViewBag.AtributosGrid.Add("Estoque");
            ViewBag.AtributosGrid.Add("LeadTime");

            ViewBag.AtributosEditable.Add("Nome");
            ViewBag.AtributosEditable.Add("Descrição");
            ViewBag.AtributosEditable.Add("LeadTime");
            
            ViewBag.AtributosAdd.Add("Nome");
            ViewBag.AtributosAdd.Add("Descrição");
            ViewBag.AtributosAdd.Add("LeadTime");

            return View();
        }

        [HttpPost]
        public string Adicionar()
        {
            produtos.descricao = Request.Params["Descricao"];
            produtos.leadTime = Convert.ToInt32((Request.Params["LeadTime"] == "" ? "0" : Request.Params["LeadTime"]));
            produtos.nome = Request.Params["Nome"];
            return produtosModel.Adicionar(produtos);
        }

        [HttpGet]
        public string Pesquisar()
        {
            produtos.produtoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            produtos.nome = Request.Params["Nome"];
            List<Produtos> lp = produtosModel.Pesquisar(produtos.produtoID, produtos.nome);

            string txt = JsonConvert.SerializeObject(lp);

            return txt;
        }

        [HttpGet]
        public string PesquisarProdutosFilhos()
        {
            produtos.produtoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            List<ProdutosFilhos> lpf = produtosModel.PesquisarFilho(produtos.produtoID);

            string txt = JsonConvert.SerializeObject(lpf);

            return txt;
        }

        [HttpPost]
        public string Alterar()
        {
            produtos.descricao = Request.Params["Descricao"];
            produtos.leadTime = Convert.ToInt32((Request.Params["LeadTime"] == "" ? "0" : Request.Params["LeadTime"]));
            produtos.nome = Request.Params["Nome"];
            return produtosModel.Alterar(produtos);
        }

        [HttpPost]
        public string Deletar()
        {
            produtos.produtoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            return produtosModel.Deletar(produtos.produtoID);
        }
    }
}