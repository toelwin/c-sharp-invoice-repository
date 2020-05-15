using invoice_repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace invoice_repository_test
{
    public class InvoiceRepositoryTest
    {
        private IQueryable<Invoice> _query;
        public InvoiceRepositoryTest()
        {
            var inv1 = new Invoice()
            {
                Id = 1,
                Number = "0001",
                Buyer = "Buyer1",
                Seller = "Seller1",
                Description = "Invoice1",
                CreationDate = DateTime.Parse("01/01/2020")
            };
            inv1.InvoiceItems.Add(new InvoiceItem { Name = "Apple", Quantity = 100U, Price = 0.5m });
            inv1.InvoiceItems.Add(new InvoiceItem { Name = "Orrange", Quantity = 50U, Price = 0.25m });

            var inv2 = new Invoice()
            {
                Id = 2,
                Number = "0002",
                Buyer = "Buyer2",
                Seller = "Seller2",
                Description = "Invoice2",
                CreationDate = DateTime.Parse("02/02/2020"),
                AcceptanceDate = DateTime.Parse("06/03/2020")
            };
            inv2.InvoiceItems.Add(new InvoiceItem { Name = "Banana", Quantity = 100U, Price = 0.5m });
            inv2.InvoiceItems.Add(new InvoiceItem { Name = "Grapes", Quantity = 50U, Price = 1.25m });
            inv2.InvoiceItems.Add(new InvoiceItem { Name = "Strawberries", Quantity = 75U, Price = 2.0m });


            var inv3 = new Invoice()
            {
                Id = 2,
                Number = "0003",
                Buyer = "Buyer3",
                Seller = "Seller1",
                Description = "Invoice3",
                CreationDate = DateTime.Parse("04/02/2020"),
                AcceptanceDate = DateTime.Parse("07/03/2020")
            };
            inv2.InvoiceItems.Add(new InvoiceItem { Name = "Banana", Quantity = 200U, Price = 0.5m });
            _query = (new List<Invoice> { inv1, inv2 }).AsQueryable();
        }


        [Fact]
        public void ShouldRetrunTotalInvoiceValueIfValidInvoiceId()
        {

            IInvoiceRepository repo = new InvoiceRepository(_query);

            var actual = repo.GetTotal(1);

            Assert.Equal(62.5m, actual);
        }

        [Fact]
        public void ShouldRetrunNullTotalInvoiceValueIfInvalidInvoiceId()
        {

            IInvoiceRepository repo = new InvoiceRepository(_query);

            var actual = repo.GetTotal(9);

            Assert.Null(actual);
        }

        [Fact]
        public void SholdReturnTotalValueOfAllUnpaidInvoices()
        {
            IInvoiceRepository repo = new InvoiceRepository(_query);

            var actual = repo.GetTotalUnpaid();

            Assert.Equal(62.5m, actual);

        }

        [Fact]
        public void ShouldReturnInvoiceItemsFromGivenPeriod()
        {
            IInvoiceRepository repo = new InvoiceRepository(_query);

            var actual = repo.GetItemsReport(DateTime.Parse("01/02/2020"), null);

            var expected = new KeyValuePair<string, int>("Banana", 300);

            Assert.Contains(expected, actual);
        }
    }
}
