using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace invoice_repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private IQueryable<Invoice> _query;
        public InvoiceRepository(IQueryable<Invoice> query)
        {
            _query = query;
        }
        public Dictionary<string, int> GetItemsReport(DateTime? fromDate, DateTime? toDate)
        {
            return (
                from inv in _query
                where (toDate ?? DateTime.MaxValue) >= inv.CreationDate || (fromDate ?? DateTime.MinValue) <= inv.CreationDate
                from item in inv.InvoiceItems
                group item by item.Name into g
                select new { ItemName = g.Key, Quantity = g.Sum(i => (int) i.Quantity) }
                ).ToDictionary(r => r.ItemName, r => r.Quantity, StringComparer.OrdinalIgnoreCase);
        }

        public decimal? GetTotal(int invoiceId)
        {
            var invoice = (from inv in _query
                           where inv.Id == invoiceId
                           select inv).FirstOrDefault();

            return invoice?.InvoiceItems.Sum(item => item.Price * item.Quantity);
        }

        public decimal GetTotalUnpaid()
        {
            return (from inv in _query
                    where inv.AcceptanceDate == null
                    select inv.InvoiceItems.Sum(item => item.Price * item.Quantity)).FirstOrDefault();
        }
    }
}
