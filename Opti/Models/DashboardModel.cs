using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;

namespace Opti.Models
{
    public class DashboardModel
    {


        public List<Dashboard> Pesquisar()
        {
            DashboardModel pm = new DashboardModel();
            IEnumerable<Dashboard> dashboard;

            return dashboard.ToList();
        }

    }
}