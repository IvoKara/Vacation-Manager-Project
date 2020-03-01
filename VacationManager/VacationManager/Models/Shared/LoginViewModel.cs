using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Shared
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email required to register.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required to register.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
