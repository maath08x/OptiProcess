namespace Opti.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pedidos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedidos()
        {
            PedidosProdutos = new HashSet<PedidosProdutos>();
        }

        [Key]
        public int pedidoID { get; set; }

        public int pessoaID { get; set; }

        public int tipoPedido { get; set; }

        public DateTime dtPedido { get; set; }

        public DateTime? dtPrevisao { get; set; }

        public bool finalizado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<PedidosProdutos> PedidosProdutos { get; set; }



        public int leadTime = 0;

        Produtos p = new Produtos();


        #region "M�todos CRUD"
        public List<Pedidos> Pesquisar(int pedidoID, int pessoaID)
        {
            PedidosModel pm = new PedidosModel();
            return pm.Pesquisar(pedidoID, pessoaID);
        }

        public string Adicionar(Pedidos p, List<PedidosProdutos> lpp)
        {
            PedidosModel pm = new PedidosModel();
            return pm.Adicionar(p, lpp);
        }

        public string Alterar(Pedidos p)
        {
            PedidosModel pm = new PedidosModel();
            return pm.Alterar(p);
        }

        public string Deletar(int pedidoID)
        {
            PedidosModel pm = new PedidosModel();
            return pm.Deletar(pedidoID);
        }

        public List<PedidosProdutos> PesquisaProdutos(int pedidoID)
        {
            PedidosModel pm = new PedidosModel();
            return pm.PesquisarPedidosProdutos(pedidoID,0);
        }
        #endregion

        #region "M�todos para verifica��es"

        public string Verifica(List<PedidosProdutos> lpp)
        {
            string situacaoPedido = "";
            if (lpp[0].produtoID == 0)
            {
                return "Por favor, digite um id v�lido.";
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
            List<PedidosProdutos> lpp = pm.PesquisarPedidosProdutos(pedidoID, 0);

            string retorno = Verifica(lpp);

            if (p.pedidoOk)
            {
                lp[0].dtPrevisao = DateTime.Now;
                DateTime dt = DateTime.Today;
                dt = dt.AddDays(leadTime);
                lp[0].dtPrevisao = dt;
                lp[0].finalizado = true;
                Alterar(lp[0]);
                /*
                OrdemProducao op = new OrdemProducao();
                List<OrdemProducao> lop = op.Pesquisar(0, 0, lp[0].pedidoID);
                if (lop.Count > 0)
                {
                    lop[0].dtConclusao = DateTime.Today;
                    op.Concluir(lop[0]);
                }
                */
                MovimentaEstoque(pedidoID);
                return "Pedido finalizado";
            }
            else
            {
                return retorno;
            }
        }
        #endregion

        #region "Movimenta��o de estoque"
        public void MovimentaEstoque(int pedidoID)
        {
            PedidosModel pm = new PedidosModel();
            OrdemProducao op = new OrdemProducao();
            List<PedidosProdutos> lpp = pm.PesquisarPedidosProdutos(pedidoID, 0);
            List<OrdemProducao> lop = op.Pesquisar(0, 0, pedidoID);

            // Conclui as OP's nao conclu�das
            for (int i = 0; i < lop.Count; i++)
            {
                if (lop[i].dtConclusao == null)
                {
                    lop[i].dtConclusao = DateTime.Today;
                    op.Concluir(lop[i]);
                }
            }

            // Movimenta o estoque dos produtos
            for (int i = 0; i < lpp.Count; i++)
            {
                Produtos produtos = new Produtos();
                List<Produtos> lp = produtos.Pesquisar(lpp[i].produtoID, "");

                lp[i].qntEstoque = (lp[i].qntEstoque - lpp[i].qntPedido);
                produtos.Alterar(lp[i]);

            }
        }
        #endregion

        #region Emitir OP
        public string EmitirOP(int pedidoID)
        {
            OrdemProducao ordemProducao = new OrdemProducao();
            List<OrdemProducao> lop = ordemProducao.Pesquisar(0, 0, pedidoID);

            if (lop.Count > 0)
            {
                return "Este pedido j� possui Ordem de Produ��o, portanto n�o � poss�vel emitir novamente.";
            }
            else
            {
                string produzir = "";
                Produtos p = new Produtos();
                Pedidos pedidos = new Pedidos();
                List<PedidosProdutos> lpp = pedidos.PesquisaProdutos(pedidoID);
                for (int i = 0; i < lpp.Count; i++)
                {
                    produzir += p.VerificaQuntidade(lpp[i].produtoID, lpp[i].qntPedido);
                }

                if (p.compraGeral != null)
                {
                    return "Infelizmente n�o � possivel emitir ordem de produ��o, ainda � necess�rio comprar os seguintes itens: " + p.compraGeral;
                }
                else
                {
                    if (p.producaoGeral != null)
                    {
                        string[] a = p.producaoGeral.Split(',');
                        try
                        {
                            for (int k = 0; k < p.produtos.Length; k++)
                            {
                                if (p.produtos[k][2] == "Produzir")
                                {
                                    Maquinarios m = new Maquinarios();

                                    // Insere OP
                                    OrdemProducao op = new OrdemProducao();
                                    op.produtoID = Convert.ToInt32(p.produtos[k][0]);
                                    op.quantidade = Convert.ToInt32(p.produtos[k][1]);
                                    op.dtOrdemProd = DateTime.Now;
                                    op.dtPrevisao = DateTime.Now.AddDays(Convert.ToInt32(p.produtos[k][5]));
                                    op.pedidoID = lpp[0].pedidoID;
                                    op.maquinarioID = p.maquinarioApropriado(Convert.ToInt32(p.produtos[k][0]));
                                    op.Adicionar(op);

                                    // Altera maquinario
                                    m.dtDesocupacao = op.dtPrevisao;
                                    m.dtOcupacao = DateTime.Today;
                                    m.statusMaquinario = 1;
                                    m.maquinarioID = op.maquinarioID;
                                    m.Alterar(m);
                                }
                            }
                            return "OP emitida";
                        }
                        catch (Exception)
                        {
                            return "N�o foi possivel emitir a OP";
                        }
                    }
                    else
                    {
                        return "Os itens deste pedido n�o precisam de OP!";
                    }
                }
            }
        }
        #endregion
    }
}
