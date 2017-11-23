using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API.AplicativoFIAP
{
    public class FiapDbContext : DbContext
    {
        public FiapDbContext() : base("name=FiapConnection") { }

        public System.Data.Entity.DbSet<API.AplicativoFIAP.Models.Professor> Professors { get; set; }
    }
}