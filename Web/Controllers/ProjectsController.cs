using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.Shared;
using Web.Models.Projects;
using Data.Entitiy;

namespace Web
{
    public class ProjectsController : Controller
    {
        private readonly VacantionContext _context;
        private const int PageSize = 10;

        public ProjectsController(VacantionContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index(ProjectsIndexViewModel model)
        {
            model.Pager = new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<Project> items = await _context.Projects.Skip((model.Pager.CurrentPage - 1) * PageSize).
                Take(PageSize).Select(u => new Project()
            {
                Id = u.Id,
                ProjectName = u.ProjectName,
                Description = u.Description,
                WorkingTeams = u.WorkingTeams

            }).ToListAsync();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await _context.Projects.CountAsync() / (double)PageSize);

            return View(model);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectsCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                Project newProject = new Project
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description
                };

                _context.Add(newProject);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int ?id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project project = await _context.Projects.FirstAsync(u => u.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            ProjectsEditViewModel model = new ProjectsEditViewModel
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Description = project.Description,
                WorkingTeams = project.WorkingTeams
            };

            return View(model);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProjectsEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project
                {
                    Id = model.Id,
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    WorkingTeams = model.WorkingTeams
                };

                _context.Update(project);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Project project = await _context.Projects.FirstAsync(u => u.Id == id);
            _context.Remove(project);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}