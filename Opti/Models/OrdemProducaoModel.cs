namespace Opti.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class OrdemProducaoModel : DbContext
    {
        public OrdemProducaoModel()
            : base("name=OrdemProducaoModel")
        {
        }

        public virtual DbSet<OrdemProducaoProcessos> OrdemProducaoProcessos { get; set; }
        public virtual DbSet<OrdemProducao> OrdemProducao { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        #region M�todos CRUD
        public List<OrdemProducao> Pesquisar(int id, int produtoID, int pedidoID)
        {
            OrdemProducaoModel opm = new OrdemProducaoModel();
            IEnumerable<OrdemProducao> op;

            if (id != 0)
            {
                op = from o in opm.OrdemProducao where o.ordemProducaoID == id select o;
            }
            else if (produtoID != 0)
            {
                op = from o in opm.OrdemProducao where o.produtoID == produtoID select o;
            }
            else if (pedidoID != 0)
            {
                op = from o in opm.OrdemProducao where o.pedidoID == pedidoID select o;
            }
            else
            {
                op = from o in opm.OrdemProducao select o;
            }

            return op.ToList();
        }

        public List<OrdemProducaoProcessos> Pesquisar(int id)
        {
            OrdemProducaoModel opm = new OrdemProducaoModel();
            IEnumerable<OrdemProducaoProcessos> opp;

            opp = from o in opm.OrdemProducaoProcessos where o.ordemProducaoProcID == id select o;

            return opp.ToList();
        }

        public string Adicionar(OrdemProducao op)
        {
            OrdemProducaoModel opm = new OrdemProducaoModel();
            opm.OrdemProducao.Add(op);
            try
            {
                opm.SaveChanges();
                return "OP emitida";
            }
            catch (Exception e)
            {
                return "Nao foi poss�vel emitir sua OP";
            }
        }

        public string Concluir(OrdemProducao o)
        {
            OrdemProducaoModel opm = new OrdemProducaoModel();
            OrdemProducao op = opm.OrdemProducao.Single(c => c.ordemProducaoID.Equals(o.ordemProducaoID));

            if (op.dtConclusao == null)
                op.dtConclusao = DateTime.Now;
            else
                return "N�o � poss�vel concluir uma OP que j� foi conclu�da.";

            try
            {
                opm.SaveChanges();
                MaquinariosModel m = new MaquinariosModel();
                m.ConcluirOP(op.maquinarioID, op.dtPrevisao);
                return "OP conclu�da.";
            }
            catch (Exception e)
            {
                return "N�o foi poss�vel concluir esta OP.";
            }
        }
        #endregion
    }
}
