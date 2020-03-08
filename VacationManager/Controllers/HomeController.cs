using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Context;
using Data.Entitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VacationManager.Models;
using Web.Models.Shared;

namespace VacationManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly VacantionContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private const int PageSize = 10;
        private string[] roles = { "Unassigned", "Team Lead", "Developer", "CEO" };

        public HomeController(VacantionContext context, UserManager<User> userManager,
            SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            foreach (var role in roles)
            {
                if (!_roleManager.Roles.Any(x => x.Name == role))
                {
                    _context.Add(new Role() { Name = role, NormalizedName = role.ToUpper() });
                    _context.SaveChanges();
                }
            }
            if(!_context.Teams.Any(x => x.TeamName == "-"))
            {
                _context.Add(new Team() { TeamName = "-" });
                _context.SaveChanges();
            }
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, lockoutOnFailure:false);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt!");
                await _signInManager.SignOutAsync();
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            SignupViewModel model = new SignupViewModel();
            return  View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    Password = model.Password,
                    Role = _roleManager.Roles.First(x => x.Name == "Unassigned"),
                    Team = _context.Teams.FirstOrDefault(x => x.TeamName == "-"),
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var res2 = await _userManager.AddToRoleAsync(newUser, "Unassigned");
                    //newUser.Role.UsersInRole.Add(newUser);
                    await _signInManager.SignInAsync(newUser, false);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
