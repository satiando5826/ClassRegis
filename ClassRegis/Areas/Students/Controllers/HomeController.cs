using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClassRegis.Data;
using ClassRegis.Models;
using ClassRegis.Models.ViewModel;
using ClassRegis.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClassRegis.Areas.Students.Controllers
{
    [Area("Students")]

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ClasssesSearchViewModel classesSearchVM;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
            classesSearchVM = new ClasssesSearchViewModel()
            {
                Classes = new List<Classes>(),
                Students = new List<Models.Students>(),
                StudyClasses = new StudyClasses(),
                lstStudyClasses = new List<StudyClasses>()

            };
            classesSearchVM.Classes = _db.Classes.Include(c => c.Rooms)
                                .Include(c => c.Subjects).Include(c => c.Teachers).ToList();
            classesSearchVM.Students = _db.Students.ToList();
            classesSearchVM.lstStudyClasses = _db.StudyClasses.ToList();
        }

        public IActionResult Index(string subjectName = null, string classesName = null
                                    , string roomName = null, string teacherName = null)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);



            classesSearchVM.Classes = _db.Classes.Include(c => c.Rooms)
                                .Include(c => c.Subjects).Include(c => c.Teachers).ToList();
            classesSearchVM.Students = _db.Students.ToList();

            if (roomName != null)
            {
                classesSearchVM.Classes = classesSearchVM.Classes.Where(r => r.Rooms.Name.ToLower()
                                        .Contains(roomName.ToLower())).ToList();
            }
            if (subjectName != null)
            {
                classesSearchVM.Classes = classesSearchVM.Classes.Where(s => s.Subjects.Name.ToLower()
                                        .Contains(subjectName.ToLower())).ToList();
            }
            if (classesName != null)
            {
                classesSearchVM.Classes = classesSearchVM.Classes.Where(c => c.Name.ToLower()
                                        .Contains(classesName.ToLower())).ToList();
            }
            if (teacherName != null)
            {
                classesSearchVM.Classes = classesSearchVM.Classes.Where(r => r.Teachers.Name.ToLower()
                                        .Contains(teacherName.ToLower())).ToList();
            }
            classesSearchVM.lstStudyClasses = _db.StudyClasses.Where(sc => sc.Student.Email == User.Identity.Name).ToList();
            return View(classesSearchVM);
        }

        //get Add
        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            classesSearchVM.CurrClasses = await _db.Classes.FindAsync(id);

            if (classesSearchVM.CurrClasses == null)
            {
                return NotFound();
            }

            return View(classesSearchVM);
        }
        //Post Add
        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Addpost(int id)
        {
            if (ModelState.IsValid)
            {
                classesSearchVM.StudyClasses.classId = id;
                classesSearchVM.StudyClasses.studentsId = classesSearchVM.Students.Where(s => s.Email == User.Identity.Name).FirstOrDefault().Id;
                _db.Update(classesSearchVM.StudyClasses);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classesSearchVM.StudyClasses);
        }

        //show StudyClass
        public IActionResult ShowClass()
        {
            List<StudyClasses> studyClasses = _db.StudyClasses.Where(sc=>sc.Student.Email == User.Identity.Name).ToList();
            return View(studyClasses);
        }
        //get Withdraw
        public async Task<IActionResult> Withdraw(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyclasses = await _db.StudyClasses.FindAsync(id);
            if (studyclasses == null)
            {
                return NotFound();
            }
            classesSearchVM.StudyClasses = studyclasses;
            return View(classesSearchVM.StudyClasses);
        }

        //Post Withdraw
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(int id)
        {
            if(ModelState.IsValid)
            {
                var studyclasses = await _db.StudyClasses.FindAsync(id);
                _db.Remove(studyclasses);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classesSearchVM.StudyClasses);
        }

        ////show Enrolled
        public IActionResult ShowEnroll(int? id)
        {
            List<StudyClasses> studentclass = _db.StudyClasses.Where(sc => sc.classId == id).ToList();

            List<Models.Students> studentEnroll = new List<Models.Students>();

            foreach (var studentss in studentclass)
            {
                studentEnroll.Add(studentss.Student);
            }
            return View(studentEnroll);
        }
    }
}