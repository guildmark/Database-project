using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Models.Details
{
    public class UserDetails
    {

        public UserDetails() { }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required!")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="Password is required!")]
        [DataType(DataType.Password)]
        [MinLength(3,ErrorMessage = "Minimum 3 characters required")]
        public string Password { get; set; }
 
        public int Image { get; set; }

        public string Description { get; set; }
        [Display(Name = "Registered Mail")]
        public string Mail { get; set; }
    }
}
