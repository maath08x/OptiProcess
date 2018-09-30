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
        private static ProdutosMaquinarios produtosMaquinarios = new ProdutosMaquinarios();
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
            return produtos.Adicionar(produtos);
        }

        [HttpPost]
        public string AdicionarSubProdutos()
        {
            produtosFilhos.produtoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            produtosFilhos.filhoID = Convert.ToInt32((Request.Params["SubProdutoID"] == "" ? "0" : Request.Params["SubProdutoID"]));
            produtosFilhos.quantidade = Convert.ToInt32((Request.Params["Quantidade"] == "" ? "0" : Request.Params["Quantidade"]));
            return produtosFilhos.Adicionar(produtosFilhos);
        }

        [HttpPost]
        public string AdicionarMaqProdutos()
        {
            produtosMaquinarios.tipoMaquinario = Convert.ToInt32((Request.Params["TipoMaqProdutoID"] == "" ? "0" : Request.Params["TipoMaqProdutoID"]));
            produtosMaquinarios.produtoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            return produtosMaquinarios.Adicionar(produtosMaquinarios);
        }

        [HttpGet]
        public string Pesquisar()
        {
            produtos.produtoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            produtos.nome = Request.Params["Nome"];
            List<Produtos> lp = produtos.Pesquisar(produtos.produtoID, produtos.nome);

            string txt = JsonConvert.SerializeObject(lp);

            return txt;
        }

        [HttpGet]
        public string PesquisarProdutosFilhos()
        {
            produtosFilhos.produtosFilhosID = Convert.ToInt32((Request.Params["ProdutoID"] == "" ? "0" : Request.Params["ProdutoID"]));
            produtosFilhos.produtoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            List<ProdutosFilhos> lpf = produtosFilhos.Pesquisar(produtosFilhos.produtoID, produtosFilhos.produtosFilhosID);

            string txt = JsonConvert.SerializeObject(lpf);

            return txt;
        }

        [HttpGet]
        public string PesquisarProdutosMaquinarios()
        {
            produtos.produtoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            List<ProdutosMaquinarios> lpf = produtosMaquinarios.Pesquisar(produtos.produtoID);

            string txt = JsonConvert.SerializeObject(lpf);

            return txt;
        }

        [HttpPost]
        public string Alterar()
        {
            produtos.descricao = Request.Params["Descricao"];
            produtos.leadTime = Convert.ToInt32((Request.Params["LeadTime"] == "" ? "0" : Request.Params["LeadTime"]));
            produtos.nome = Request.Params["Nome"];
            return produtos.Alterar(produtos);
        }

        [HttpPost]
        public string AlterarSubProdutos()
        {
            produtosFilhos.produtoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            produtosFilhos.filhoID = Convert.ToInt32((Request.Params["SubProdutoID"] == "" ? "0" : Request.Params["SubProdutoID"]));
            produtosFilhos.quantidade = Convert.ToInt32((Request.Params["Quantidade"] == "" ? "0" : Request.Params["Quantidade"]));
            return produtosFilhos.Alterar(produtosFilhos);
        }


        [HttpPost]
        public string Deletar()
        {
            produtos.produtoID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            return produtos.Deletar(produtos.produtoID);
        }

        [HttpPost]
        public string DeletarSubProdutos()
        {
            produtosFilhos.produtosFilhosID = Convert.ToInt32((Request.Params["SubProdutoID"] == "" ? "0" : Request.Params["SubProdutoID"]));
            return produtosFilhos.Deletar(produtosFilhos.produtosFilhosID);
        }

        [HttpPost]
        public string DeletarMaqProdutos()
        {
            produtosMaquinarios.produtosMaquinariosID = Convert.ToInt32((Request.Params["MaqProdutoID"] == "" ? "0" : Request.Params["MaqProdutoID"]));
            return produtosMaquinarios.Deletar(produtosMaquinarios.produtosMaquinariosID);
        }
        
    }
}