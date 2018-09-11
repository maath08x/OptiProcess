namespace Opti.Models
{
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
        public virtual ICollection<ProdutosFilhos> ProdutosFilhos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdutosFilhos> ProdutosFilhos1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdutosMaquinarios> ProdutosMaquinarios { get; set; }
    }
}
