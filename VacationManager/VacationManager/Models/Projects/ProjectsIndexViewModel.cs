using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Shared;
using Data.Entitiy;

namespace Web.Models.Projects
{
    public class ProjectsIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<Project> Items { get; set; }
    }
}
