using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using ModelsLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ItemLogic : IItemLogic
    {
        private readonly P1DatabaseContext _context;
        public ItemLogic()
        {
            this._context = new P1DatabaseContext();
        }
        public ItemViewModel GetItemViewModelFromProductId(int id, int stock)
        {
            var p = _context.Products.Where(x => x.ProductId == id).Single();
            ItemViewModel item = new ItemViewModel();
            item.ProductId = p.ProductId;
            item.ProductName = p.ProductName;
            item.Price = p.Price;
            item.ProductDesc = p.ProductDesc;
            item.OrderQuantity = 1;
            item.Stock = stock;
            return item;
        }
        public async Task<List<ItemViewModel>> QueryItemView(int id)
        {
            List<ItemViewModel> items = null;
            try
            {
                items = (from m1 in _context.Products
                             join m2 in _context.StoreInventories
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
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"There was a problem gettign the players list");
            }
            
            return items;
        }
        public CartViewModel AddItemToCart(CartViewModel cart, ItemViewModel item)
        {
            cart.TotalPrice += item.Price * item.OrderQuantity;
            cart.Items.Add(item);
            return cart;
        }

        public async Task<bool> CreateOrderAsync(CartViewModel cart)
        {
            Customer customer = _context.Customers.Where(x => x.CustomerId == cart.CustomerId).Single();
            Store store = _context.Stores.Where(x => x.StoreId == cart.StoreId).Single();
            Order order = new Order()
            {
                CustomerId = cart.CustomerId,
                StoreId = cart.StoreId,
                TotalPrice = cart.TotalPrice,
                OrderTime = DateTime.UtcNow,
                Customer = customer,
                Store = store,
            };
            await _context.Orders.AddAsync(order);
                try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"There was a problem updating the Db => {ex.InnerException}");
                return false;
            }
            catch (DbUpdateException ex)
            {       //change this to logging
                Console.WriteLine($"There was a problem updating the Db => {ex.InnerException}");
                return false;
            }
            await CreateOrderItemsAsync(cart);
            return true;
        }
        public async Task<bool> CreateOrderItemsAsync(CartViewModel cart)
        {
            Order order = _context.Orders.OrderBy(x => x.OrderId).Last();
            
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (ItemViewModel item in cart.Items)
            {
                OrderItem orderItem = new OrderItem()
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    OrderQuantity = item.OrderQuantity,
                    Product = _context.Products.Where(x => x.ProductId == item.ProductId).Single(),

                };
                await _context.OrderItems.AddAsync(orderItem);
                StoreInventory inven = _context.StoreInventories.Where(x => x.StoreId == cart.StoreId && x.ProductId == item.ProductId).Single();
                inven.Quantity -= item.OrderQuantity;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"There was a problem updating the Db => {ex.InnerException}");
                return false;
            }
            catch (DbUpdateException ex)
            {       //change this to logging
                Console.WriteLine($"There was a problem updating the Db => {ex.InnerException}");
                return false;
            }
            return true;
        }
    }
}