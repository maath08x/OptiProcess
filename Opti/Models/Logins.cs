namespace Opti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Logins
    {
        [Key]
        public int loginID { get; set; }

        public int pessoaID { get; set; }

        [Required]
        [StringLength(20)]
        public string login { get; set; }

        [StringLength(20)]
        public string senha { get; set; }
    }
}
