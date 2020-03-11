using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entitiy
{
    public class Vacantion
    {
        public Vacantion()
        {
            this.DateOfCreation = DateTime.Now;
            this.IsApproved = false;
            this.IsPending = true;
        }

        [Key]
        public int Id { get; set; }
       
        public string Type { get; set; }

        public DateTime DateOfCreation { get; set; }
        
        public DateTime FromDate { get; set; }
       
        public DateTime ToDate { get; set; }

        public bool HalfDayVacantion { get; set; }
        
        public bool IsApproved { get; set; }

        public bool IsPending { get; set; }

        public bool Editted { get; set; }

        [Required]
        public User FromUser { get; set; }

        public byte[] ImageUpload { get; set; }
    }
}
