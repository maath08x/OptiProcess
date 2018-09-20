using Opti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;

namespace Opti.Controllers
{
    public class PedidosController : Controller
    {
        private static Pedidos pedidos = new Pedidos();
        private static PedidosProdutos pedidosProdutos = new PedidosProdutos();
        private static PedidosModel pedidosModel = new PedidosModel();
        // GET: Pedidos
        public ActionResult Index()
        {
            ViewBag.Title = "Pedidos";
            ViewBag.AtributosSearch = new List<string>();
            ViewBag.AtributosGrid = new List<string>();
            ViewBag.AtributosEditable = new List<string>();
            ViewBag.AtributosAddDiff = new List<string>();

            ViewBag.AtributosSearch.Add("ID");
            ViewBag.AtributosSearch.Add("Pessoa ID");
            ViewBag.AtributosSearch.Add("Tipo");

            ViewBag.AtributosGrid.Add("ID");
            ViewBag.AtributosGrid.Add("Pessoa ID");
            ViewBag.AtributosGrid.Add("Tipo");
            ViewBag.AtributosGrid.Add("Emissão do Pedido");
            ViewBag.AtributosGrid.Add("Previsão de Conclusão");
            ViewBag.AtributosGrid.Add("Finalizado");

            ViewBag.AtributosEditable.Add("Pessoa ID");

            ViewBag.AtributosAddDiff.Add("Pessoa ID");
            ViewBag.AtributosAddDiff.Add("Tipo");

            return View();
        }

        [HttpGet]
        public string PesquisarPedidos()
        {
            pedidos.pedidoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            pedidos.pessoaID = Convert.ToInt32((Request.Params["PessoaID"] == "" ? "0" : Request.Params["PessoaID"]));
            pedidos.tipoPedido = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            List<Pedidos> lp = pedidosModel.Pesquisar(pedidos.pedidoID, pedidos.pessoaID);

            string txt = JsonConvert.SerializeObject(lp);

            return txt;
        }

        [HttpGet]
        public string PesquisarPedidosProdutos()
        {
            int pedidoID = Convert.ToInt32((Request.Params["PedidoID"] == "" ? "0" : Request.Params["PedidoID"]));
            int pedidoProdutoID = Convert.ToInt32((Request.Params["PedidoProdutoID"] == "" ? "0" : Request.Params["PedidoProdutoID"]));
            List<PedidosProdutos> lpp = pedidosModel.PesquisarPedidosProdutos(pedidoID, pedidoProdutoID);

            string txt = JsonConvert.SerializeObject(lpp);

            return txt;
        }

        [HttpPost]
        public string Alterar()
        {
            pedidos.pedidoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            pedidos.pessoaID = Convert.ToInt32((Request.Params["PessoaID"] == "" ? "0" : Request.Params["PessoaID"]));

            return pedidosModel.Alterar(pedidos);
        }

        [HttpPost]
        public string AlterarPedidosProdutos()
        {
            pedidosProdutos.pedProdutosID = Convert.ToInt32((Request.Params["PedidoProdutoID"] == "" ? "0" : Request.Params["PedidoProdutoID"]));
            pedidosProdutos.qntPedido = Convert.ToInt32((Request.Params["QntPedido"] == "" ? "0" : Request.Params["QntPedido"]));

            return pedidosModel.AlterarProd(pedidosProdutos);
        }

        [HttpPost]
        public string AdicionarPedidosProdutos()
        {
            pedidos.pedidoID = Convert.ToInt32((Request.Params["PedidoID"] == "" ? "0" : Request.Params["PedidoID"]));
            pedidosProdutos.produtoID = Convert.ToInt32((Request.Params["ProdutoID"] == "" ? "0" : Request.Params["ProdutoID"]));
            pedidosProdutos.qntPedido = Convert.ToInt32((Request.Params["QntPedido"] == "" ? "0" : Request.Params["QntPedido"]));

            List<PedidosProdutos> lpp = new List<PedidosProdutos>();
            lpp.Add(pedidosProdutos);

            return pedidosModel.Adicionar(pedidos, lpp);
        }

        [HttpPost]
        public string DeletarPedidosProdutos()
        {
            pedidosProdutos.pedProdutosID = Convert.ToInt32((Request.Params["PedProdutoID"] == "" ? "0" : Request.Params["PedProdutoID"]));

            return pedidosModel.DeletarProd(pedidosProdutos.pedProdutosID);
        }

        [HttpPost]
        public string DeletarPedido()
        {
            int PedidoID = Convert.ToInt32((Request.Params["PedidoID"] == "" ? "0" : Request.Params["PedidoID"]));

            return pedidosModel.Deletar(PedidoID);
        }

        [HttpPost]
        public string AdicionarPedido()
        {
            pedidos.pessoaID = Convert.ToInt32((Request.Params["PessoaID"] == "" ? "0" : Request.Params["PessoaID"]));
            pedidos.tipoPedido = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            List<PedidosProdutos> lpp = new List<PedidosProdutos>();

            return pedidosModel.Adicionar(pedidos, lpp);
        }

        [HttpPost]
        public string FinalizarPedido()
        {
            pedidos.pedidoID = Convert.ToInt32((Request.Params["PedidoID"] == "" ? "0" : Request.Params["PedidoID"]));

            return pedidosModel.VerificaFinaliza(pedidos.pedidoID);
        }
    }
}