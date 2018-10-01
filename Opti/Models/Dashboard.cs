using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Opti.Models
{


    public class Dashboard
    {
        [Key]
        public int pedidoID { get; set; }


        [StringLength(20)]
        public string nome { get; set; }        
       
        public int leadTime { get; set; }
        
        public int qntEstoque { get; set; }
        
        public int qntEstoqueReservado { get; set; }
               
        public int QntPedidoTotal { get; set; }
                       
        [Column(TypeName = "date")]
        public DateTime? dtPedido { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dtPrevisao { get; set; }


        public List<Dashboard> Pesquisar()
        {
            DashboardModel pm = new DashboardModel();
            return pm.Pesquisar();
        }

    }





}