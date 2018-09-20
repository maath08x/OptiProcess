namespace Opti.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class PedidosModel : DbContext
    {
        public PedidosModel()
            : base("name=PedidosModel")
        {
        }

        public virtual DbSet<Pedidos> Pedidos { get; set; }
        public virtual DbSet<PedidosProdutos> PedidosProdutos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedidos>()
                .HasMany(e => e.PedidosProdutos)
                .WithRequired(e => e.Pedidos)
                .WillCascadeOnDelete(false);
        }

        public int leadTime = 0;
        ProdutosModel p = new ProdutosModel();

        public List<Pedidos> Pesquisar(int pedidoID, int pessoaID)
        {
            PedidosModel pm = new PedidosModel();
            IEnumerable<Pedidos> pedido;

            if (pedidoID != 0 && pessoaID != 0)
            {
                pedido = from p in pm.Pedidos where p.pedidoID == pedidoID & p.pessoaID == pessoaID select p;
            }
            else if (pedidoID != 0)
            {
                pedido = from p in pm.Pedidos where p.pedidoID == pedidoID select p;
            }
            else if (pessoaID != 0)
            {
                pedido = from p in pm.Pedidos where p.pessoaID == pessoaID select p;
            }
            else
            {
                pedido = from p in pm.Pedidos select p;
            }

            return pedido.ToList();
        }

        public List<PedidosProdutos> PesquisarPedidosProdutos(int pedidoID, int pedProdutosID)
        {
            PedidosModel pm = new PedidosModel();
            IEnumerable<PedidosProdutos> pp;

            if (pedProdutosID != 0)
                pp = from p in pm.PedidosProdutos where p.pedProdutosID == pedProdutosID select p;
            else
                pp = from p in pm.PedidosProdutos where p.pedidoID == pedidoID select p;

            return pp.ToList();
        }

        public string Adicionar(Pedidos p, List<PedidosProdutos> lpp)
        {
            PedidosModel pm = new PedidosModel();
            try
            {
                p.dtPedido = DateTime.Now;

                if (p.pedidoID == 0)
                {
                    pm.Pedidos.Add(p);
                    pm.SaveChanges();
                }

                for (int i = 0; i < lpp.Count(); i++)
                {
                    lpp[i].pedidoID = p.pedidoID;
                    pm.PedidosProdutos.Add(lpp[i]);
                    pm.SaveChanges();
                }
                return "Pedido incluído";
            }
            catch (Exception e)
            {
                return "Não foi possível inserir seu pedido";
            }
        }

        public string Alterar(Pedidos p)
        {
            PedidosModel pm = new PedidosModel();
            Pedidos pedidos = pm.Pedidos.Single(c => c.pedidoID.Equals(p.pedidoID));

            pedidos.dtPrevisao = (p.dtPrevisao == Convert.ToDateTime("01/01/2001") ? pedidos.dtPrevisao : p.dtPrevisao);
            pedidos.pessoaID = (p.pessoaID == 0 ? pedidos.pessoaID : p.pessoaID);
            pedidos.finalizado = (p.finalizado == null ? pedidos.finalizado : p.finalizado);

            try
            {
                pm.SaveChanges();
                return "Pedido alterado";
            }
            catch (Exception e)
            {
                return "Não foi possível alterar seu pedido.";
            }
        }

        public string AlterarProd(PedidosProdutos pp)
        {
            PedidosModel pm = new PedidosModel();
            try
            {
                PedidosProdutos pedidoProduto = pm.PedidosProdutos.Single(c => c.pedProdutosID.Equals(pp.pedProdutosID));
                pedidoProduto.qntPedido = pp.qntPedido;
                pm.SaveChanges();
                return "Produto alterado";
            }
            catch (Exception e)
            {
                return "Não foi possível alterar seu produto.";
            }
        }

        public string Deletar(int pedidoID)
        {
            PedidosModel pm = new PedidosModel();
            try
            {
                IEnumerable<PedidosProdutos> pedidoProduto;
                pedidoProduto = from p in pm.PedidosProdutos where p.pedidoID == pedidoID select p;
                pm.PedidosProdutos.RemoveRange(pedidoProduto);

                Pedidos pedido = pm.Pedidos.Single(c => c.pedidoID.Equals(pedidoID));
                pm.Pedidos.Remove(pedido);

                pm.SaveChanges();
                return "Deletado com sucesso";
            }
            catch (Exception e)
            {
                return "Não foi possível deletar.";
            }

        }

        public string DeletarProd(int pedProdutoID)
        {
            PedidosModel pm = new PedidosModel();
            PedidosProdutos pp = pm.PedidosProdutos.Single(c => c.pedProdutosID.Equals(pedProdutoID));
            pm.PedidosProdutos.Remove(pp);
            try
            {
                pm.SaveChanges();
                return "Deletado com sucesso";
            }
            catch (Exception e)
            {
                return "Não foi possível deletar.";
            }
        }

        #region "Métodos para verificações"

        public string Verifica(List<PedidosProdutos> lpp)
        {
            string situacaoPedido = "";
            if (lpp[0].produtoID == 0)
            {
                return "Por favor, digite um id válido.";
            }

            for (int i = 0; i < lpp.Count; i++)
            {
                situacaoPedido += p.VerificaQuntidade(lpp[i].produtoID, lpp[i].qntPedido);
            }
            return situacaoPedido;
        }

        public string VerificaFinaliza(int pedidoID)
        {
            PedidosModel pm = new PedidosModel();
            List<Pedidos> lp = pm.Pesquisar(pedidoID, 0);
            List<PedidosProdutos> lpp = pm.PesquisarPedidosProdutos(pedidoID,0);

            string retorno = Verifica(lpp);

            if (p.pedidoOk)
            {
                DateTime dtPrevisao = DateTime.Now;
                dtPrevisao = dtPrevisao.AddDays(leadTime);

                lp[0].dtPrevisao = DateTime.Now;
                lp[0].dtPrevisao = dtPrevisao;
                lp[0].finalizado = true;
                Alterar(lp[0]);

                OrdemProducaoModel op = new OrdemProducaoModel();
                List<OrdemProducao> lop = op.Pesquisar(0, 0, lp[0].pedidoID);
                if (lop.Count > 0)
                {
                    lop[0].dtConclusao = DateTime.Today;
                    op.Concluir(lop[0]);
                }
                return "Pedido finalizado";
            }
            else
            {
                return retorno;
            }
        }
        #endregion
    }
}
