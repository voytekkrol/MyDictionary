using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Models;

namespace MyDictionary.Controllers
{
    public class WordsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Word Word{ get; set; }

        public WordsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Word = new Word();

            if (id == null)
            {
                return View(Word);
            }

            Word = _db.Words.FirstOrDefault(u => u.Id == id);
            if (Word == null)
            {
                return NotFound();
            }
            return View(Word);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Words.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var wordFromDb = await _db.Words.FirstOrDefaultAsync(u => u.Id == id);
            if (wordFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Words.Remove(wordFromDb);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
