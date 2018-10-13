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
        private static Produtos produtos = new Produtos();
        private static ProdutosModel produtosModel = new ProdutosModel();
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
            ViewBag.AtributosEstoSeguro = new List<string>();

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
            List<Pedidos> lp = pedidos.Pesquisar(pedidos.pedidoID, pedidos.pessoaID);

            string txt = JsonConvert.SerializeObject(lp);

            return txt;
        }

        [HttpGet]
        public string PesquisarPedidosProdutos()
        {
            int pedidoID = Convert.ToInt32((Request.Params["PedidoID"] == "" ? "0" : Request.Params["PedidoID"]));
            int pedidoProdutoID = Convert.ToInt32((Request.Params["PedidoProdutoID"] == "" ? "0" : Request.Params["PedidoProdutoID"]));
            int produtoID = Convert.ToInt32((Request.Params["ProdutoID"] == "" ? "0" : Request.Params["ProdutoID"]));
            List<PedidosProdutos> lpp = pedidosProdutos.Pesquisar(pedidoID, pedidoProdutoID, produtoID);

            string txt = JsonConvert.SerializeObject(lpp);

            return txt;
        }

        [HttpPost]
        public string Alterar()
        {
            pedidos.pedidoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            pedidos.pessoaID = Convert.ToInt32((Request.Params["PessoaID"] == "" ? "0" : Request.Params["PessoaID"]));

            return pedidos.Alterar(pedidos);
        }

        [HttpPost]
        public string AlterarPedidosProdutos()
        {
            pedidosProdutos.pedProdutosID = Convert.ToInt32((Request.Params["PedidoProdutoID"] == "" ? "0" : Request.Params["PedidoProdutoID"]));
            pedidosProdutos.qntPedido = Convert.ToInt32((Request.Params["QntPedido"] == "" ? "0" : Request.Params["QntPedido"]));

            return pedidosProdutos.Alterar(pedidosProdutos);
        }

        [HttpPost]
        public string AdicionarPedidosProdutos()
        {
            pedidos.pedidoID = Convert.ToInt32((Request.Params["PedidoID"] == "" ? "0" : Request.Params["PedidoID"]));
            pedidosProdutos.produtoID = Convert.ToInt32((Request.Params["ProdutoID"] == "" ? "0" : Request.Params["ProdutoID"]));
            pedidosProdutos.qntPedido = Convert.ToInt32((Request.Params["QntPedido"] == "" ? "0" : Request.Params["QntPedido"]));

            List<PedidosProdutos> lpp = new List<PedidosProdutos>();
            lpp.Add(pedidosProdutos);

            return pedidos.Adicionar(pedidos, lpp);
        }

        [HttpPost]
        public string DeletarPedidosProdutos()
        {
            pedidosProdutos.pedProdutosID = Convert.ToInt32((Request.Params["PedProdutoID"] == "" ? "0" : Request.Params["PedProdutoID"]));

            return pedidosProdutos.Deletar(pedidosProdutos.pedProdutosID);
        }

        [HttpPost]
        public string DeletarPedido()
        {
            int PedidoID = Convert.ToInt32((Request.Params["PedidoID"] == "" ? "0" : Request.Params["PedidoID"]));

            return pedidos.Deletar(PedidoID);
        }

        [HttpPost]
        public string AdicionarPedido()
        {
            pedidos.pessoaID = Convert.ToInt32((Request.Params["PessoaID"] == "" ? "0" : Request.Params["PessoaID"]));
            pedidos.tipoPedido = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            List<PedidosProdutos> lpp = new List<PedidosProdutos>();

            return pedidos.Adicionar(pedidos, lpp);
        }

        [HttpPost]
        public string FinalizarPedido()
        {
            pedidos.pedidoID = Convert.ToInt32((Request.Params["PedidoID"] == "" ? "0" : Request.Params["PedidoID"]));

            return pedidos.VerificaFinaliza(pedidos.pedidoID);
        }

        [HttpPost]
        public string EmitirOP()
        {
            pedidos.pedidoID = Convert.ToInt32((Request.Params["PedidoID"] == "" ? "0" : Request.Params["PedidoID"]));

            return pedidos.EmitirOP(pedidos.pedidoID);
        }

        [HttpPost]
        public int EstoqueSeguro()
        {
            int mediaProduto;
            int mediaFornecedor;

            mediaProduto = Convert.ToInt32((Request.Params["MediaProduto"] == "" ? "0" : Request.Params["mediaProduto"]));
            mediaFornecedor = Convert.ToInt32((Request.Params["MediaFornecedor"] == "" ? "0" : Request.Params["mediaFornecedor"]));

            return produtos.EstoqueSeguro(mediaProduto,mediaFornecedor);
        }

    }
}