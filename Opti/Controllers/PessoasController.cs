using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Opti.Controllers
{
    public class PessoasController : Controller
    {
        // GET: Pessoas
        public ActionResult Index()
        {
            ViewBag.Title = "Pessoas";
            ViewBag.AtributosEditable = new List<string>();
            ViewBag.AtributosSearch = new List<string>();
            ViewBag.AtributosGrid = new List<string>();

            ViewBag.AtributosSearch.Add("ID");
            ViewBag.AtributosSearch.Add("Nome");
            ViewBag.AtributosSearch.Add("Tipo");

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

            ViewBag.AtributosGrid.Add("ID");
            ViewBag.AtributosGrid.Add("Nome");
            ViewBag.AtributosGrid.Add("Tipo");
            ViewBag.AtributosGrid.Add("Nome");
            ViewBag.AtributosGrid.Add("Fantasia");
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
    }
}