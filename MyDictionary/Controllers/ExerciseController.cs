using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyDictionary.Models;
using MyDictionary.Models.ViewModels;
using MyDictionary.Utilitiy;

namespace MyDictionary.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext _db;
        Random rnd = new Random();

        [BindProperty]
        public ExerciseViewModel ExerciseVM { get; set; }


        public ExerciseController(ApplicationDbContext db)
        {
            _db = db;
            ExerciseVM = new ExerciseViewModel()
            {
                Word = new Word(),
                LessonList = _db.Words.OrderBy(u => u.Lesson).Select(w => w.Lesson).Distinct().ToList(),
                RepetitionList = new List<string>() { "No repetitions", "With Repetitions", "Only wrongs"}
            };
        }


        public IActionResult Index()
        {
            return View(ExerciseVM);
        }

        public async Task<IActionResult> Exercise(string lesson, string repetition)
        {
            if (!string.IsNullOrEmpty(lesson))
            {
                ExerciseVM.Lesson = lesson;
            }
            if (!string.IsNullOrEmpty(repetition))
            {
                ExerciseVM.Repetition = repetition;
            }
            var listOfWords = await _db.Words.Where(w => w.Lesson == ExerciseVM.Lesson).ToListAsync();
            int number = rnd.Next(0, listOfWords.Count);

            ExerciseVM.Word = listOfWords[number];

            return View(ExerciseVM);
        }
    }
}
