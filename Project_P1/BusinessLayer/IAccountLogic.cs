using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IAccountLogic
    {
        Task<bool> RegisterCustomerAsync(Customer customer);
        int CreateCustomer(Customer c);
        bool CheckLogin(Customer c);
        Task<List<Customer>> CustomerListAsync();
        Task<List<Store>> LocationListAsync();
        Task<List<Product>> ItemListAsync();
    }
}
