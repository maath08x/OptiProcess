namespace Opti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pessoas
    {
        [Key]
        public int pessoaID { get; set; }

        public int tipoPessoa { get; set; }

        [Required]
        [StringLength(50)]
        public string nome { get; set; }

        [Required]
        [StringLength(20)]
        public string fantasia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nascimento { get; set; }

        [StringLength(20)]
        public string telefone { get; set; }

        [StringLength(30)]
        public string email { get; set; }

        [StringLength(30)]
        public string rua { get; set; }

        [StringLength(15)]
        public string numero { get; set; }

        [StringLength(30)]
        public string cidade { get; set; }

        [StringLength(30)]
        public string estado { get; set; }

        public int? documento { get; set; }

        public DateTime? dtCadastro { get; set; }
    }
}
