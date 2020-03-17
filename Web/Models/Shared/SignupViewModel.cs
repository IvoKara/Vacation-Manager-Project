using Microsoft.AspNetCore.Mvc;
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
        [MaxLength(50, ErrorMessage = "Username cannot be more than 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You need to place your Name.")]
        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage = "Name should consist of letters only and capital first letter")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You need to place your Name")]
        [MaxLength(50, ErrorMessage = "Username cannot be more than 50 characters")]
        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage = "Name should consist of letters only and capital first letter")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email required to register.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required to register.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
