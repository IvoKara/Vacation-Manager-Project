using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Microsoft.AspNetCore.Mvc;
using Data.Entitiy;
using Microsoft.AspNetCore.Identity;
using Web.Models.Users;
using Web.Models.Shared;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> TeamLead(UsersIndexViewModel model)
        {
            model.Pager = new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<User> items = await _context.Users.Include(u => u.Role)
                .Where(u => u.Role.Name == "Team Lead")
                .Skip((model.Pager.CurrentPage - 1) * PageSize).
                Take(PageSize).Select(u => new User()
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Role = u.Role,
                    Team = u.Team

                }).ToListAsync();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await _context.Users.CountAsync() / (double)PageSize);
            return View("~/Views/Users/Index.cshtml", model);
        }

        public async Task<IActionResult> CEO(UsersIndexViewModel model)
        {
            model.Pager = new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<User> items = await _context.Users.Include(u => u.Role)
                .Where(u => u.Role.Name == "CEO")
                .Skip((model.Pager.CurrentPage - 1) * PageSize).
                Take(PageSize).Select(u => new User()
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Role = u.Role,
                    Team = u.Team

                }).ToListAsync();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await _context.Users.CountAsync() / (double)PageSize);
            return View("~/Views/Users/Index.cshtml", model);
        }

        public async Task<IActionResult> Developer(UsersIndexViewModel model)
        {
            model.Pager = new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<User> items = await _context.Users.Include(u => u.Role)
                .Where(u => u.Role.Name == "Developer")
                .Skip((model.Pager.CurrentPage - 1) * PageSize).
                Take(PageSize).Select(u => new User()
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Role = u.Role,
                    Team = u.Team

                }).ToListAsync();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await _context.Users.CountAsync() / (double)PageSize);
            return View("~/Views/Users/Index.cshtml", model);
        }

        public async Task<IActionResult> Unassigned(UsersIndexViewModel model)
        {
            model.Pager = new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<User> items = await _context.Users.Include(u => u.Role)
                .Where(u => u.Role.Name == "Unassigned")
                .Skip((model.Pager.CurrentPage - 1) * PageSize).
                Take(PageSize).Select(u => new User()
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Role = u.Role,
                    Team = u.Team

                }).ToListAsync();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await _context.Users.CountAsync() / (double)PageSize);
            return View("~/Views/Users/Index.cshtml", model);
        }
    }
}