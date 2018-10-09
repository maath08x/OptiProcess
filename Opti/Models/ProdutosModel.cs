namespace Opti.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class ProdutosModel : DbContext
    {
        public ProdutosModel()
            : base("name=ProdutosModel")
        {
        }

        public virtual DbSet<Produtos> Produtos { get; set; }
        public virtual DbSet<ProdutosFilhos> ProdutosFilhos { get; set; }
        public virtual DbSet<ProdutosMaquinarios> ProdutosMaquinarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
        }

        public List<Produtos> Pesquisar(int produtoID, string nome)
        {
            ProdutosModel pm = new ProdutosModel();
            IEnumerable<Produtos> produto;

            if (produtoID != 0 && nome != "" && nome != null)
            {
                produto = from p in pm.Produtos where p.nome == nome & p.produtoID == produtoID select p;
            }
            else if (produtoID != 0)
            {
                produto = from p in pm.Produtos where p.produtoID == produtoID select p;
            }
            else if (nome != "")
            {
                produto = from p in pm.Produtos where p.nome == nome select p;
            }
            else
            {
                produto = from p in pm.Produtos select p;
            }

            return produto.ToList();
        }

        public List<ProdutosFilhos> PesquisarFilho(int produtoID, int produtoFilhoID)
        {
            ProdutosModel pm = new ProdutosModel();
            IEnumerable<ProdutosFilhos> produtoFilho;

            if(produtoFilhoID != 0)
            {
                produtoFilho = from p in pm.ProdutosFilhos where p.produtosFilhosID == produtoFilhoID select p;
            }
            else if (produtoID != 0)
            {
                produtoFilho = from p in pm.ProdutosFilhos where p.produtoID == produtoID select p;
            }
            else
            {
                produtoFilho = from p in pm.ProdutosFilhos select p;
            }

            return produtoFilho.ToList();
        }

        public List<ProdutosMaquinarios> PesquisarPM(int produtoID)
        {
            ProdutosModel pm = new ProdutosModel();
            IEnumerable<ProdutosMaquinarios> produtoMaquinario;

            if (produtoID != 0)
            {
                produtoMaquinario = from p in pm.ProdutosMaquinarios where p.produtoID == produtoID select p;
            }
            else
            {
                produtoMaquinario = from p in pm.ProdutosMaquinarios select p;
            }

            return produtoMaquinario.ToList();
        }

        public string Adicionar(Produtos p)
        {
            ProdutosModel pm = new ProdutosModel();

            try
            {
                pm.Produtos.Add(p);
                pm.SaveChanges();
                return "Produto incluído.";
            }
            catch (Exception e)
            {
                return "Não foi possível inserir este produto.";
            }
        }

        public string Adicionar(ProdutosFilhos pf)
        {
            ProdutosModel pm = new ProdutosModel();
            try
            {
                pm.ProdutosFilhos.Add(pf);
                pm.SaveChanges();
                return "Produto incluído.";
            }
            catch (Exception e)
            {
                return "Não foi possível inserir este produto.";
            }
        }

        public string Adicionar(ProdutosMaquinarios p)
        {
            ProdutosModel pm = new ProdutosModel();

            try
            {
                pm.ProdutosMaquinarios.Add(p);
                pm.SaveChanges();
                return "Produto incluído.";
            }
            catch (Exception e)
            {
                return "Não foi possível inserir este produto.";
            }
        }

        public string Alterar(Produtos p)
        {
            try
            {
                ProdutosModel pm = new ProdutosModel();
                Produtos produtos = pm.Produtos.Single(c => c.produtoID.Equals(p.produtoID));
                //--Corrigir alterar

                produtos.descricao = (p.descricao == null ? produtos.descricao : p.descricao);
                produtos.estoqueSeguranca = (p.estoqueSeguranca == null ? produtos.estoqueSeguranca : p.estoqueSeguranca);
                produtos.qntEstoque = (p.qntEstoque == null ? produtos.qntEstoque : p.qntEstoque);
                produtos.leadTime = (p.leadTime == null ? produtos.leadTime : p.leadTime);
                produtos.nome = (p.nome == null ? produtos.nome : p.nome);
                produtos.politicaLote = (p.politicaLote == null ? produtos.politicaLote : p.politicaLote);
                produtos.unidadeMedida = (p.unidadeMedida == 0 ? produtos.unidadeMedida : p.unidadeMedida);

                pm.SaveChanges();

                return "Produto alterado.";
            }
            catch (Exception e)
            {
                return "Não foi possível alterar";
            }
        }

        public string Alterar(ProdutosFilhos pf)
        {
            try
            {
                ProdutosModel pm = new ProdutosModel();
                ProdutosFilhos produtosFilhos = pm.ProdutosFilhos.Single(c => c.produtosFilhosID.Equals(pf.produtosFilhosID));

                produtosFilhos.quantidade = pf.quantidade;

                pm.SaveChanges();

                return "Produto alterado.";
            }
            catch (Exception e)
            {
                return "Não foi possível alterar";
            }
        }

        public string Deletar(int produtoID)
        {
            try
            {
                ProdutosModel pm = new ProdutosModel();
                Produtos produtos = pm.Produtos.Single(c => c.produtoID.Equals(produtoID));

                pm.Produtos.Remove(produtos);

                pm.SaveChanges();

                return "Produto removido.";
            }
            catch (Exception e)
            {
                return "Não foi possível remover";
            }
        }

        public string DeletarFilho(int produtoFilhoID)
        {
            try
            {
                ProdutosModel pm = new ProdutosModel();
                ProdutosFilhos produtosFilhos = pm.ProdutosFilhos.Single(c => c.produtosFilhosID.Equals(produtoFilhoID));

                pm.ProdutosFilhos.Remove(produtosFilhos);

                pm.SaveChanges();

                return "Produto removido.";
            }
            catch (Exception e)
            {
                return "Não foi possível remover";
            }
        }

        public string DeletarProdutoMaquinario(int produtosMaquinariosID)
        {
            try
            {
                ProdutosModel pm = new ProdutosModel();
                ProdutosMaquinarios produtosMaquinarios = pm.ProdutosMaquinarios.Single(c => c.produtosMaquinariosID.Equals(produtosMaquinariosID));

                pm.ProdutosMaquinarios.Remove(produtosMaquinarios);

                pm.SaveChanges();

                return "Tipo removido.";
            }
            catch (Exception e)
            {
                return "Não foi possível remover";
            }
        }
    }
}
