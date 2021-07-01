using System;
using Xunit;
using BusinessLayer;
using ModelsLibrary;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Linq;
using ModelsLibrary.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting 
{
    
    public class ItemTest
   {
        //private readonly DbContextOptions options = new DbContextOptions<P1DatabaseContext>().UseInMemoryDatabase(databaseName: "TestaDb").Options;
        public P1DatabaseContext context = new P1DatabaseContext();
        public ItemLogic itemLogic = new ItemLogic();

        public CartViewModel AddItemToCart(CartViewModel cart, ItemViewModel item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateOrderAsync(CartViewModel cart)
        {
            throw new NotImplementedException();
        }

        public ItemViewModel GetItemViewModelFromProductId(int id, int stock)
        {
            throw new NotImplementedException();
        }
        [Fact]
        public async Task Task_QueryItemViewAsync()
        {
            int id = -1;
            List<ItemViewModel> result = await itemLogic.QueryItemView(id);
            Xunit.Assert.Empty(result);
            id = 2;
            List<ItemViewModel> items = (from m1 in context.Products
                     join m2 in context.StoreInventories
                     on m1.ProductId equals m2.ProductId
                     where m2.StoreId == id
                     select new ItemViewModel
                     {
                         ProductId = m1.ProductId,
                         ProductName = m1.ProductName,
                         Price = m1.Price,
                         ProductDesc = m1.ProductDesc,
                         Stock = m2.Quantity,
                         OrderQuantity = 0
                     }
                         ).ToList();
            result = await itemLogic.QueryItemView(id);
            Xunit.Assert.Equal(items.Count(), result.Count());
            //CollectionAssert.AreEquivalent(items,result);
        }
    }
}
