namespace Opti.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class TiposGeraisModel : DbContext
    {
        public TiposGeraisModel()
            : base("name=TiposGeraisModel")
        {
        }

        public virtual DbSet<TiposGerais> TiposGerais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TiposGerais>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<TiposGerais>()
                .HasMany(e => e.TiposGerais1)
                .WithOptional(e => e.TiposGerais2)
                .HasForeignKey(e => e.telaID);
        }

        public List<TiposGerais> Pesquisar(int tipoID, string nome)
        {
            TiposGeraisModel tgm = new TiposGeraisModel();
            IEnumerable<TiposGerais> tiposGerais;

            if (tipoID != 0 && nome != "" && nome != null)
            {
                tiposGerais = from p in tgm.TiposGerais where p.nome == nome & p.tipoID == tipoID select p;
            }
            else if (tipoID != 0)
            {
                tiposGerais = from p in tgm.TiposGerais where p.tipoID == tipoID select p;
            }
            else if (nome != "")
            {
                tiposGerais = from p in tgm.TiposGerais where p.nome.Contains(nome) select p;
            }
            else
            {
                tiposGerais = from p in tgm.TiposGerais select p;
            }

            return tiposGerais.ToList();
        }

        public string Adicionar(TiposGerais tiposGerais)
        {
            TiposGeraisModel tgm = new TiposGeraisModel();

            try
            {
                tgm.TiposGerais.Add(tiposGerais);
                tgm.SaveChanges();
                return "Tipo cadastrado";
            }
            catch (Exception)
            {
                return "Não foi possível cadastrar este tipo.";
            }
        }

        public string Alterar(TiposGerais tiposGerais)
        {
            TiposGeraisModel tgm = new TiposGeraisModel();
            TiposGerais tipoGeral = tgm.TiposGerais.Single(c => c.tipoID.Equals(tiposGerais.tipoID));

            tipoGeral.nome = tiposGerais.nome;
            tipoGeral.telaID = (tiposGerais.telaID == null ? tipoGeral.telaID : tiposGerais.telaID);

            try
            {
                tgm.SaveChanges();

                return "Item alterado.";
            }
            catch (Exception)
            {
                return "Não foi possível alterar.";
            }
        }

        public string Deletar(int tipoID)
        {
            TiposGeraisModel tgm = new TiposGeraisModel();
            try
            {
                TiposGerais tipoGeral = tgm.TiposGerais.Single(c => c.tipoID.Equals(tipoID));

                tgm.TiposGerais.Remove(tipoGeral);
                tgm.SaveChanges();

                return "Deletado com sucesso";
            }
            catch (Exception e)
            {
                return "Não foi possivel deletar.";
            }
        }
    }
}
