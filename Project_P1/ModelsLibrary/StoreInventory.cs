using System;
using System.Collections.Generic;

#nullable disable

namespace ModelsLibrary
{
    public partial class StoreInventory
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
