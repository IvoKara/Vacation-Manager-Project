using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Roles;
using Data.Entitiy;
using Microsoft.AspNetCore.Identity;

namespace Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly VacantionContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private const int PageSize = 10;

        public RolesController(VacantionContext context, UserManager<User> userManager,
            RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TeamLead()
        {
            return View();
        }
        public IActionResult CEO()
        {
            return View();
        }
        public IActionResult Unassigned()
        {
            return View();
        }
        public IActionResult Developer()
        {
            return View();
        }
    }
}