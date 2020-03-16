using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Projects
{
    public class ProjectsEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Project cannot be more than 50 characters")]
        public string ProjectName { get; set; }

        [MaxLength(250, ErrorMessage = "Max characters for decsription are 250")]
        public string Description { get; set; }
    }
}
