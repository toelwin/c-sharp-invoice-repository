# Setting

* Existing classes: Invoice, InvoiceItems
* IQuerable<Invoice>


# Specification
* InvoiceRepository to handle

  * GetTotal - retruns total value of an invoice by Id.  return null for invalid id.
  * GetTotalUnpaid - returns total value of all unpaid invoices
  * GetItemReport - return all invoice items with respective total counts for a given period in a dictionary.  date range is nullable.