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


        public List<Logins> Pesquisar(string logins, int loginId)
        {
            LoginsModel pm = new LoginsModel();
            IEnumerable<Logins> login;

            if (loginId != 0 && logins != null)
            {
                login = from p in pm.Logins where p.loginID == loginId select p;
            }
            else if (loginId != 0)
            {
                login = from p in pm.Logins where p.loginID == loginId select p;
            }
            else if (logins != "")
            {
                login = from p in pm.Logins where p.login.Contains(logins) select p;
            }
            else
            {
                login = from p in pm.Logins select p;
            }

            return login.ToList();
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
            Logins login = lm.Logins.Single(c => c.loginID.Equals(logins.loginID));

            login.login = logins.login;

            lm.SaveChanges();
        }

        public void Deletar(int loginID)
        {
            LoginsModel lm = new LoginsModel();
            Logins login = lm.Logins.Single(c => c.loginID.Equals(loginID));
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
