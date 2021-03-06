namespace Opti.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class PessoasModel : DbContext
    {
        public PessoasModel()
            : base("name=PessoasModel")
        {
        }

        public virtual DbSet<Pessoas> Pessoas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoas>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Pessoas>()
                .Property(e => e.fantasia)
                .IsUnicode(false);

            modelBuilder.Entity<Pessoas>()
                .Property(e => e.telefone)
                .IsUnicode(false);

            modelBuilder.Entity<Pessoas>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Pessoas>()
                .Property(e => e.rua)
                .IsUnicode(false);

            modelBuilder.Entity<Pessoas>()
                .Property(e => e.numero)
                .IsUnicode(false);

            modelBuilder.Entity<Pessoas>()
                .Property(e => e.cidade)
                .IsUnicode(false);

            modelBuilder.Entity<Pessoas>()
                .Property(e => e.estado)
                .IsUnicode(false);
        }

        public List<Pessoas> Pesquisar(int pessoaId, string nome, int tipoPessoa)
        {
            PessoasModel pm = new PessoasModel();
            IEnumerable<Pessoas> pessoa;

            if (pessoaId != 0 && nome != "" && nome != null)
            {
                pessoa = from p in pm.Pessoas where p.nome == nome & p.pessoaID == pessoaId select p;
            }
            else if (pessoaId != 0)
            {
                pessoa = from p in pm.Pessoas where p.pessoaID == pessoaId select p;
            }
            else if (nome != "")
            {
                pessoa = from p in pm.Pessoas where p.nome.Contains(nome) select p;
            }
            else
            {
                pessoa = from p in pm.Pessoas select p;
            }

            return pessoa.ToList();
        }

        public List<Pessoas> Pesquisar()
        {
            PessoasModel pm = new PessoasModel();
            IEnumerable<Pessoas> pessoa;

            pessoa = from p in pm.Pessoas select p;

            return pessoa.ToList();
        }

        public string Adicionar(Pessoas pessoa)
        {
            PessoasModel pm = new PessoasModel();

            try
            {
                pm.Pessoas.Add(pessoa);
                pm.SaveChanges();
                return "Pessoa cadastrada";
            }
            catch (Exception)
            {
                return "N�o foi poss�vel cadastrar esta pessoa.";
            }
        }

        public string Adicionar(Pessoas pessoa, Logins login)
        {
            
            try
            {
                PessoasModel pm = new PessoasModel();
                pm.Pessoas.Add(pessoa);
                pm.SaveChanges();

                login.pessoaID = pessoa.pessoaID;
                LoginsModel lm = new LoginsModel();
                lm.Inserir(login);
                return "Pessoa cadastrada";
            }
            catch (Exception e)
            {
                return "N�o foi poss�vel cadastrar esta pessoa.";
            }

          
        }

        public string Alterar(Pessoas pessoas)
        {
            PessoasModel pm = new PessoasModel();
            Pessoas pessoa = pm.Pessoas.Single(c => c.pessoaID.Equals(pessoas.pessoaID));

            pessoa.cidade = pessoas.cidade;
            pessoa.documento = pessoas.documento;
            pessoa.dtCadastro = pessoas.dtCadastro;
            pessoa.email = pessoas.email;
            pessoa.estado = pessoas.estado;
            pessoa.fantasia = pessoas.fantasia;
            pessoa.nascimento = pessoas.nascimento;
            pessoa.nome = pessoas.nome;
            pessoa.numero = pessoas.numero;
            pessoa.rua = pessoas.rua;
            pessoa.telefone = pessoas.telefone;

            try
            {
                pm.SaveChanges();

                return "Item alterado.";
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public List<Pessoas> Alterar(Pessoas pessoas, Logins logins)
        {
            PessoasModel pm = new PessoasModel();
            Pessoas pessoa = pm.Pessoas.Single(c => c.pessoaID.Equals(pessoas.pessoaID));

            pessoa.cidade = pessoas.cidade;
            pessoa.documento = pessoas.documento;
            pessoa.dtCadastro = pessoas.dtCadastro;
            pessoa.email = pessoas.email;
            pessoa.estado = pessoas.estado;
            pessoa.fantasia = pessoas.fantasia;
            pessoa.nascimento = pessoas.nascimento;
            pessoa.nome = pessoas.nome;
            pessoa.numero = pessoas.numero;
            pessoa.rua = pessoas.rua;
            pessoa.telefone = pessoas.telefone;

            pm.SaveChanges();

            Logins login = new Logins();
            LoginsModel loginModel = new LoginsModel();
            login.login = logins.login;
            loginModel.Alterar(logins);

            return pm.Pessoas.Where(e => e.pessoaID == pessoas.pessoaID).ToList();
        }

        public string Deletar(int pessoaID, int tipoPessoa)
        {
            PessoasModel pm = new PessoasModel();
            try
            {
                Pessoas pessoa = pm.Pessoas.Single(c => c.pessoaID.Equals(pessoaID));

                if (tipoPessoa == 8)
                {
                    LoginsModel login = new LoginsModel();
                    login.Deletar(pessoaID);
                }

                pm.Pessoas.Remove(pessoa);
                pm.SaveChanges();

                return "Deletado com sucesso";
            }
            catch (Exception e)
            {
                return "N�o foi possivel deletar.";
            }
        }
    }
}
