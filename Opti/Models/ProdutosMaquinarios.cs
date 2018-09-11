namespace Opti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProdutosMaquinarios
    {
        public int produtosMaquinariosID { get; set; }

        public int produtoID { get; set; }

        public int tipoMaquinario { get; set; }

        public virtual Produtos Produtos { get; set; }
    }
}
