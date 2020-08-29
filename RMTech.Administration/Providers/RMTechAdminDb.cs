using RMTech.Administration.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RMTech.Administration.Providers
{
    public class RMTechAdminDb : DbContext 
    { 
      public RMTechAdminDb() : base("RMTechAdmin")
        {
            Database.SetInitializer<RMTechAdminDb>(null);
        }
        public virtual DbSet<Invoice> Invoices {get;set; }
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
    }
}