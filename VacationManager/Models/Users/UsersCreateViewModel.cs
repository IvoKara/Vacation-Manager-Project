using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Users
{
    public class UsersCreateViewModel
    {
        [Required]
        [MaxLength(80, ErrorMessage = "UserName name cannot be longer than 80 characters")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "FirstName name cannot be longer than 80 characters")]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(80, ErrorMessage = "LastName name cannot be longer than 80 characters")]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(20, ErrorMessage = "Role name cannot be longer than 20 characters")]
        public string Role { get; set; }

        //[Required]
        //public Teams.TeamsViewModel Team { get; set; }
    }
}
