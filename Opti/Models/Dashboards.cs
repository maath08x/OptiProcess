namespace Opti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dashboards
    {
        [Key]
        public int RollNo { get; set; }

        public int QuartoMes { get; set; }
        public int TerceiroMes { get; set; }
        public int SegundoMes { get; set; }
        public int PrimeiroMes { get; set; }
        public int MesAtual { get; set; }
        public int Primeiro { get; set; }
        public int Segundo { get; set; }
        public int Terceiro { get; set; }
        public int Quarto { get; set; }
        public int Quinto { get; set; }
        public int Sexto { get; set; }
        public int Setimo { get; set; }
        public int Oitavo { get; set; }
        public int Nono { get; set; }
        public int Decimo { get; set; }
        public int DecimoPrimeiro { get; set; }
        public int DecimoSegundo { get; set; }
        public int DecimoTerceiro { get; set; }
        public int DecimoQuarto { get; set; }
        public int DecimoQuinto { get; set; }

        public Dashboards()
        {
            RollNo = 0;
            QuartoMes = 0;
            TerceiroMes = 0;
            SegundoMes = 0;
            PrimeiroMes = 0;
            MesAtual = 0;
            Primeiro = 0;
            Segundo = 0;
            Terceiro = 0;
            Quarto = 0;
            Quinto = 0;
            Sexto  = 0;
            Setimo = 0;
            Oitavo = 0;
            Nono = 0;
            Decimo = 0;
            DecimoPrimeiro = 0;
            DecimoSegundo = 0;
            DecimoTerceiro = 0;
            DecimoQuarto = 0;
            DecimoQuinto = 0;
        }

        public List<Dashboards> PrimeiroGrafico()
        {
            Home dm = new Home();
            return dm.PrimeiroGrafico();
        }

        public List<Dashboards> SegundoGrafico()
        {
            Home dm = new Home();
            return dm.SegundoGrafico();
        }

        public List<Produtos> TerceiroGrafico()
        {
            Home dm = new Home();
            return dm.TerceiroGrafico();
        }
    }
}
