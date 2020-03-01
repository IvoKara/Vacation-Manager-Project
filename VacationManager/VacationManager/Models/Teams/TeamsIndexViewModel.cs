using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Shared;
using Data.Entitiy;

namespace Web.Models.Teams
{
    public class TeamsIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<Team> Items { get; set; }
    }
}
