﻿using Newtonsoft.Json;
using Opti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Opti.Controllers
{
    public class MaquinariosController : Controller
    {
        private static Maquinarios maquinarios = new Maquinarios();
        private static MaquinariosModel maquinariosModel = new MaquinariosModel();
        // GET: Maquinarios
        public ActionResult Index()
        {
            ViewBag.Title = "Maquinários";
            ViewBag.AtributosEditable = new List<string>();
            ViewBag.AtributosEditableSel = new List<string>();
            ViewBag.AtributosAdd = new List<string>();
            ViewBag.AtributosAddSel = new List<string>();
            ViewBag.AtributosSearch = new List<string>();
            ViewBag.AtributosGrid = new List<string>();
            ViewBag.AtributosConfig = new List<string>();

            ViewBag.AtributosSearch.Add("ID");
            ViewBag.AtributosSearch.Add("Nome");

            ViewBag.AtributosEditable.Add("Nome");
            ViewBag.AtributosEditable.Add("Descrição");
            ViewBag.AtributosEditableSel.Add("SelTipo");

            ViewBag.AtributosAdd.Add("Nome");
            ViewBag.AtributosAdd.Add("Descrição");
            ViewBag.AtributosAddSel.Add("SelTipo");

            ViewBag.AtributosGrid.Add("ID");
            ViewBag.AtributosGrid.Add("Nome");
            ViewBag.AtributosGrid.Add("Tipo");
            ViewBag.AtributosGrid.Add("Descrição");
            ViewBag.AtributosGrid.Add("Status");
            ViewBag.AtributosGrid.Add("Data de Ocupação");
            ViewBag.AtributosGrid.Add("Previsão de Desocupação");



            return View();
        }

        [HttpPost]
        public string Adicionar()
        {
            maquinarios.descricao = Request.Params["Descricao"];
            maquinarios.nome = Request.Params["Nome"];
            maquinarios.tipoMaquinario = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            maquinarios.statusMaquinario = Convert.ToInt32((Request.Params["Status"] == "" ? "0" : Request.Params["Status"]));
            return maquinarios.Adicionar(maquinarios);
        }

        [HttpGet]
        public JsonResult Pesquisar()
        {
            maquinarios.maquinarioID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            maquinarios.nome = (Request.Params["Nome"] == null ? "" : Request.Params["Nome"]);
            maquinarios.tipoMaquinario = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            List<Maquinarios> lm = maquinarios.Pesquisar(maquinarios.maquinarioID, maquinarios.nome, 0);
            
            return Json(lm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string Alterar()
        {
            maquinarios.descricao = Request.Params["Descricao"];
            maquinarios.nome = Request.Params["Nome"];
            maquinarios.tipoMaquinario = Convert.ToInt32((Request.Params["Tipo"] == "" ? "0" : Request.Params["Tipo"]));
            return maquinarios.Alterar(maquinarios);
        }

        [HttpPost]
        public string Deletar()
        {
            maquinarios.maquinarioID = Convert.ToInt32((Request.Params["ID"] == "" ? "0" : Request.Params["ID"]));
            return maquinarios.Deletar(maquinarios.maquinarioID);
        }
    }
}