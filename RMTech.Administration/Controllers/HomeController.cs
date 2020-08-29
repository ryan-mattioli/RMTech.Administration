using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RMTech.Administration.Models;
using RMTech.Administration.Providers;

namespace RMTech.Administration.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (RMTechAdminDb db = new RMTechAdminDb())
            {
                var customers = db.Customers.ToList();
                return View(customers);
            }
              
        }

        [Route("create-invoice")]
        [HttpPost]
        public ActionResult CreateInvoice(Invoice invoice)
        {
            using (RMTechAdminDb db = new RMTechAdminDb()) {
               
                invoice.Total = invoice.InvoiceTotal;
                invoice.InvStatus = "N";
                db.Invoices.Add(invoice);
                db.SaveChanges();
  
            }
            return Json(invoice.Id);
        }

        [Route("get-invoice/{invoiceId}")]
        public ActionResult GetInvoice(int invoiceId)
        {
            using (RMTechAdminDb db = new RMTechAdminDb())
            {
                var invoice = db.Invoices.Include("Lines").Where(x => x.Id == invoiceId).FirstOrDefault();
                return View("Invoice", invoice);
            }
        }
        [Route("save-customer")]
        [HttpPost]
        public ActionResult SaveCustomer(Customer customer)
        {
            using (RMTechAdminDb db = new RMTechAdminDb())
            {
                if (customer.Id == 0)
                {
                    db.Customers.Add(customer);
                }
                else
                {
                    db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                }
               
                db.SaveChanges();
                return Json("OK");
            }
        }

        [Route("edit-invoice/{invoiceId}")]
        public ActionResult EditInvoice(int invoiceId)
        {
            using (RMTechAdminDb db = new RMTechAdminDb())
            {
                var invoice = db.Invoices.Include("Lines").Where(x => x.Id == invoiceId).FirstOrDefault();
                return View("EditInvoice", invoice);
            }
        }

        [Route("update-invoice")]
        [HttpPost]
        public ActionResult UpdateInvoice(Invoice invoice)
        {
            using (RMTechAdminDb db = new RMTechAdminDb())
            {
                
                foreach (InvoiceLine l in invoice.Lines)
                {
                    if (l.Id == 0)
                    {
                  
                        db.InvoiceLines.Add(l);
                        invoice.InvoiceDate = DateTime.Now;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(l).State = System.Data.Entity.EntityState.Modified;
                    }

                }


                db.Entry(invoice).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(invoice));
            }

        }
        [Route("delete-invoice-line")]
        [HttpPost]
        public ActionResult DeleteInvoiceLine(InvoiceLine line)
        {

            using (RMTechAdminDb db = new RMTechAdminDb())
            {
               var dbLine = db.InvoiceLines.Find(line.Id);
                db.InvoiceLines.Remove(dbLine);
                db.SaveChanges();
                return Json("OK");
            }
        }

        [Route("recent-invoices")]
        [HttpGet]
        public ActionResult RecentInvoices()
        {
            using (RMTechAdminDb db = new RMTechAdminDb())
            {
                var invoices = db.Invoices.Include("Lines")
                    .OrderByDescending(x => x.Id).Take(10).ToList();

                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(invoices));
            }
        }
    }

}
