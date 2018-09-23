using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Opti.Models
{
    public class Usuarios
    {

       // [Key]
       // public int loginID { get; set; }

        //[Key]
        //public int pessoaID { get; set; }

        [Required]
        [StringLength(50)]
        public string nome { get; set; }

        [Required]
        [StringLength(30)]
        public string email { get; set; }

        [Required]
        [StringLength(20)]
        public string usuario { get; set; }

        [Required]
        [StringLength(14)]
        public string senha { get; set; }

        [StringLength(30)]
        public string rua { get; set; }

        [StringLength(15)]
        public string numero { get; set; }

        [StringLength(30)]
        public string cidade { get; set; }

        [StringLength(30)]
        public string estado { get; set; }

        [StringLength(30)]
        public string fone { get; set; }

        [StringLength(30)]
        public DateTime? nascimento { get; set; }

        public DateTime? dtCadastro { get; set; }
    }
}