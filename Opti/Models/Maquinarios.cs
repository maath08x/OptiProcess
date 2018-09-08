namespace Opti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Maquinarios
    {
        [Key]
        public int maquinarioID { get; set; }

        public int? tipoMaquinario { get; set; }

        public int? statusMaquinario { get; set; }

        public DateTime? dtOcupacao { get; set; }

        public DateTime? dtDesocupacao { get; set; }

        [Required]
        [StringLength(20)]
        public string nome { get; set; }

        [StringLength(35)]
        public string descricao { get; set; }
    }
}
