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
            ViewBag.AtributosAddDiff = new List<string>();



            return View();
        }
    }
}