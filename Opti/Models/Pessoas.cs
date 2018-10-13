namespace Opti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pessoas
    {
        [Key]
        public int pessoaID { get; set; }

        public int tipoPessoa { get; set; }

        [Required]
        [StringLength(50)]
        public string nome { get; set; }
        
        [StringLength(20)]
        public string fantasia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nascimento { get; set; }

        [StringLength(20)]
        public string telefone { get; set; }

        [StringLength(30)]
        public string email { get; set; }

        [StringLength(30)]
        public string rua { get; set; }

        [StringLength(15)]
        public string numero { get; set; }

        [StringLength(30)]
        public string cidade { get; set; }

        [StringLength(30)]
        public string estado { get; set; }

        public int? documento { get; set; }

        public DateTime? dtCadastro { get; set; }

        #region "Métodos CRUD"
        public List<Pessoas> Pesquisar(int pessoaID, string nome, int tipoPessoa)
        {
            PessoasModel pm = new PessoasModel();
            return pm.Pesquisar(pessoaID, nome, tipoPessoa);
        }

        public List<Pessoas> Pesquisar()
        {
            PessoasModel pm = new PessoasModel();
            return pm.Pesquisar();
        }

        public string Adicionar(Pessoas pessoas, Logins logins)
        {
            LoginsModel lm = new LoginsModel();
            List<Logins> lp = lm.Pesquisar(logins.login, 0);

            if (lp.Count == 0)
            {
                PessoasModel pm = new PessoasModel();
                return pm.Adicionar(pessoas, logins);
            }
            else
            {
                return "Este usuário já existe em nosso sistema.";
            }
        }

        public string Adicionar(Pessoas pessoas)
        {
            PessoasModel pm = new PessoasModel();
            return pm.Adicionar(pessoas);
        }

        public string Alterar(Pessoas pessoas)
        {
            PessoasModel pm = new PessoasModel();
            return pm.Alterar(pessoas);
        }

        public List<Pessoas> Alterar(Pessoas pessoas, Logins logins)
        {
            PessoasModel pm = new PessoasModel();
            return pm.Alterar(pessoas, logins);
        }

        public string Deletar(int pessoaID, int tipoPessoa)
        {
            PessoasModel pm = new PessoasModel();
            return pm.Deletar(pessoaID, tipoPessoa);
        }
        #endregion
    }
}
