using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Shared;
using Data.Entitiy;

namespace Web.Models.Users
{
    public class UsersIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<User> Items { get; set; }
    }
}
