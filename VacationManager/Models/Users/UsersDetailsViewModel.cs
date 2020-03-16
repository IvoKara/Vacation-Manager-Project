using Data.Entitiy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Users
{
    public class UsersDetailsViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username required.")]
        [MaxLength(50, ErrorMessage = "Username cannot be more than 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You need to place a Name.")]
        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage = "Name should consist of letters only and capital first letter")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You need to place a Name.")]
        [MaxLength(50, ErrorMessage = "Username cannot be more than 50 characters")]
        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage = "Name should consist of letters only and capital first letter")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email required.")]
        [EmailAddress]
        public string Email { get; set; }
        
        public string Role { get; set; }

        public Team Team { get; set; }

        public bool IsLeader { get; set; }
    }
}
