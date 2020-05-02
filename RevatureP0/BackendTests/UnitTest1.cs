using Microsoft.EntityFrameworkCore;
using P0DatabaseApi;
using StoreBackend_Api;
using System;
using System.Linq;
using Xunit;

namespace BackendTests
{
    public class UnitTest1
    {
        [Fact]
        public void AddsCustomerToDb()
        {
            //Arrange
            var options = BuildInMemoryDb();
            string fName = "Bob", lName = "Dole", pNum = "555-555-5555";
            CustomerInfo customerInfo = null;

            //Act
            using (var context = new P0DbContext(options))
            {
                var backend = new StoreBackend(context);
                customerInfo = backend.AddNewCustomer(fName, lName, pNum);
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var customer = (from c in context.Customers
                               where c.FirstName == fName && c.LastName == lName
                               select c).Take(1).FirstOrDefault();

                Assert.Equal(customer.CustomerId, customerInfo.CustomerId);
                Assert.Equal(fName, customer.FirstName);
                Assert.Equal(lName, customer.LastName);
                Assert.Equal(pNum, customer.PhoneNumber);
            }
        }

        [Fact]
        public void GetsCustomerByName()
        {
            //Arrange
            var options = BuildInMemoryDb();
            string fName = "Bob", lName = "Dole", pNum = "555-555-5555";
            int custId;

            //Act
            using (var context = new P0DbContext(options))
            {
                var customer = new Customer
                {
                    FirstName = fName,
                    LastName = lName,
                    PhoneNumber = pNum
                };
                context.Add(customer);
                context.SaveChanges();

                custId = customer.CustomerId;
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var backend = new StoreBackend(context);
                var customerInfo = backend.GetCustomerInfo(fName, lName);

                Assert.Equal(custId, customerInfo.CustomerId);
                Assert.Equal(fName, customerInfo.FirstName);
                Assert.Equal(lName, customerInfo.LastName);
                Assert.Equal(pNum, customerInfo.PhoneNumber);
            }
        }

        private DbContextOptions<P0DbContext> BuildInMemoryDb()
        {
            return new DbContextOptionsBuilder<P0DbContext>()
                .UseInMemoryDatabase(databaseName: "TestDbOne")
                .Options;
        }
    }
}