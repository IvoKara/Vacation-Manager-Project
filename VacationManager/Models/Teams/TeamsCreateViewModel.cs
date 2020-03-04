using Data.Entitiy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Teams
{
    public class TeamsCreateViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
       // []
        public string TeamName { get; set; }

        [Required]
        [InverseProperty("Team")]
        public ICollection<User> Developers { get; set; }

        public User Leader { get; set; }

        //[Unique(ErrorMessage = "This already exist !!")]
        public Project WorkingOnProject { get; set; }
    }
}
