using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IOrderLogic
    {
        Task<List<Order>> OrderListAsync(int id);
        Task<List<Order>> StoreOrderListAsync(int id);
        Task<List<OrderItem>> OrderItemListAsync(int id);

    }
}
