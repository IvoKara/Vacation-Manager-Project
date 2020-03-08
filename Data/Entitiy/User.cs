using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        //[Required]
        public Role Role { get; set; }

        //[Required]
        //[InverseProperty("Developers")]
        public Team Team { get; set; }

        //[Required]
        [InverseProperty("FromUser")]
        public ICollection<Vacantion> Vacantions { get; set; }
    }
}
