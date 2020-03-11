using Data.Entitiy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
    
namespace Web.Models.Vacations
{
    public class VacationsDetailsViewModel
    {
        public int Id { get; set;
        }
        public string Type { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DateTime DateOfCreation { get; set; }

        public bool IsApproved { get; set; }

        public bool IsPending { get; set; }

        public bool HalfDayVacantion { get; set; }

        public byte[] ImageUpload { get; set; }

        public User FromUser { get; set; }
    }
}
