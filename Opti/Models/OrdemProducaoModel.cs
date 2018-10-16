namespace Opti.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity.Core;

    public partial class OrdemProducaoModel : DbContext
    {
        public OrdemProducaoModel()
            : base("name=OrdemProducaoModel")
        {
        }

        public virtual DbSet<OrdemProducao> OrdemProducao { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        #region Métodos CRUD
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

        public string Adicionar(OrdemProducao op)
        {
            OrdemProducaoModel opm = new OrdemProducaoModel();
            opm.OrdemProducao.Add(op);
            try
            {
                opm.SaveChanges();

                Produtos produtos = new Produtos();
                produtos.SubtraiSubItens(op);

                return "OP emitida";
            }
            catch (Exception e)
            {
                return "Nao foi possível emitir sua OP";
            }
        }

        public string Concluir(OrdemProducao o)
        {
            OrdemProducaoModel opm = new OrdemProducaoModel();
            OrdemProducao op = opm.OrdemProducao.Single(c => c.ordemProducaoID.Equals(o.ordemProducaoID));

            if (op.dtConclusao == null)
                op.dtConclusao = DateTime.Now;
            else
                return "Não é possível concluir uma OP que já foi concluída.";

            try
            {
                opm.SaveChanges();
                Maquinarios m = new Maquinarios();
                m.ConcluirOP(op.maquinarioID, op.dtPrevisao);
                return "OP concluída.";
            }
            catch (Exception e)
            {
                return "OP não concluída.";
            }
        }
        #endregion
    }
}
