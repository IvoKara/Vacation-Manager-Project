using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Data.Entitiy;

namespace Data.Entitiy
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        public string TeamName { get; set; }

        [Required]
        [InverseProperty("Team")]
        public ICollection<User> Developers { get; set; }

        public User Leader { get; set; }

        public Project WorkingOnProject { get; set; }

    }
}
