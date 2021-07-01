using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;

namespace BusinessLayer
{
    public class AccountLogic : IAccountLogic
    {
		private readonly P1DatabaseContext _context;
		public AccountLogic()
        {
			this._context = new P1DatabaseContext();
        }
		public AccountLogic(P1DatabaseContext context)
		{
			this._context = context;
		}
		public async Task<bool> RegisterCustomerAsync(Customer customer)
		{
			//create a try/catch to save the player
			await _context.Customers.AddAsync(customer);
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
		public int CreateCustomer(Customer c)
        {
			using (var context = new P1DatabaseContext())
			{
				var user = new Customer();
				user.FirstName = c.FirstName;
				user.LastName = c.LastName;
				user.Username = c.Username;
				user.UserPassord = c.UserPassord;

				//this.user = user;
				context.Customers.Add(user);
				//context.SaveChanges();
				return 0;
			}
        }
		public bool CheckLogin(Customer c)
        {
			using (var context = new P1DatabaseContext())
			{
				Customer user = context.Customers.Where(x => x.Username == c.Username).FirstOrDefault();
				if (context.Customers.Where(x => x.Username == c.Username).FirstOrDefault() != null)
				{
					if (context.Customers.Where(x => x.UserPassord == c.UserPassord).FirstOrDefault() != null)
					{
						return true;
					}
					else
					{
						Console.WriteLine("Your password was incorrect, please try again");
					}
				}
				else
				{
					Console.WriteLine("That username does not exist, please try again");
				}
				return false;
			}
		}
		public async Task<List<Customer>> CustomerListAsync()
		{
			using (var context = new P1DatabaseContext())
			{
				List<Customer> users = null;
				try
				{
					users = context.Customers.ToList();
				}
				catch (ArgumentNullException ex)
				{
					Console.WriteLine($"There was a problem gettign the players list");
				}
				return users;
			}
		}
		public async Task<List<Store>> LocationListAsync()
		{
			using (var context = new P1DatabaseContext())
			{
				List<Store> locations = null;
				try
				{
					locations = _context.Stores.ToList();
				}
				catch (ArgumentNullException ex)
				{
					Console.WriteLine($"There was a problem gettign the players list");
				}
				return locations;
			}
		}
		public async Task<List<Product>> ItemListAsync()
		{
			using (var context = new P1DatabaseContext())
			{
				List<Product> items = null;
				try
				{
					items = _context.Products.ToList();
				}
				catch (ArgumentNullException ex)
				{
					Console.WriteLine($"There was a problem gettign the players list");
				}
				return items;
			}
		}
		
	}
}
