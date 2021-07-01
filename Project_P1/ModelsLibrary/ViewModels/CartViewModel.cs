using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.ViewModels
{
    public class CartViewModel
    {
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual ICollection<ItemViewModel> Items { get; set; }
        public CartViewModel()
        {
            Items = new HashSet<ItemViewModel>();
        }
        

    }
}
