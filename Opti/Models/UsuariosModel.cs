

namespace Opti.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public class UsuariosModel : DbContext
    {



        public string Alterar(Usuarios usuarios)
        {
            //UsuarioModel pm = new UsuarioModel();
            //Usuario usuario = pm.Pessoas.Single(c => c.pessoaID.Equals(usuarios.pessoaID));

            //usuario.cidade = usuarios.cidade;
            //usuario.documento = usuarios.documento;
            //usuario.dtCadastro = usuarios.dtCadastro;
            //usuario.email = usuarios.email;
            //usuario.estado = usuarios.estado;
            //usuario.fantasia = usuarios.fantasia;
            //usuario.nascimento = usuarios.nascimento;
            //usuario.nome = usuarios.nome;
            //usuario.numero = usuarios.numero;
            //usuario.rua = usuarios.rua;
            //usuario.telefone = usuarios.telefone;

            try
            {
                //pm.SaveChanges();

                return "Item alterado.";
            }
            catch (Exception)
            {
                return "Não foi possível alterar.";
            }
        }

    }
}