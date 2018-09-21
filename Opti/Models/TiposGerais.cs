namespace Opti.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TiposGerais
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TiposGerais()
        {
            TiposGerais1 = new HashSet<TiposGerais>();
        }

        [Key]
        public int tipoID { get; set; }

        public int? telaID { get; set; }

        [Required]
        [StringLength(20)]
        public string nome { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<TiposGerais> TiposGerais1 { get; set; }
        [JsonIgnore]
        public virtual TiposGerais TiposGerais2 { get; set; }
    }
}
