using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ModelsLibrary
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        [Display(Name = "Name")]
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string UserPassord { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
