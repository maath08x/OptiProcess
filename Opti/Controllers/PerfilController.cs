using Opti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Opti.Controllers
{
    public class PerfilController : Controller
    {

        private static Usuarios usuarios = new Usuarios();
        private static PessoasModel pessoasModel = new PessoasModel();
        // GET: Perfil
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public string Alterar()
        {
            //usuarios.pessoaID = Convert.ToInt32(Request.Params["ID"]);
            usuarios.nome = Request.Params["Nome"];
            usuarios.email = Request.Params["Email"];
            usuarios.usuario = Request.Params["Usuario"];
            usuarios.senha = Request.Params["Senha"];
            usuarios.rua = Request.Params["Rua"];
            usuarios.numero = Request.Params["Numero"];
            usuarios.cidade = Request.Params["Cidade"];
            usuarios.estado = Request.Params["Estado"]; 
            usuarios.fone = Request.Params["Telefone"];
            usuarios.nascimento = Convert.ToDateTime(Request.Params["Nascimento"]);
            usuarios.dtCadastro = DateTime.Now;


            return "";
            //return UsuariosModel.Alterar(usuarios);
        }

    }
}

