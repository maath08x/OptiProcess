using Opti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Opti.Controllers
{
    public class OrdemProducaoController : Controller
    {
        private static OrdemProducao ordemProducao = new OrdemProducao();
        private static OrdemProducaoModel ordemProducaoModel = new OrdemProducaoModel();

        // GET: OrdemProducao
        public ActionResult Index()
        {
            ViewBag.Title = "Ordem de Produção";
            ViewBag.AtributosSearch = new List<string>();
            ViewBag.AtributosSearchSel = new List<string>();
            ViewBag.AtributosGrid = new List<string>();

            ViewBag.AtributosSearch.Add("OP ID");
            ViewBag.AtributosSearch.Add("Pedido ID");
            ViewBag.AtributosSearchSel.Add("SelProduto ID");

            ViewBag.AtributosGrid.Add("ID");
            ViewBag.AtributosGrid.Add("Produto");
            ViewBag.AtributosGrid.Add("Quantidade");
            ViewBag.AtributosGrid.Add("Maquinário");
            ViewBag.AtributosGrid.Add("Pedido");
            ViewBag.AtributosGrid.Add("Data da Ordem de Produção");
            ViewBag.AtributosGrid.Add("Data de Previsão");
            ViewBag.AtributosGrid.Add("Data de Conclusão");

            return View();
        }

        [HttpGet]
        public JsonResult Pesquisar()
        {
            int opID = Convert.ToInt32((Request.Params["OPID"] == "" ? "0" : Request.Params["OPID"]));
            int produtoID = Convert.ToInt32((Request.Params["ProdutoID"] == "" ? "0" : Request.Params["ProdutoID"]));
            int pedidoID = Convert.ToInt32((Request.Params["PedidoID"] == "" ? "0" : Request.Params["PedidoID"]));
            List<OrdemProducao> lop = ordemProducao.Pesquisar(opID, produtoID, pedidoID);

            return Json(lop, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string Adicionar()
        {
            ordemProducao.dtOrdemProd = Convert.ToDateTime(Request.Params["dtOrdemProd"]);
            ordemProducao.dtPrevisao = Convert.ToDateTime(Request.Params["dtPrevisao"]);
            ordemProducao.maquinarioID = Convert.ToInt32(Request.Params["maquinarioID"]);
            ordemProducao.pedidoID = Convert.ToInt32(Request.Params["pedidoID"]);
            ordemProducao.produtoID = Convert.ToInt32(Request.Params["produtoID"]);
            ordemProducao.quantidade = Convert.ToInt32(Request.Params["quantidade"]);
            return ordemProducao.Adicionar(ordemProducao);
        }

        [HttpPost]
        public string Concluir()
        {
            ordemProducao.ordemProducaoID = Convert.ToInt32((Request.Params["OPID"] == "" ? "0" : Request.Params["OPID"]));
            return ordemProducao.Concluir(ordemProducao);
        }
    }
}