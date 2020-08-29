using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RMTech.Administration.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Total { get; set; }
        public virtual ICollection<InvoiceLine> Lines { get; set; }
        [NotMapped]
        public decimal InvoiceTotal  { get
            {
                if (this.Lines == null) return 0.0M;
                return this.Lines.ToList().Sum(x => x.LineTotal);
            } set { }
        }
        public string CustomerAddress1 { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZip { get; set; }
        public string CustomerPhone { get; set; }
        public string InvStatus { get; set; }
        public int CustomerId { get; set; }
    }

    public class InvoiceLine
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public DateTime SvcDate { get; set; }
        public string Descr { get; set; }
        public decimal Hours { get; set; }
        public decimal Rate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal LineTotal { get; set; }
    }
}