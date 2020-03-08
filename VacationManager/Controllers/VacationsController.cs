using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Data.Entitiy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.Shared;
using Web.Models.Vacations;

namespace Web.Controllers
{
    public class VacationsController : Controller
    {
        private readonly VacantionContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private const int PageSize = 10;

        public VacationsController(VacantionContext context, UserManager<User> userManager,
            RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        // GET: Users
        public async Task<IActionResult> Index(VacationsIndexViewModel model)
        {
            if(!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction(nameof(NotLoggedIn));
            }
            User user = await _context.Users.
                 Include(u => u.Role).
                 Include(u => u.Team).
                 FirstAsync(u => u.UserName == User.Identity.Name);
            if (user.Role.Name == "CEO" /*|| user.Role.Name == "Team Lead"*/)
            {
                return RedirectToAction(nameof(ApproveRequests));
            }
            model.Pager = new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            /*List<Vacantion> items = user.Vacantions.Skip((model.Pager.CurrentPage - 1) * PageSize).
                Take(PageSize).Select(v => new Vacantion()
                {
                    Id = v.Id,
                    Type = v.Type,
                    FromDate = v.FromDate,
                    ToDate = v.ToDate,
                    HalfDayVacantion = v.HalfDayVacantion,
                    Verified = v.Verified,
                    //FromUser = v.FromUser

                }).ToList();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await _context.Users.CountAsync() / (double)PageSize);*/

            return View(model);
        }

        public IActionResult NotLoggedIn()
        {
            return View();
        }

        public IActionResult ApproveRequests()
        {
            return View();
        }

        // GET: Vacations/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Vacations/Create
        public ActionResult SendRequest()
        {
            return View();
        }

        // POST: Vacations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendRequest(IFormCollection collection)
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

        // GET: Vacations/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Vacations/Edit/5
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

        // GET: Vacations/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vacations/Delete/5
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