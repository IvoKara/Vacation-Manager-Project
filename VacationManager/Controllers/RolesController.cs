using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly VacantionContext _context;
        private const int PageSize = 10;

        public RolesController(VacantionContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}