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
    public class OrderLogic : IOrderLogic
    {
        private readonly P1DatabaseContext _context;
        public OrderLogic()
        {
            this._context = new P1DatabaseContext();
        }
		public async Task<List<Order>> OrderListAsync(int id)
		{
			
			List<Order> orders = null;
			try
			{
				orders = _context.Orders.Where(x=>x.CustomerId==id).ToList();
				foreach (Order order in orders)
				{
					order.Customer = _context.Customers.Where(x => x.CustomerId == order.CustomerId).Single();
					order.Store = _context.Stores.Where(x => x.StoreId == order.StoreId).Single();
				}
			}
			catch (ArgumentNullException ex)
			{
				Console.WriteLine($"There was a problem gettign the players list");
			}
			return orders;
			
		}
		public async Task<List<Order>> StoreOrderListAsync(int id)
		{

			List<Order> orders = null;
			try
			{
				orders = _context.Orders.Where(x => x.StoreId == id).ToList();
				foreach (Order order in orders)
				{
					order.Customer = _context.Customers.Where(x => x.CustomerId == order.CustomerId).Single();
					order.Store = _context.Stores.Where(x => x.StoreId == order.StoreId).Single();
				}
			}
			catch (ArgumentNullException ex)
			{
				Console.WriteLine($"There was a problem gettign the players list");
			}
			return orders;

		}
		public async Task<List<OrderItem>> OrderItemListAsync(int id)
		{

			List<OrderItem> orderItems = null;
			try
			{
				orderItems = _context.OrderItems.Where(x => x.OrderId == id).ToList();
				foreach(OrderItem item in orderItems)
                {
					item.Product = _context.Products.Where(x => x.ProductId == item.ProductId).Single();

                }
			}
			catch (ArgumentNullException ex)
			{
				Console.WriteLine($"There was a problem gettign the players list");
			}
			return orderItems;

		}
	}
}
