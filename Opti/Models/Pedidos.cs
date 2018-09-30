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


        #region "Métodos CRUD"
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

                OrdemProducao op = new OrdemProducao();
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
