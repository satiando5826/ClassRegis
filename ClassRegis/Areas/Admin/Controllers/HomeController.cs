using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRegis.Data;
using ClassRegis.Models;
using ClassRegis.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRegis.Areas.Admin
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminVM AdminVM { get; set; }


        public HomeController(ApplicationDbContext db)
        {
            _db = db;

            AdminVM = new AdminVM()
            {
                Subjects = _db.Subjects.ToList(),
                Rooms = _db.Rooms.ToList()
            };
        }

        
        public async Task<IActionResult> Index()
        {
            AdminVM.Subjects = await _db.Subjects.ToListAsync();
            AdminVM.Rooms = await _db.Rooms.ToListAsync();
            return View(AdminVM);
        }

        //get Create subject
        public IActionResult CreateSubject()
        {
            return View();
        }

        //post Create subject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubject(Subjects subjects)
        {
            if (ModelState.IsValid)
            {
                _db.Add(subjects);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subjects);
        }

        //get Edit subject

    }
}