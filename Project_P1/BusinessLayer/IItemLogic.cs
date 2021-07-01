using ModelsLibrary;
using ModelsLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IItemLogic
    {
        public ItemViewModel GetItemViewModelFromProductId(int id, int stock);
        public Task<List<ItemViewModel>> QueryItemView(int id);
        public CartViewModel AddItemToCart(CartViewModel cart, ItemViewModel item);
        public Task<bool> CreateOrderAsync(CartViewModel cart);
    }
}
