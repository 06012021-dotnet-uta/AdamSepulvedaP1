using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ModelsLibrary
{
    public partial class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Quantity")]
        public int OrderQuantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public decimal tp
        {
            get
            {
                return Product.Price * OrderQuantity;
            }
        }
    }
}
