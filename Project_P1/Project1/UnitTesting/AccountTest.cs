using System;
using Xunit;
using BusinessLayer;
using ModelsLibrary;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting 
{
    
    public class AccountTest
   {
        //private readonly DbContextOptions options = new DbContextOptions<P1DatabaseContext>().UseInMemoryDatabase(databaseName: "TestaDb").Options;
        public P1DatabaseContext context = new P1DatabaseContext();
        public AccountLogic accountLogic = new AccountLogic();
        [Fact]
        public void Task_CheckLogin()
        {
            //Arrange
            var c = new Customer()
            {
                CustomerId = 1,
                FirstName = "blank",
                LastName = "blank",
                Username = "blank",
                UserPassord = "blank",
            };
            var c2 = new Customer();
            var expectedValue = false;

            //Act
            var result = accountLogic.CheckLogin(c);

            //Assert
            Xunit.Assert.Equal(expectedValue, result);
            Xunit.Assert.False(accountLogic.CheckLogin(c2));
        }
        //[Fact]
        //public void Task_RegisterCustomerAsync()
        //{
        //    var c = new Customer()
        //    {
        //        CustomerId = 1,
        //        FirstName = "blank",
        //        LastName = "blank",
        //        Username = "blank",
        //        UserPassord = "blank",
        //    };
        //    var result = accountLogic.RegisterCustomerAsync(c);
        //    //Assert.Equal(true, result);
        //}
        //[Fact]
        //public void Task_CustomerListAsync()
        //{

        //}
        [Fact]
        public async Task Task_CustomerListAsync()
        {
            List<Customer> list = context.Customers.ToList();
            List<Customer> result = await accountLogic.CustomerListAsync();
            Xunit.Assert.Equal(list.Count(),result.Count());
            Xunit.Assert.NotNull(result);
        }
        [Fact]
        public async Task Task_LocationListAsync()
        {
            List<Store> list = context.Stores.ToList();
            List<Store> result = await accountLogic.LocationListAsync();
            Xunit.Assert.Equal(list.Count(), result.Count());
            Xunit.Assert.NotNull(result);
        }
        [Fact]
        public async Task Task_ItemListAsync()
        {
            List<Product> list = context.Products.ToList();
            List<Product> result = await accountLogic.ItemListAsync();
            Xunit.Assert.Equal(list.Count(), result.Count());
            //CollectionAssert.AreEqual(list, result);
            Xunit.Assert.NotNull(result);
        }
    }
}
