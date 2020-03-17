using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Shared
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username required to login.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password required to login.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
