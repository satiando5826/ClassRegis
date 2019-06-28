using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRegis.Data;
using ClassRegis.Models;
using ClassRegis.Models.ViewModel;
using ClassRegis.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassRegis.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubjectController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            return View(_db.Subjects);
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
                return RedirectToAction(nameof(Index));
            }
            return View(subjects);
        }

        //get Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        //get Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjects = await _db.Subjects.FindAsync(id);
            if (subjects == null)
            {
                return NotFound();
            }

            return View(subjects);
        }

        //post Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var subjects = await _db.Subjects.FindAsync(id);
            _db.Subjects.Remove(subjects);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //get Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjects = await _db.Subjects.FindAsync(id);
            if (subjects == null)
            {
                return NotFound();
            }

            return View(subjects);
        }

        //post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subjects subjects)
        {
            if (id != subjects.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(subjects);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subjects);
        }
    }
}