namespace Opti.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class Home : DbContext
    {
        public Home()
            : base("name=Home")
        {
        }
        
        public virtual DbSet<Dashboards> Dashboards { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Maquinarios> Maquinarios { get; set; }
        public virtual DbSet<OrdemProducao> OrdemProducao { get; set; }
        public virtual DbSet<Pedidos> Pedidos { get; set; }
        public virtual DbSet<PedidosProdutos> PedidosProdutos { get; set; }
        public virtual DbSet<Pessoas> Pessoas { get; set; }
        public virtual DbSet<Produtos> Produtos { get; set; }
        public virtual DbSet<ProdutosFilhos> ProdutosFilhos { get; set; }
        public virtual DbSet<ProdutosMaquinarios> ProdutosMaquinarios { get; set; }
        public virtual DbSet<TiposGerais> TiposGerais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logins>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<Logins>()
                .Property(e => e.senha)
                .IsUnicode(false);

            modelBuilder.Entity<Maquinarios>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Maquinarios>()
                .Property(e => e.descricao)
                .IsFixedLength();

            modelBuilder.Entity<Pedidos>()
                .HasMany(e => e.PedidosProdutos)
                .WithRequired(e => e.Pedidos)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<Produtos>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Produtos>()
                .Property(e => e.descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Produtos>()
                .HasMany(e => e.ProdutosFilhos)
                .WithRequired(e => e.Produtos)
                .HasForeignKey(e => e.filhoID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Produtos>()
                .HasMany(e => e.ProdutosFilhos1)
                .WithRequired(e => e.Produtos1)
                .HasForeignKey(e => e.produtoID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Produtos>()
                .HasMany(e => e.ProdutosMaquinarios)
                .WithRequired(e => e.Produtos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TiposGerais>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<TiposGerais>()
                .HasMany(e => e.TiposGerais1)
                .WithOptional(e => e.TiposGerais2)
                .HasForeignKey(e => e.telaID);
        }

        public List<Dashboards> PrimeiroGrafico()
        {
            Home dm = new Home();
            List<Dashboards> d = new List<Dashboards>();

            d = dm.Dashboards.SqlQuery(
                "SELECT " +
                    "1 RollNo, " +
                    "6 [Primeiro], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 1, 103)) = (CONVERT(VARCHAR(103), dtPedido - 1, 103))) [Segundo], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 2, 103)) = (CONVERT(VARCHAR(103), dtPedido - 2, 103))) [Terceiro], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 3, 103)) = (CONVERT(VARCHAR(103), dtPedido - 3, 103))) [Quarto], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 4, 103)) = (CONVERT(VARCHAR(103), dtPedido - 4, 103))) [Quinto], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 5, 103)) = (CONVERT(VARCHAR(103), dtPedido - 5, 103))) [Sexto], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 6, 103)) = (CONVERT(VARCHAR(103), dtPedido - 6, 103))) [Setimo], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 7, 103)) = (CONVERT(VARCHAR(103), dtPedido - 7, 103))) [Oitavo], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 8, 103)) = (CONVERT(VARCHAR(103), dtPedido - 8, 103))) [Nono], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 9, 103)) = (CONVERT(VARCHAR(103), dtPedido - 9, 103))) [Decimo], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 10,103)) = (CONVERT(VARCHAR(103), dtPedido - 10,103))) [DecimoPrimeiro], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 11,103)) = (CONVERT(VARCHAR(103), dtPedido - 11,103))) [DecimoSegundo], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 12,103)) = (CONVERT(VARCHAR(103), dtPedido - 12,103))) [DecimoTerceiro], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 13,103)) = (CONVERT(VARCHAR(103), dtPedido - 13,103))) [DecimoQuarto], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(CONVERT(VARCHAR(103), GETDATE() - 14,103)) = (CONVERT(VARCHAR(103), dtPedido - 14,103))) [DecimoQuinto], " +
                    "0 QuartoMes, " + 
                    "0 TerceiroMes, " + 
                    "0 SegundoMes, " + 
                    "0 PrimeiroMes, " +
                    "0 MesAtual"
                ).ToList();

            return d;
        }

        public List<Dashboards> SegundoGrafico()
        {
            Home dm = new Home();
            List<Dashboards> d = new List<Dashboards>();

            d = dm.Dashboards.SqlQuery(
                "SELECT " +
                    "1 RollNo, " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(MONTH(GETDATE()) - 4) = (MONTH(dtPedido))) [QuartoMes], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(MONTH(GETDATE()) - 3) = (MONTH(dtPedido))) [TerceiroMes], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(MONTH(GETDATE()) - 2) = (MONTH(dtPedido))) [SegundoMes], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(MONTH(GETDATE()) - 1) = (MONTH(dtPedido))) [PrimeiroMes], " +
                    "(SELECT COUNT(PedidoID) FROM Pedidos WITH(NOLOCK) WHERE(MONTH(GETDATE())) = (MONTH(dtPedido))) [MesAtual], "+
                    "0 [Primeiro], " +
                    "0 [Segundo], " +
                    "0 [Terceiro], " +
                    "0 [Quarto], " +
                    "0 [Quinto], " +
                    "0 [Sexto], " +
                    "0 [Setimo], " +
                    "0 [Oitavo], " +
                    "0 [Nono], " +
                    "0 [Decimo], " +
                    "0 [DecimoPrimeiro], " +
                    "0 [DecimoSegundo], " +
                    "0 [DecimoTerceiro], " +
                    "0 [DecimoQuarto], " +
                    "0 [DecimoQuinto] "
                ).ToList();

            return d;
        }

        public List<Produtos> TerceiroGrafico()
        {
            Home dm = new Home();
            List<Produtos> d = new List<Produtos>();

            d = dm.Produtos.SqlQuery(
                "SELECT * " +
                    "FROM Produtos WITH(NOLOCK) " +
                    "ORDER BY qntEstoque DESC"
                ).ToList();

            return d;
        }
    }
}
