using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Data.Entitiy;

namespace Data.Entitiy
{
    public class Project
    {
        public int Id { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }

        [Required]
        [InverseProperty("WorkingOnProject")]
        public ICollection<Team> WorkingTeams { get; set; }
    }
}
