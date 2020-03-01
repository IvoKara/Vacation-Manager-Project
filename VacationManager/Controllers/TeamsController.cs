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
                WorkingOnProject = u.WorkingOnProject

            }).ToListAsync();

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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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