namespace Opti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrdemProducao")]
    public partial class OrdemProducao
    {
        [Key]
        [Column(Order = 0)]
        public int ordemProducaoID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int produtoID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int quantidade { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maquinarioID { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int pedidoID { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime dtOrdemProd { get; set; }

        public DateTime? dtPrevisao { get; set; }

        public DateTime? dtConclusao { get; set; }

        #region "Métodos CRUD"

        public List<OrdemProducao> Pesquisar(int opID, int produtoID, int pedidoID)
        {
            OrdemProducaoModel opm = new OrdemProducaoModel();
            return opm.Pesquisar(opID, produtoID, pedidoID);
        }

        public string Adicionar(OrdemProducao op)
        {
            OrdemProducaoModel opm = new OrdemProducaoModel();
            return opm.Adicionar(op);
        }

        public string Concluir(OrdemProducao op)
        {
            OrdemProducaoModel opm = new OrdemProducaoModel();
            return opm.Concluir(op);
        }

        #endregion
    }
}
