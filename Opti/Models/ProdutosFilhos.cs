namespace Opti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProdutosFilhos
    {
        public int produtosFilhosID { get; set; }

        public int produtoID { get; set; }

        public int filhoID { get; set; }

        public int? quantidade { get; set; }

        public int? unidadeQuantidade { get; set; }

        public virtual Produtos Produtos { get; set; }

        public virtual Produtos Produtos1 { get; set; }
    }
}
