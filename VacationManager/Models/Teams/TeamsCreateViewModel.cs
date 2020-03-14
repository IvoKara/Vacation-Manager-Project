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
        [Required(ErrorMessage = "Team must have a Name!")]
        public string TeamName { get; set; }

        //[Required(ErrorMessage = "Team must have at least two members.")]
        public string[] Developers { get; set; }

        //public User Leader { get; set; }

        //[Required(ErrorMessage = "Team must be working on existing or new project.")]
        public string WorkingOnProject { get; set; }

        public bool NewProject { get; set; }

        [Required(ErrorMessage = "Team must have a Leader!")]

        public string Leader { get; set; }
    }
}
