using Data.Entitiy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
    
namespace Web.Models.Vacations
{
    public class VacationsEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Must have type of the vacation.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Must have Date of the beginning of the vacation.")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "Must have Date of the ending of the vacation.")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        public User FromUser { get; set; }

        public bool HalfDayVacantion { get; set; }

        [HiddenInput]
        public byte[] CurrentImage { get; set; }

        //[Required(ErrorMessage = "Must upload image of sheet or record.")]
        [DataType(DataType.Upload)]
        public IFormFile ImageUpload { get; set; }
    }
}
