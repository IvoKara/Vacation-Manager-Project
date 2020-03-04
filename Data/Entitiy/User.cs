using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Data.Entitiy;
using Microsoft.AspNetCore.Identity;

namespace Data.Entitiy
{
    public class User : IdentityUser<int>
    {
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public Team Team { get; set; }
    }
}
