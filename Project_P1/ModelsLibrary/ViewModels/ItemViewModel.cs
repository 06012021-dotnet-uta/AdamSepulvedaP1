using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsLibrary.ViewModels
{
    public class ItemViewModel
    {
        //public IEnumerable<Product> Products { get; set; }
        //public IEnumerable<Store> Stores { get; set; }
        //public IEnumerable<StoreInventory> StoreInventories { get; set; }
        //public int OrderId { get; set; }
        public int ProductId { get; set; }
        //public int CustomerId { get; set; }
        //public int StoreId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public string ProductDesc { get; set; }
        [Required]
        public int Stock { get; set; }
        [Range(1,int.MaxValue, ErrorMessage ="The value must be greater than zero")]
        public int OrderQuantity { get; set; }
        public ItemViewModel()
        {
            
        }
    }
}
