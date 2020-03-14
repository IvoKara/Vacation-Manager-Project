using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.Shared;
using Web.Models.Teams;
using Data.Entitiy;
using Microsoft.AspNetCore.Identity;

namespace Web
{
    public class TeamsController : Controller
    {
        private readonly VacantionContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private const int PageSize = 10;
        
        public TeamsController(VacantionContext context, UserManager<User> userManager,
            RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        private async Task<User> GetCurrentUser()
        {
            return await _context.Users.
                 Include(u => u.Role).
                 Include(u => u.Team).
                 Include(u => u.Vacantions).
                 FirstAsync(u => u.UserName == User.Identity.Name);
        }

        // GET: Teams
        public async Task<IActionResult> Index(TeamsIndexViewModel model)
        {
            model.Pager = new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<Team> items = await _context.Teams.Skip((model.Pager.CurrentPage - 1) * PageSize).
                Take(PageSize).Select(u => new Team()
            {
                Id = u.Id,
                TeamName = u.TeamName,
                Developers = u.Developers,
                WorkingOnProject = u.WorkingOnProject,
                Leader = u.Leader

            }).Where(x => x.TeamName != "-").ToListAsync();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await _context.Teams.CountAsync() / (double)PageSize);

            return View(model);
        }

        // GET: Teams/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Teams/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                User currentUser = await GetCurrentUser();

                Team newTeam = new Team
                {
                    TeamName = model.TeamName,
                    WorkingOnProject = _context.Projects.First(t => t.ProjectName == model.WorkingOnProject),
                    //Developers = model.Developers.ToList();
                };

                if (model.Developers[0] == null && model.Developers[1] == null)
                {
                    ModelState.AddModelError("Developers", "Team must have at least two members exept Leader.");
                    return View(model);
                }

                try
                {
                    string[] names = model.Leader.Split();
                    newTeam.Leader = _context.Users.First(u => u.FirstName == names[0] && u.LastName == names[1]);
                    newTeam.Leader.Role = _context.Roles.First(r => r.Name == "Team Lead");
                    newTeam.Leader.Team = newTeam;
                }
                catch
                {
                    ModelState.AddModelError("Leader", "No such user.");
                    return View(model);
                }

                for (int i = 0; i < model.Developers.Count(); i++)
                {
                    if (model.Developers[i] == null)
                    {
                        break;
                    }

                    string[] names = model.Developers[i].Split();
                    if (_context.Users.Any(u => u.FirstName == names[0] && u.LastName == names[1]))
                    {
                        User member = _context.Users.First(u => u.FirstName == names[0] && u.LastName == names[1]);
                        member.Team = newTeam;
                    }
                    else
                    {
                        ModelState.AddModelError($"Developers[{i}]", "No such user.");
                        return View(model);
                    }
                }

                _context.Add(newTeam);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Teams/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}