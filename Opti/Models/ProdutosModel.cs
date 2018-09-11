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

        //Este array possui informa��es sobre os itens do pedido.
        //produtos[index][0] == produtoID;
        //produtos[index][1] == quantidade necess�ria para compra/produ��o;
        //produtos[index][2] == ok/produzir/comprar;
        //produtos[index][3] == nivel em que a recursividade est�;
        //produtos[index][4] == pai;
        //produtos[index][5] == leadTime;
        //produtos[index][6] == nome;
        public string[][] produtos = new string[0][];
        public string produtosOk;
        public bool pedidoOk = false;
        public string producaoGeral;
        public string compraGeral;
        public int leadTime;

        public List<Produtos> Pesquisar(int produtoID, string nome)
        {
            ProdutosModel pm = new ProdutosModel();
            IEnumerable<Produtos> produto;

            if (produtoID != 0 && nome != "")
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

        public List<ProdutosFilhos> PesquisarFilho(int produtoID)
        {
            ProdutosModel pm = new ProdutosModel();
            IEnumerable<ProdutosFilhos> produtoFilho;

            if (produtoID != 0)
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

        public List<Produtos> Adicionar(Produtos p)
        {
            ProdutosModel pm = new ProdutosModel();
            pm.Produtos.Add(p);
            pm.SaveChanges();

            return pm.Pesquisar(p.produtoID, "");
        }

        public List<ProdutosFilhos> Adicionar(ProdutosFilhos pf)
        {
            ProdutosModel pm = new ProdutosModel();
            pm.ProdutosFilhos.Add(pf);
            pm.SaveChanges();

            return pm.PesquisarFilho(pf.produtoID);
        }

        public string Alterar(Produtos p)
        {
            try
            {
                ProdutosModel pm = new ProdutosModel();
                Produtos produtos = pm.Produtos.Single(c => c.produtoID.Equals(p.produtoID));
                //--Corrigir alterar

                produtos.descricao = p.descricao;
                produtos.estoqueSeguranca = p.estoqueSeguranca;
                produtos.leadTime = p.leadTime;
                produtos.nome = p.nome;
                produtos.politicaLote = p.politicaLote;
                produtos.unidadeMedida = p.unidadeMedida;

                pm.SaveChanges();

                return "Produto alterado.";
            }
            catch (Exception e)
            {
                return "N�o foi poss�vel alterar";
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
                return "N�o foi poss�vel alterar";
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
                return "N�o foi poss�vel remover";
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
                return "N�o foi poss�vel remover";
            }
        }

        #region "M�TODOS AUXILIARES"
        // Este m�todo encontra o melhor maquinario no momento para determinado produto
        public int maquinarioApropriado(int produtoID)
        {
            List<Maquinarios> lm = new List<Maquinarios>();
            ProdutosModel pm = new ProdutosModel();
            List<ProdutosMaquinarios> lpm = pm.PesquisarPM(produtoID);

            for (int i = 0; i < lpm.Count; i++)
            {
                MaquinariosModel m = new MaquinariosModel();
                List<Maquinarios> lmm = m.Pesquisar(0, "", lpm[i].tipoMaquinario);

                for (int j = 0; j < lmm.Count; j++)
                {
                    lm.Add(lmm[j]);
                }
            }

            DateTime? menorData = lm[0].dtDesocupacao;
            int menorMaquinario = 0;
            for (int i = 0; i < lm.Count; i++)
            {
                if (lm[i].dtDesocupacao == null)
                {
                    return lm[i].maquinarioID;
                }
                else
                {
                    if (DateTime.Compare(Convert.ToDateTime(menorData), Convert.ToDateTime(lm[i].dtDesocupacao)) > 0)
                    {
                        menorData = lm[i].dtDesocupacao;
                        menorMaquinario = lm[i].maquinarioID;
                    }
                }
            }

            return menorMaquinario;
        }
        #endregion

        #region "Algoritmo"

        // Este m�todo verifica a quantidade de dias necess�rios para produ��o dos itens;
        public string VerificaQuntidade(int produtoID, int qntdPedido)
        {
            string retornoUsuario = "";

            bool temCompra = false;


            string compra = "";
            string producao = "";
            produtos = new string[0][];

            VerificaNecessidade(produtoID, qntdPedido, 1, null);

            // Este 'for' verifica se � necess�rio comprar ou produzir o atual item do pedido
            for (int j = produtos.Length - 1; j >= 0; j--)
            {
                if (produtos[j][2] == "Ok" && produtos[j][3] == "1")
                {
                    produtosOk += produtos[j][0] + ",";
                }

                if (produtos[j][2] == "Comprar" && produtos[j][3] == "1")
                {
                    retornoUsuario += "N�o temos o produto " + produtos[j][6] + ", ser� necess�rio realizar sua compra\r\n";
                }

                if (produtos[j][2] == "Comprar" && produtos[j][3] != "1")
                {
                    compra += produtos[j][6] + ",";
                    temCompra = true;
                    compraGeral += compra;
                }

                // Caso seja necess�rio produzir algum item, verifica a quantidade de dias para sua produ��o
                if (produtos[j][2] == "Produzir")
                {
                    int segurancaCompra = 0;
                    if (temCompra)
                    {
                        segurancaCompra = 7;
                    }

                    MaquinariosModel m = new MaquinariosModel();

                    if (produtos[j][3] != "1")
                    {
                        producao += produtos[j][6] + ",";
                    }
                    producaoGeral += producao;

                    DateTime? min = m.MenorData(Convert.ToInt32(produtos[j][0]));

                    int totalDias = 0;
                    if (min != null)
                    {
                        totalDias = (Convert.ToDateTime(min).Subtract(DateTime.Now)).Days;
                    }

                    leadTime = leadTime + Convert.ToInt32(produtos[j][5]) + (totalDias < 0 ? 0 : totalDias) + 1 + segurancaCompra;
                }
            }

            // Concatena mensagens para usus�rio
            if (compra.Length != 0 || producao.Length != 0)
            {
                retornoUsuario += "Para produzir o produto " + produtos[0][6] + " ser� necess�rio:";

                if (compra.Length != 0)
                {
                    retornoUsuario += "\r\nComprar os seguintes materiais: " + compra.Substring(0, compra.Length - 1) + ".";
                }
                if (producao.Length != 0)
                {
                    retornoUsuario += "\r\nProduzir os seguintes materiais: " + producao.Substring(0, producao.Length - 1) + ".";
                }
            }
            else
            {
                pedidoOk = true;
            }



            if (leadTime > 0)
            {
                retornoUsuario += "\r\nO processo de produ��o poder� demorar " + leadTime + " dias.";

                if (temCompra)
                {
                    retornoUsuario += " Este prazo pode sofrer altera��es de acordo com a data de entrega dos produtos pendentes para compra.";
                }
            }

            return retornoUsuario;
        }

        // Este m�todo verifica se o produto est� ok ou se � necess�rio comprar ou produzir e alimenta array com essas informa��es
        private void VerificaNecessidade(int produtoID, int qntSolicitada, int nivel, string pai)
        {
            Array.Resize(ref produtos, produtos.Length + 1);
            produtos[produtos.Length - 1] = new string[7];

            // Verifica se tem quantidade necess�ria em estoque
            ProdutosModel p = new ProdutosModel();
            List<Produtos> lp = p.Pesquisar(produtoID, "");
            int qntDisponivel = lp[0].qntEstoque - lp[0].qntEstoqueReservado;

            if (qntSolicitada <= qntDisponivel)
            {
                produtos[produtos.Length - 1][0] = produtoID.ToString();
                produtos[produtos.Length - 1][1] = "0";
                produtos[produtos.Length - 1][2] = "Ok";
                produtos[produtos.Length - 1][3] = nivel.ToString();
                produtos[produtos.Length - 1][4] = pai;
                produtos[produtos.Length - 1][5] = lp[0].leadTime.ToString();
                produtos[produtos.Length - 1][6] = lp[0].nome;
            }
            else
            {
                ProdutosModel pf = new ProdutosModel();
                List<ProdutosFilhos> lpf = pf.PesquisarFilho(produtoID);
                // Se n�o tem produtos "filhos" � necess�rio comprar.
                if (lpf.Count == 0)
                {
                    produtos[produtos.Length - 1][0] = produtoID.ToString();
                    produtos[produtos.Length - 1][1] = (qntSolicitada - qntDisponivel).ToString();
                    produtos[produtos.Length - 1][2] = "Comprar";
                    produtos[produtos.Length - 1][3] = nivel.ToString();
                    produtos[produtos.Length - 1][4] = pai;
                    produtos[produtos.Length - 1][5] = lp[0].leadTime.ToString();
                    produtos[produtos.Length - 1][6] = lp[0].nome;
                }
                else
                {
                    produtos[produtos.Length - 1][0] = produtoID.ToString();
                    produtos[produtos.Length - 1][1] = (qntSolicitada - qntDisponivel).ToString();
                    produtos[produtos.Length - 1][2] = "Produzir";
                    produtos[produtos.Length - 1][3] = nivel.ToString();
                    produtos[produtos.Length - 1][4] = pai;
                    produtos[produtos.Length - 1][5] = ((qntSolicitada - qntDisponivel) * lp[0].leadTime).ToString();
                    produtos[produtos.Length - 1][6] = lp[0].nome;

                    nivel++;
                    for (int i = 0; i < lpf.Count; i++)
                    {
                        int filhoID = lpf[i].filhoID;
                        int qnt = qntSolicitada - qntDisponivel;
                        VerificaNecessidade(filhoID, qnt, nivel, produtoID.ToString());
                    }
                }
            }
        }

        #endregion
    }
}
