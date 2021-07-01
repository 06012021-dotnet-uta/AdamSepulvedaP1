using System;
using System.Collections.Generic;

#nullable disable

namespace ModelsLibrary
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
            StoreInventories = new HashSet<StoreInventory>();
        }

        public int StoreId { get; set; }
        public string StoreStreet { get; set; }
        public string StoreCity { get; set; }
        public string StoreState { get; set; }
        public int StoreZip { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
    }
}
