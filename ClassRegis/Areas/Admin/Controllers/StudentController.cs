using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRegis.Data;
using ClassRegis.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRegis.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class StudentController : Controller
    {

        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Students.Include(s=>s.StudyClasses).ToList());
        }//get Create Teacher


        public IActionResult Create()
        {
            return View();
        }


        //post Create Teacher
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassRegis.Models.Students students)
        {
            if (ModelState.IsValid)
            {
                _db.Add(students);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(students);
        }

        //get Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _db.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        //get Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _db.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        //post Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _db.Students.FindAsync(id);
            _db.Students.Remove(student);
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

            var student = await _db.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        //post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClassRegis.Models.Students students)
        {
            if (id != students.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(students);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(students);
        }

    }
}