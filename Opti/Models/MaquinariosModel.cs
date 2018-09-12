namespace Opti.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class MaquinariosModel : DbContext
    {
        public MaquinariosModel()
            : base("name=MaquinariosModel")
        {
        }

        public virtual DbSet<Maquinarios> Maquinarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Maquinarios>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Maquinarios>()
                .Property(e => e.descricao)
                .IsFixedLength();
        }

        public List<Maquinarios> Pesquisar(int maquinarioID, string nome, int tipoMaquinario)
        {
            MaquinariosModel mm = new MaquinariosModel();

            IEnumerable<Maquinarios> maquinario = null;

            if (maquinarioID != 0 && nome != "" && tipoMaquinario != -1)
            {
                maquinario = from p in mm.Maquinarios where p.nome == nome & p.maquinarioID == maquinarioID select p;
            }
            else if (maquinarioID != 0)
            {
                maquinario = from p in mm.Maquinarios where p.maquinarioID == maquinarioID select p;
            }
            else if (nome != "")
            {
                maquinario = from p in mm.Maquinarios where p.nome.Contains(nome) select p;
            }
            else
            {
                maquinario = from p in mm.Maquinarios select p;
            }
            return maquinario.ToList();
        }

        public string Alterar(Maquinarios m)
        {
            try
            {
                MaquinariosModel mm = new MaquinariosModel();
                Maquinarios maquinario = mm.Maquinarios.Single(c => c.maquinarioID.Equals(m.maquinarioID));

                maquinario.nome = (m.nome == null ? maquinario.nome : m.nome);
                maquinario.descricao = (m.descricao == null ? maquinario.descricao : m.descricao);
                maquinario.dtDesocupacao = (m.dtDesocupacao == null ? maquinario.dtDesocupacao : m.dtDesocupacao);
                maquinario.dtOcupacao = (m.dtOcupacao == null ? maquinario.dtOcupacao : m.dtOcupacao);
                maquinario.statusMaquinario = (m.statusMaquinario == null ? maquinario.statusMaquinario : m.statusMaquinario);
                maquinario.tipoMaquinario = (m.tipoMaquinario == null ? maquinario.tipoMaquinario : m.tipoMaquinario);

                mm.SaveChanges();

                return "Maquinário alterado.";
            }
            catch (Exception e)
            {
                return "Não foi possível alterar o maquinário.";
            }
        }

        public string Adicionar(Maquinarios m)
        {
            try
            {
                MaquinariosModel mm = new MaquinariosModel();
                mm.Maquinarios.Add(m);
                mm.SaveChanges();
                return "Maquinario incluído";
            }
            catch
            {
                return "Não foi possível incluir o maquinário.";
            }
        }

        public string Deletar(int maquinarioID)
        {
            try
            {
                MaquinariosModel mm = new MaquinariosModel();
                Maquinarios maquinario = mm.Maquinarios.Single(m => m.maquinarioID.Equals(maquinarioID));
                mm.Maquinarios.Remove(maquinario);

                mm.SaveChanges();

                return "Maquinário deletado.";
            }
            catch (Exception e)
            {
                return "Não foi possível deletar.";
            }
        }

        #region "Métodos de verificações"
        // Este método retorna a menor data entre os maquinários do mesmo tipo
        public DateTime? MenorData(int produtoID)
        {
            ProdutosModel produtosMaquinarios = new ProdutosModel();
            List<ProdutosMaquinarios> lpm = produtosMaquinarios.PesquisarPM(produtoID);
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
