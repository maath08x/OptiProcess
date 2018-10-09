using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;

namespace Opti.Models
{
    public class DashboardModel
    {


        public List<Dashboard> PesquisarMensal()
        {
            DashboardModel pm = new DashboardModel();
            IEnumerable<Dashboard> dashboard;

            return dashboard.ToList();
        }

        public List<Dashboard> PesquisarProduto()
        {
            DashboardModel pm = new DashboardModel();
            IEnumerable<Dashboard> dashboard;

            return dashboard.ToList();
        }

        public List<Dashboard> PesquisarDiario()
        {
            DashboardModel pm = new DashboardModel();
            IEnumerable<Dashboard> dashboard;

            return dashboard.ToList();
        }

    }
}