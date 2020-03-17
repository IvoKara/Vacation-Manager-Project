using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Shared;
using Data.Entitiy;

namespace Web.Models.Vacations
{
    public class VacationsIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<Vacantion> Items { get; set; }
    }
}
