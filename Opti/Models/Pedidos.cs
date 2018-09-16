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

        public DateTime dtPrevisao { get; set; }

        public bool finalizado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<PedidosProdutos> PedidosProdutos { get; set; }
    }
}
