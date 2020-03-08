using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.Shared;
using Web.Models.Users;
using Data.Entitiy;
using Microsoft.AspNetCore.Identity;

namespace Web
{
    public class UsersController : Controller
    {
        private readonly VacantionContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private const int PageSize = 10;

        public UsersController(VacantionContext context, UserManager<User> userManager,
            RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        // GET: Users
        public async Task<IActionResult> Index(UsersIndexViewModel model)
        {
            model.Pager = new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<User> items = await _context.Users.Skip((model.Pager.CurrentPage - 1) * PageSize).
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

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsersCreateViewModel model)
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
                    Role = _roleManager.Roles.First(x => x.Name == model.Role),
                    Team = _context.Teams.FirstOrDefault(x => x.TeamName == model.Team),
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var res2 = await _userManager.AddToRoleAsync(newUser, model.Role);
                    //newUser.Role.UsersInRole.Add(newUser);
                    //await _signInManager.SignInAsync(newUser, false);
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

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await _context.Users.
                 Include(u => u.Role).
                 Include(u => u.Team).
                 FirstAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            UsersDetailsViewModel model = new UsersDetailsViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Team = user.Team.TeamName,
                Role = user.Role.Name
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await _context.Users.FirstAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            UsersUserEditViewModel model = new UsersUserEditViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password
            };

            return View(model);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(UsersUserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id.ToString());
                user.Id = model.Id;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.EmailConfirmed = true;
                user.Password = model.Password; 
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                user.SecurityStamp = Guid.NewGuid().ToString();

                await _signInManager.SignOutAsync();
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
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

        // GET: Users/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await _context.Users.
                Include(u => u.Role).
                Include(u => u.Team).
                FirstAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            ViewData["EditUser"] = user.UserName;
           // ViewData["BeforeRole"] = user.Role.Name;
            ViewData["BeforeTeam"] = user.Team;

            UsersEditViewModel model = new UsersEditViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                //Password = user.Password,
                //Role = user.Role.Name,
                //Team = user.Team.TeamName
            };

            return View(model);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsersEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.Include(u => u.Role).Include(u => u.Team).FirstAsync(u => u.Id == model.Id);
                user.Id = model.Id;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.EmailConfirmed = true;
                string beforeRole = user.Role.Name;
                user.Role = _roleManager.Roles.First(x => x.Name == model.Role);
                user.Team = _context.Teams.FirstOrDefault(x => x.TeamName == model.Team);
                user.SecurityStamp = Guid.NewGuid().ToString();

                //await _signInManager.SignOutAsync();

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if(beforeRole != user.Role.Name)
                    {
                        var res3 = await _userManager.RemoveFromRoleAsync(user, beforeRole);
                        //user.Role.UsersInRole.Remove(user);
                        var res2 = await _userManager.AddToRoleAsync(user, model.Role);
                        //user.Role.UsersInRole.Add(user);
                    }

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

        public async Task<IActionResult> Delete(int id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            User logged = await _userManager.FindByNameAsync(User.Identity.Name);
            if (logged.UserName == user.UserName)
            {
                await _signInManager.SignOutAsync();
            }
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}