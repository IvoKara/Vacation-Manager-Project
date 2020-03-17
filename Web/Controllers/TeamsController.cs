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
        private const int PageSize = 10;
        
        public TeamsController(VacantionContext context)
        {
            _context = context;
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
            return View(new TeamsCreateViewModel { BoxesToShow = 2 });
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
                    //TeamName = model.TeamName,
                    WorkingOnProject = _context.Projects.First(t => t.ProjectName == model.WorkingOnProject),
                    //Developers = model.Developers.ToList();
                };

                if (_context.Teams.Any(x => x.TeamName == model.TeamName))
                {
                    ModelState.AddModelError($"TeamName", "Team with such name exists already.");
                }
                else
                {
                    newTeam.TeamName = model.TeamName;
                }

                try
                {
                    string[] names = model.Leader.Split();
                    newTeam.Leader = _context.Users
                        .Include(u => u.Role)
                        .First(u => u.FirstName == names.First() && u.LastName == names.Last());
                    if (newTeam.Leader.Role.Name == "Team Lead")
                    {
                        ModelState.AddModelError("Leader", "This user is already a leader of an existing team.");
                        
                    }
                    else if (newTeam.Leader.Role.Name == "CEO")
                    {
                        ModelState.AddModelError("Leader", "This is the CEO.");
                    }
                    else
                    {
                        newTeam.Leader.Role = _context.Roles.First(r => r.Name == "Team Lead");
                        newTeam.Leader.Team = newTeam;
                    }
                }
                catch
                {
                    ModelState.AddModelError("Leader", "No such user.");
                }


                var developers = model.Developers.Where(c => c != null).ToArray();
                model.BoxesToShow = developers.Count() < 2 ? 2 : developers.Count();

                bool mark = false;
                if (developers.Count() < 2)
                {
                    ModelState.AddModelError("Developers", "Team must have at least two members exept the Leader.");
                    mark = true;
                }

                bool duplicates = false;
                if(developers.Count() != developers.Distinct().Count())
                {
                    duplicates = true;
                }

                for (int i = 0; i < developers.Count(); i++)
                {
                    if(duplicates)
                    {
                        for (int j = i + 1; j < developers.Count(); j++)
                        {
                            if(developers[j] == developers[i])
                            {
                                ModelState.AddModelError($"Developers[{j}]", "Already taken in above choices.");
                                break;
                            }
                        }
                    }

                    string[] names = developers[i].Split();
                    if (_context.Users.Any(u => u.FirstName == names.First() && u.LastName == names.Last()))
                    {
                        User member = _context.Users
                            .Include(u => u.Role)
                            .First(u => u.FirstName == names[0] && u.LastName == names[1]);
                        if (member == newTeam.Leader) 
                        {
                            ModelState.AddModelError($"Developers[{i}]", "This user is Team Leader of this team.");
                        }
                        else if (member.Role.Name == "Team Lead")
                        {
                            ModelState.AddModelError($"Developers[{i}]", "This user is a leader of an existing team.");
                        }
                        else
                        {
                            member.Team = newTeam;
                            member.Role = _context.Roles.First(r => r.Name == "Developer");
                        }
                    }
                    else
                    {
                        mark = true;
                        ModelState.AddModelError($"Developers[{i}]", "No such user.");
                    }
                }

                if (mark || duplicates)
                {
                    return View(model);
                }

                _context.Add(newTeam);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private bool CheckforDuplicates(string[] array)
        {
            var duplicates = array
             .GroupBy(p => p)
             .Where(g => g.Count() > 1)
             .Select(g => g.Key);


            return (duplicates.Count() > 0);
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

        // POST: Teams/Delete/5
       // [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Team team = await _context.Teams
                .Include(u => u.Developers)
                .Include(t => t.Leader)
                //.Include(x => x.WorkingOnProject)
                .FirstAsync(t => t.Id == id);

            foreach (var item in team.Developers)
            {
                item.Team = await _context.Teams.FirstAsync(x => x.TeamName == "-");
                item.Role = await _context.Roles.FirstAsync(x => x.Name == "Unassigned");
            }

            team.Leader.Team = await _context.Teams.FirstAsync(x => x.TeamName == "-");
            team.Leader.Role = await _context.Roles.FirstAsync(x => x.Name == "Unassigned");

            _context.Remove(team);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}