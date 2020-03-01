using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private const int PageSize = 10;

        public HomeController(VacantionContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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
                User signedUser = await _userManager.FindByEmailAsync(model.Email);
                await _signInManager.SignInAsync(signedUser, false);

                if (await _userManager.CheckPasswordAsync(signedUser, model.Password))
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt!");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return  View();
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
                    Password = model.Password,
                    Role = "Unassigned"
                };

                _context.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
