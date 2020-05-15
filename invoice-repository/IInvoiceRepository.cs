using System;
using System.Collections.Generic;
using System.Text;

namespace invoice_repository
{
    public interface IInvoiceRepository
    {
        public decimal? GetTotal(int invoiceId);
        public decimal GetTotalUnpaid();
        public Dictionary<string, int> GetItemsReport(DateTime? fromDate, DateTime? toDate);
    }
}
