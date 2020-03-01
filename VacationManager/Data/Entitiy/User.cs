using System;
using System.Collections.Generic;
using System.Text;
using Data.Entitiy;
using Microsoft.AspNetCore.Identity;

namespace Data.Entitiy
{
    public class User : IdentityUser<int>
    {
        /*public int Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }*/

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public Team Team { get; set; }
    }
}
