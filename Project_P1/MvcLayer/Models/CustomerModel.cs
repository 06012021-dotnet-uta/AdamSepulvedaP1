using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLayer.Models
{
    public class CustomerModel
    {
        [Required(ErrorMessage = "The Fname is required")]
        [MinLength(5, ErrorMessage = "THis is the custom error message")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
    }
}
