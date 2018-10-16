namespace Opti.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Produtos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produtos()
        {
            ProdutosFilhos = new HashSet<ProdutosFilhos>();
            ProdutosFilhos1 = new HashSet<ProdutosFilhos>();
            ProdutosMaquinarios = new HashSet<ProdutosMaquinarios>();
        }

        [Key]
        public int produtoID { get; set; }

        [Required]
        [StringLength(20)]
        public string nome { get; set; }

        [StringLength(50)]
        public string descricao { get; set; }

        public int qntEstoque { get; set; }

        public int leadTime { get; set; }

        public int unidadeMedida { get; set; }

        public int estoqueSeguranca { get; set; }

        public int politicaLote { get; set; }

        public int unidadePoliticaLote { get; set; }

        public int qntEstoqueReservado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<ProdutosFilhos> ProdutosFilhos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<ProdutosFilhos> ProdutosFilhos1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<ProdutosMaquinarios> ProdutosMaquinarios { get; set; }

        //Este array possui informações sobre os itens do pedido.
        //produtos[index][0] == produtoID;
        //produtos[index][1] == quantidade necessária para compra/produção;
        //produtos[index][2] == ok/produzir/comprar;
        //produtos[index][3] == nivel em que a recursividade está;
        //produtos[index][4] == pai;
        //produtos[index][5] == leadTime;
        //produtos[index][6] == nome;
        public string[][] produtos = new string[0][];
        public string produtosOk;
        public bool pedidoOk = false;
        public string producaoGeral;
        public string compraGeral;


        #region "Métodos CRUD"
        public List<Produtos> Pesquisar(int produtoID, string nome)
        {
            ProdutosModel pm = new ProdutosModel();
            return pm.Pesquisar(produtoID, nome);
        }

        public string Adicionar(Produtos p)
        {
            ProdutosModel pm = new ProdutosModel();
            return pm.Adicionar(p);
        }

        public string Alterar(Produtos p)
        {
            ProdutosModel pm = new ProdutosModel();
            return pm.Alterar(p);
        }

        public string Deletar(int produtoID)
        {
            ProdutosModel pm = new ProdutosModel();
            return pm.Deletar(produtoID);
        }

        #endregion

        #region "MÉTODOS AUXILIARES"
        // Este método encontra o melhor maquinario no momento para determinado produto
        public int maquinarioApropriado(int produtoID)
        {
            List<Maquinarios> lm = new List<Maquinarios>();
            ProdutosModel pm = new ProdutosModel();
            List<ProdutosMaquinarios> lpm = pm.PesquisarPM(produtoID);

            for (int i = 0; i < lpm.Count; i++)
            {
                Maquinarios m = new Maquinarios();
                List<Maquinarios> lmm = m.Pesquisar(0, "", lpm[i].tipoMaquinario);

                for (int j = 0; j < lmm.Count; j++)
                {
                    lm.Add(lmm[j]);
                }
            }

            DateTime? menorData = lm[0].dtDesocupacao;
            int menorMaquinario = lm[0].maquinarioID;
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

        // Este método verifica a quantidade de dias necessários para produção dos itens;
        public string VerificaQuntidade(int produtoID, int qntdPedido)
        {
            string retornoUsuario = "";

            bool temCompra = false;


            string compra = "";
            string producao = "";
            produtos = new string[0][];

            VerificaNecessidade(produtoID, qntdPedido, 1, null);

            // Este 'for' verifica se é necessário comprar ou produzir o atual item do pedido
            for (int j = produtos.Length - 1; j >= 0; j--)
            {
                if (produtos[j][2] == "Ok" && produtos[j][3] == "1")
                {
                    produtosOk += produtos[j][0] + ",";
                }

                if (produtos[j][2] == "Comprar" && produtos[j][3] == "1")
                {
                    retornoUsuario += "Não temos o produto " + produtos[j][6] + ", será necessário realizar sua compra\r\n";
                }

                if (produtos[j][2] == "Comprar" && produtos[j][3] != "1")
                {
                    compra += produtos[j][6] + ",";
                    temCompra = true;
                    compraGeral += compra;
                }

                // Caso seja necessário produzir algum item, verifica a quantidade de dias para sua produção
                if (produtos[j][2] == "Produzir")
                {
                    int segurancaCompra = 0;
                    if (temCompra)
                    {
                        segurancaCompra = 7;
                    }

                    Maquinarios m = new Maquinarios();

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

            // Concatena mensagens para ususário
            if (compra.Length != 0 || producao.Length != 0)
            {
                retornoUsuario += "Para produzir o produto " + produtos[0][6] + " será necessário:";

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
                retornoUsuario += "\r\nO processo de produção poderá demorar " + leadTime + " dias.";

                if (temCompra)
                {
                    retornoUsuario += " Este prazo pode sofrer alterações de acordo com a data de entrega dos produtos pendentes para compra.";
                }
            }

            return retornoUsuario;
        }

        // Este método verifica se o produto está ok ou se é necessário comprar ou produzir e alimenta array com essas informações
        private void VerificaNecessidade(int produtoID, int qntSolicitada, int nivel, string pai)
        {
            Array.Resize(ref produtos, produtos.Length + 1);
            produtos[produtos.Length - 1] = new string[7];

            // Verifica se tem quantidade necessária em estoque
            ProdutosModel p = new ProdutosModel();
            List<Produtos> lp = p.Pesquisar(produtoID, "");
            int qntDisponivel = lp[0].qntEstoque - lp[0].qntEstoqueReservado;

            // Estoque de segurança
            /*
            if (nivel == 1)
            {
                int qntSeguranca = EstoqueSeguro(produtoID);

                if ((qntDisponivel - qntSolicitada) < qntSeguranca)
                    qntSolicitada = (qntSeguranca + qntSolicitada) - qntDisponivel;
            }
            */

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
                List<ProdutosFilhos> lpf = pf.PesquisarFilho(produtoID, 0);
                // Se não tem produtos "filhos" é necessário comprar.
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

        public int EstoqueSeguro(int mediaProduto, int mediaFornecedor)
        {
            int estoqueSeguro;
            PedidosModel pm = new PedidosModel();
            List<PedidosProdutos> lpp = pm.PesquisarPedidosProdutos(0, 0, produtoID);
            Produtos produtos = new Produtos();
            //int diasUteis = 250;
            //TimeSpan dtDiferenca;
            //int dias;

            //Percorre a lista
            //for (int i = 0; i < lpp.Count; i++)
            //{
            //    List<Produtos> lp = produtos.Pesquisar(lpp[i].produtoID, "");
            //    if (lpp[i].Pedidos.tipoPedido == 4 && lpp[i].Pedidos.dtPedido > DateTime.Now.AddYears(-1))
            //    {
            //        //dtDiferenca= (DateTime.Parse(lpp[i].Pedidos.dtPrevisao) - DateTime.Parse(lpp[i].Pedidos.dtPedido));
            //        dtDiferenca = Convert.ToDateTime(lpp[i].Pedidos.dtPrevisao).Subtract(Convert.ToDateTime(lpp[i].Pedidos.dtPedido));
            //        mediaFornecedor = mediaFornecedor + dtDiferenca.Days;
            //    }
            //}
            //for (int i = 0; i < lpp.Count; i++)
            //{
            //    List<Produtos> lp = produtos.Pesquisar(lpp[i].produtoID, "");
            //    if (lpp[i].Pedidos.tipoPedido == 5 && lpp[i].Pedidos.dtPedido > DateTime.Now.AddYears(-1))
            //    {
            //        mediaProduto = (mediaProduto + lpp[i].qntPedido);
            //    }
            //}
                    


            estoqueSeguro = mediaProduto * mediaFornecedor;


            //produtos.estoqueSeguranca = estoqueSeguro;
           // produtos.Alterar(produtos);


            return estoqueSeguro;


        }

        #endregion

        #region Movimentações de Estoque
        public void SubtraiSubItens(OrdemProducao op)
        {
            ProdutosFilhos produtosFilhos = new ProdutosFilhos();
            List<ProdutosFilhos> lp = produtosFilhos.Pesquisar(op.produtoID,0);

            for (int i = 0; i < lp.Count; i++)
            {
                Produtos produtos = new Produtos();
                List<Produtos> listProdutos = produtos.Pesquisar(lp[i].filhoID,"");
                listProdutos[0].produtoID = lp[i].filhoID;
                listProdutos[0].qntEstoque = listProdutos[0].qntEstoque - (Convert.ToInt32(op.quantidade) * Convert.ToInt32(lp[i].quantidade));
                produtos.Alterar(listProdutos[0]);
            }
        }
        #endregion
    }
}
