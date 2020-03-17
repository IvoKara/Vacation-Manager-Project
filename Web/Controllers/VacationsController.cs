using System;
using System.Collections.Generic;
using System.IO;
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
        private string[] vacationTypes = { "paid", "unpaid", "sick" };
        public VacationsController(VacantionContext context, UserManager<User> userManager,
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

        // GET: Users
        public async Task<IActionResult> Index(VacationsIndexViewModel model)
        {
            if(!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction(nameof(NotLoggedIn));
            }

            model.Pager = new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            User currentUser = await GetCurrentUser();

            List<Vacantion> items = currentUser.Vacantions.Skip((model.Pager.CurrentPage - 1) * PageSize).
                Take(PageSize).Select(v => new Vacantion()
                {
                    Id = v.Id,
                    Type = v.Type,
                    FromDate = v.FromDate,
                    ToDate = v.ToDate,
                    HalfDayVacantion = v.HalfDayVacantion,
                    IsPending = v.IsPending,
                    IsApproved = v.IsApproved,
                    DateOfCreation = v.DateOfCreation,
                    Editted = v.Editted

                }).ToList();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await _context.Users.CountAsync() / (double)PageSize);

            return View(model);
        }

        public async Task<IActionResult> Approve(int id)
        {
            Vacantion vacation = await _context.Vacantions.FirstAsync(u => u.Id == id);
            vacation.IsApproved = true;
            vacation.IsPending = false;
            _context.SaveChanges();
            return RedirectToAction(nameof(ApproveRequests));
        }

        public async Task<IActionResult> Reject(int id)
        {
            Vacantion vacation = await _context.Vacantions.FirstAsync(u => u.Id == id);
            vacation.IsApproved = false;
            vacation.IsPending = false;
            _context.SaveChanges();
            return RedirectToAction(nameof(ApproveRequests));
        }

        public IActionResult NotLoggedIn()
        {
            return View();
        }

        public async Task<IActionResult> ApproveRequests(VacationsIndexViewModel model)
        {
            model.Pager = new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            User currentUser = await GetCurrentUser();

            var vacations = _context.Vacantions.Include(v => v.FromUser).Where(v => v.IsPending);
            if (currentUser.Role.Name == "Team Lead")
            {
                vacations = vacations.Where(v => v.FromUser.Team == currentUser.Team && v.FromUser != currentUser);
            }

            List<Vacantion> items = vacations.Skip((model.Pager.CurrentPage - 1) * PageSize)
                .Take(PageSize).Select(v => new Vacantion()
                    {
                        Id = v.Id,
                        FromUser = v.FromUser,
                        Type = v.Type,
                        FromDate = v.FromDate,
                        ToDate = v.ToDate,
                        HalfDayVacantion = v.HalfDayVacantion,
                        IsPending = v.IsPending,
                        IsApproved = v.IsApproved,
                        DateOfCreation = v.DateOfCreation,
                        Editted = v.Editted
                    })
                .ToList();

            model.Items = items;
            model.Pager.PagesCount = (int)Math.Ceiling(await _context.Users.CountAsync() / (double)PageSize);

            return View(model);
        }

        // GET: Vacations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vacantion vacantion = await _context.Vacantions.Include(v => v.FromUser).FirstAsync(u => u.Id == id);
            if (vacantion == null)
            {
                return NotFound();
            }
            VacationsDetailsViewModel model = new VacationsDetailsViewModel
            {
                Id = vacantion.Id,
                FromDate = vacantion.FromDate,
                ToDate = vacantion.ToDate,
                Type = vacantion.Type,
                DateOfCreation = vacantion.DateOfCreation,
                HalfDayVacantion = vacantion.HalfDayVacantion,
                ImageUpload = vacantion.ImageUpload,
                IsApproved = vacantion.IsApproved,
                IsPending = vacantion.IsPending,
                FromUser = vacantion.FromUser
            };
            return View(model);
        }

        // GET: Vacations/Create
        [HttpGet]
        public IActionResult SendRequest()
        {
            return View();
        }

        // POST: Vacations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendRequest(VacationsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                User currentUser = await GetCurrentUser();

                Vacantion newVacation = new Vacantion
                {
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    Type = model.Type,
                    FromUser = currentUser,
                    HalfDayVacantion = model.HalfDayVacantion
                };

                bool mark = false;
                if(model.FromDate > model.ToDate)
                {
                    ModelState.AddModelError("FromDate", "From Date must be less than To Date.");
                    ModelState.AddModelError("ToDate", "From Date must be less than To Date.");
                    mark = true;
                }

                if (model.Type == "sick" && model.ImageUpload == null)
                {
                    ModelState.AddModelError("ImageUpload", "Must upload image of sheet or record.");
                    mark = true;
                }
                else if (model.ImageUpload != null)
                {
                    if (model.ImageUpload.Length > 0)
                    //Convert Image to byte and save to database
                    {
                        byte[] p1 = null;
                        using (var fs1 = model.ImageUpload.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        newVacation.ImageUpload = p1;
                    }
                }
                
                if(mark)
                {
                    return View(model);
                }

                _context.Add(newVacation);
                _context.SaveChanges();
                
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // GET: Vacations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vacantion vacantion = await _context.Vacantions.Include(v => v.FromUser).FirstAsync(u => u.Id == id);
            if (vacantion == null)
            {
                return NotFound();
            }
            VacationsEditViewModel model = new VacationsEditViewModel
            {
                Id = vacantion.Id,
                FromDate = vacantion.FromDate,
                ToDate = vacantion.ToDate,
                Type = vacantion.Type,
                HalfDayVacantion = vacantion.HalfDayVacantion,
            };
            
            if(model.Type == "sick")
            {
                model.CurrentImage = vacantion.ImageUpload;

                /*var stream = new MemoryStream(model.CurrentImage);
                model.ImageUpload = new FormFile(stream, 0, model.CurrentImage.Length, "file", "sickSheet");*/
            }

            return View(model);
        }

        // POST: Vacations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VacationsEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Vacantion vacation = await _context.Vacantions.Include(v => v.FromUser).FirstAsync(u => u.Id == model.Id);
                vacation.Id = model.Id;
                vacation.FromDate = model.FromDate;
                vacation.ToDate = model.ToDate;
                vacation.Type = model.Type;
                vacation.HalfDayVacantion = model.HalfDayVacantion;
                vacation.DateOfCreation = DateTime.Now;
                vacation.Editted = true;

                if(vacation.Type == "sick")
                {
                    vacation.HalfDayVacantion = false;
                    vacation.ImageUpload = model.CurrentImage;

                    if (model.CurrentImage == null && model.ImageUpload == null)
                    {
                        ModelState.AddModelError("ImageUpload", "Must upload image of sheet or record.");
                        return await Edit(model.Id);
                    }
                    else if (model.ImageUpload != null)
                    {
                        if (model.ImageUpload.Length > 0)
                        //Convert Image to byte and save to database
                        {
                            byte[] p1 = null;
                            using (var fs1 = model.ImageUpload.OpenReadStream())
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                            }
                            vacation.ImageUpload = p1;
                        }
                    }
                }
                else
                {
                    vacation.ImageUpload = null;
                }
                

                _context.Update(vacation);
                _context.SaveChanges();

                return RedirectToAction("Details", "Vacations", new { id = vacation.Id });
            }

            return View(model);
        }

        // POST: Vacations/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Vacantion vacation = await _context.Vacantions.FirstAsync(u => u.Id == id);
            _context.Remove(vacation);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}