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
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RoomController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            return View(_db.Rooms);
        }



        //get Create room
        public IActionResult Create()
        {
            return View();
        }


        //post Create room
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                _db.Add(rooms);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rooms);
        }

        //get Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _db.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            
            return View(room);
        }

        //get Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _db.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        //post Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _db.Rooms.FindAsync(id);
            _db.Rooms.Remove(room);
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

            var room = await _db.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        //post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rooms room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(room);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }
    }
}