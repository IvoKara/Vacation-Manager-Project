﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entitiy
{
    public class Role : IdentityRole<int>
    { 
        //[Required]
        [InverseProperty("Role")]
        public ICollection<User> UsersInRole { get; set; }
    }
}
