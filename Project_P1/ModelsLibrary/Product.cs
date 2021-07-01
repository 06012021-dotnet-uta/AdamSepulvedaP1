using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ModelsLibrary
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
            StoreInventories = new HashSet<StoreInventory>();
        }

        public int ProductId { get; set; }
        [Display(Name ="Product")]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Description")]
        public string ProductDesc { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
    }
}
