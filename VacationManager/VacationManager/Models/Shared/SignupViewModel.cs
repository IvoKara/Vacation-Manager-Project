using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Shared
{
    public class SignupViewModel
    { 
        [Required(ErrorMessage = "Username required to register.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You need to place your Name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You need to place your Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email required to register.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required to register.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; }

        public string Team { get; set; }
    }
}
