using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRegis.Data;
using ClassRegis.Models;
using ClassRegis.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClassRegis.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminVM AdminVM { get; set; }

        public SubjectController(ApplicationDbContext db)
        {
            _db = db;
            AdminVM = new AdminVM()
            {
                Subjects = _db.Subjects.ToList(),
                Rooms = _db.Rooms.ToList()
            };
        }
        public IActionResult Index()
        {
            return View();
        }

        

            //get Create subject
            public IActionResult Create()
        {
            return View();
        }


        //post Create subject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subjects subjects)
        {
            if (ModelState.IsValid)
            {
                _db.Add(subjects);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index),"Home",new { area = "Admin" });
            }
            return View(subjects);
        }
    }
}