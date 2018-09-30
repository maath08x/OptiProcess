namespace Opti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PedidosProdutos
    {
        [Key]
        public int pedProdutosID { get; set; }

        public int pedidoID { get; set; }

        public int produtoID { get; set; }

        public int qntPedido { get; set; }

        public virtual Pedidos Pedidos { get; set; }

        #region "Métodos CRUD"
        public List<PedidosProdutos> Pesquisar(int pedidoID, int pedidoProdutoID)
        {
            PedidosModel pm = new PedidosModel();
            return pm.PesquisarPedidosProdutos(pedidoID, pedidoProdutoID);
        }

        public string Deletar(int pedProdutoID)
        {
            PedidosModel pm = new PedidosModel();
            return pm.DeletarProd(pedProdutoID);
        }

        public string Alterar(PedidosProdutos pp)
        {
            PedidosModel pm = new PedidosModel();
            return pm.AlterarProd(pp);
        }
        #endregion
    }
}
