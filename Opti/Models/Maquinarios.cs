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

        #region Método CRUD
        public List<Maquinarios> Pesquisar(int maquinarioID, string nome, int tipoMaquinario)
        {
            MaquinariosModel mm = new MaquinariosModel();
            return mm.Pesquisar(maquinarioID, nome, tipoMaquinario);
        }

        public string Alterar(Maquinarios m)
        {
            MaquinariosModel mm = new MaquinariosModel();
            return mm.Alterar(m);
        }

        public string Adicionar(Maquinarios m)
        {
            MaquinariosModel mm = new MaquinariosModel();
            return mm.Adicionar(m);
        }

        public string Deletar(int maquinarioID)
        {
            MaquinariosModel mm = new MaquinariosModel();
            return mm.Deletar(maquinarioID);
        }
        #endregion

        #region "Métodos de verificações"
        // Este método retorna a menor data entre os maquinários do mesmo tipo
        public DateTime? MenorData(int produtoID)
        {
            ProdutosMaquinarios produtosMaquinarios = new ProdutosMaquinarios();
            List<ProdutosMaquinarios> lpm = produtosMaquinarios.Pesquisar(produtoID);
            DateTime? min = null;

            // Verifica nos máquinarios que podem produzir aquele material, qual a menor data possível para produção
            for (int k = 0; k < lpm.Count; k++)
            {
                Maquinarios m = new Maquinarios();
                DateTime? minThis = null;
                List<Maquinarios> lm = Pesquisar(0, "", lpm[0].tipoMaquinario);

                for (int l = 0; l < lm.Count; l++)
                {
                    if (lm[l].statusMaquinario == 0)
                    {
                        continue;
                    }

                    if (minThis == null)
                    {
                        minThis = lm[l].dtDesocupacao;
                    }
                    else if (minThis != null && lm[l].dtDesocupacao != null)
                    {
                        if (DateTime.Compare(Convert.ToDateTime(minThis), Convert.ToDateTime(lm[l].dtDesocupacao)) > 0)
                        {
                            minThis = lm[l].dtDesocupacao;
                        }
                    }
                }
                if (min == null)
                {
                    min = minThis;
                }
                else if (min != null && minThis != null)
                {
                    if (DateTime.Compare(Convert.ToDateTime(min), Convert.ToDateTime(minThis)) > 0)
                    {
                        min = minThis;
                    }
                }
            }
            return min;
        }

        public void ConcluirOP(int maquinarioID, DateTime? dtPrevisao)
        {
            List<Maquinarios> lm = Pesquisar(maquinarioID, "", 0);

            if (lm[0].dtDesocupacao == dtPrevisao)
            {
                lm[0].dtDesocupacao = null;
                lm[0].statusMaquinario = 0;
            }
            else
            {
                int dts = (Convert.ToDateTime(lm[0].dtDesocupacao).Subtract(DateTime.Now)).Days;
                if (dts > 0)
                {
                    lm[0].dtDesocupacao = Convert.ToDateTime(lm[0].dtDesocupacao).AddDays(dts * -1);
                }
            }

            MaquinariosModel mm = new MaquinariosModel();
            mm.Alterar(lm[0]);
        }
        #endregion
    }
}
