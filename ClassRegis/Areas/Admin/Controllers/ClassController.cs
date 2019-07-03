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
using Microsoft.EntityFrameworkCore;

namespace ClassRegis.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser+ "," + SD.AdminTeacher)]
    [Area("Admin")]
    public class ClassController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ClassViewModel ClassVM { get; set; }
        public ClassController(ApplicationDbContext db)
        {
            _db = db;
            ClassVM = new ClassViewModel()
            {
                Subjects = new List<Subjects>(),
                Rooms = new List<Rooms>(),
                Teachers = new List<Teachers>(),
                Classes = new Models.Classes(),
                StudyClasses = new StudyClasses()
            };
            ClassVM.Classes = _db.Classes.Include(c => c.Rooms)
                                .Include(c => c.Subjects).Include(c => c.Teachers).FirstOrDefault();
            ClassVM.Subjects = _db.Subjects.ToList();
            ClassVM.Rooms = _db.Rooms.ToList();
            ClassVM.Teachers = _db.Teachers.ToList();
            
        }



        public IActionResult Index()
        {
            return View(_db.Classes);
        }

        //get Create Class
        public IActionResult Create()
        {
            if (User.IsInRole(SD.AdminTeacher))
            {
                ClassVM.accountTeacher = ClassVM.Teachers
                                                 .Where(t => t.Email == User.Identity.Name).FirstOrDefault();
            }
            return View(ClassVM);
        }


        //post Create Class
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                _db.Add(ClassVM.Classes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ClassVM.Classes);
        }

        //get Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classes = await _db.Classes.FindAsync(id);
            if (classes == null)
            {
                return NotFound();
            }

            return View(classes);
        }

        //get Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classes = await _db.Classes.FindAsync(id);
            if (classes == null)
            {
                return NotFound();
            }

            return View(classes);
        }

        //post Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var classes = await _db.Classes.FindAsync(id);
            _db.Classes.Remove(classes);
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

            var classes = await _db.Classes.FindAsync(id);
            if (classes == null)
            {
                return NotFound();
            }
            ClassVM.Classes = classes;
            return View(ClassVM);
        }

        //post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Classes classes)
        {
            if (id != classes.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(classes.Id);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classes);
        }

        ////show Enrolled
        public IActionResult ShowEnroll(int? id)
        {
            List<StudyClasses> studentclass = _db.StudyClasses.Where(sc => sc.classId == id).Include(sc =>sc.Student).ToList();
           
            List<Models.Students> studentEnroll = new List<Models.Students>();

            foreach (var studentss in studentclass)
            {
                studentEnroll.Add(studentss.Student);
            }

            return View(studentEnroll);
        }
    }
}