using System;
using System.Collections.Generic;
using System.Text;

namespace invoice_repository
{
    public class InvoiceItem
    {
        public string Name { get; set; }
        public uint Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
