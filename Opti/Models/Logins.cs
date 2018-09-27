namespace Opti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Logins
    {
        [Key]
        public int loginID { get; set; }

        public int pessoaID { get; set; }

        [Required]
        [StringLength(20)]
        public string login { get; set; }

        [StringLength(20)]
        public string senha { get; set; }

        public virtual Pessoas Pessoas { get; set; }

        #region "Métodos CRUD"
        public void Inserir(Logins login)
        {
            LoginsModel lm = new LoginsModel();
            lm.Inserir(login);
        }

        public void Alterar(Logins logins)
        {
            LoginsModel lm = new LoginsModel();
            lm.Alterar(logins);
        }

        public void Deletar(int pessoaID)
        {
            LoginsModel lm = new LoginsModel();
            lm.Deletar(pessoaID);
        }

        public List<Logins> Pesquisar(string logins, int loginId)
        {
            LoginsModel lm = new LoginsModel();
            return lm.Pesquisar(logins, loginId);
        }
        #endregion

        #region "Método para autenticação"
        public bool Autenticar(string usuario, string senha)
        {
            LoginsModel lm = new LoginsModel();
            return lm.Autenticar(usuario, senha);
        }
        #endregion
    }
}
