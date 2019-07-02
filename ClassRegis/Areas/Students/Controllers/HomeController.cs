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
        
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string subjectName = null, string classesName = null
                                    , string roomName = null, string teacherName = null)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ClasssesSearchViewModel classesSearchVM = new ClasssesSearchViewModel()
            {
                Classes = new List<Classes>()
                
            };

            classesSearchVM.Classes = _db.Classes.Include(c => c.Rooms)
                                .Include(c => c.Subjects).Include(c => c.Teachers).ToList();
            

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

            return View(classesSearchVM);
        }
    }
}