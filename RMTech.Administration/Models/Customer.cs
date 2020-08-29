using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMTech.Administration.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustName { get; set; }
        public string CustAddr1 { get; set; }
        public string CustAddr2 { get; set; }
        public string CustCity { get; set; }
        public string CustState { get; set; }
        public string CustZip { get; set; }
        public string CustPhone { get; set; }
    }
}