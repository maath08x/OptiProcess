namespace Opti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrdemProducaoProcessos
    {
        [Key]
        public int ordemProducaoProcID { get; set; }

        public int OrdemProducaoID { get; set; }

        public int produtoID { get; set; }

        public int maquinarioID { get; set; }


    }
}
