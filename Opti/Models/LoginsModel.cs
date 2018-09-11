namespace Opti.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class LoginsModel : DbContext
    {
        public LoginsModel()
            : base("name=LoginsModel")
        {
        }

        public virtual DbSet<Logins> Logins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logins>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<Logins>()
                .Property(e => e.senha)
                .IsUnicode(false);
        }

        public void Inserir(Logins login)
        {
            LoginsModel lm = new LoginsModel();
            lm.Logins.Add(login);
            lm.SaveChanges();
        }

        public void Alterar(Logins logins)
        {
            LoginsModel lm = new LoginsModel();
            Logins login = lm.Logins.Single(c => c.pessoaID.Equals(logins.pessoaID));

            login.login = logins.login;

            lm.SaveChanges();
        }

        public void Deletar(int pessoaID)
        {
            LoginsModel lm = new LoginsModel();
            Logins login = lm.Logins.Single(c => c.pessoaID.Equals(pessoaID));
            lm.Logins.Remove(login);
            lm.SaveChanges();
        }

        public bool Autenticar(string usuario, string senha)
        {
            LoginsModel lm = new LoginsModel();
            IEnumerable<Logins> login = from l in lm.Logins where l.login == usuario & l.senha == senha select l;

            if (login.ToList().Count() == 1)
                return true;

            return false;
        }
    }
}
