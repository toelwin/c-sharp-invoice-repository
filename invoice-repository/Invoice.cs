using System;
using System.Collections;
using System.Collections.Generic;

namespace invoice_repository
{
    public class Invoice
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public string Number { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Date Invoice was cleared
        /// </summary>
        public DateTime? AcceptanceDate { get; set; }
        public IList<InvoiceItem> InvoiceItems { get; }
        public Invoice()
        {
            InvoiceItems = new List<InvoiceItem>();
        }

    }
}
