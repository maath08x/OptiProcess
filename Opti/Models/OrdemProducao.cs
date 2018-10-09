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
        public int ordemProducaoID { get; set; }
        
        public int produtoID { get; set; }
        
        public int quantidade { get; set; }
        
        public int maquinarioID { get; set; }
        
        public int pedidoID { get; set; }
        
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
            List<OrdemProducao> lop = Pesquisar(op.ordemProducaoID, 0, 0);
            Produtos produtos = new Produtos();
            List<Produtos> lp = produtos.Pesquisar(lop[0].produtoID, "");

            lp[0].produtoID = lop[0].produtoID;
            lp[0].qntEstoque = (lp[0].qntEstoque + lop[0].quantidade);
            produtos.Alterar(lp[0]);

            OrdemProducaoModel opm = new OrdemProducaoModel();
            return opm.Concluir(op);
        }

        #endregion
    }
}
